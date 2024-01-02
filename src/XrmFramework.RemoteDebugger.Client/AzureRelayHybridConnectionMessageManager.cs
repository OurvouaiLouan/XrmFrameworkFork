﻿using Microsoft.Azure.Relay;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XrmFramework.RemoteDebugger.Client.Recorder;
using XrmFramework.RemoteDebugger.Common;

namespace XrmFramework.RemoteDebugger.Client
{
    public class AzureRelayHybridConnectionMessageManager : IRemoteDebuggerMessageManager
    {
        private static readonly ConcurrentDictionary<Guid, RemoteDebuggerMessage> MessageSendCache = new ConcurrentDictionary<Guid, RemoteDebuggerMessage>();
        private static readonly ConcurrentDictionary<Guid, RelayedHttpListenerResponse> CurrentResponseCache = new ConcurrentDictionary<Guid, RelayedHttpListenerResponse>();
        private static readonly ConcurrentDictionary<Guid, RemoteDebuggerMessage> MessageReceiveCache = new ConcurrentDictionary<Guid, RemoteDebuggerMessage>();

        public event Action<RemoteDebugExecutionContext> ContextReceived;
        
        private ISessionRecorder SessionRecorder { get; set; }

        public AzureRelayHybridConnectionMessageManager()
        {
            // Check that a debug session exists for this project
            if (ConfigurationManager.ConnectionStrings["DebugConnectionString"] == null)
            {
                throw new Exception("The connectionString \"DebugConnectionString\" is not defined.");
            }

            // create a connection string with the listener profile
            Listener = new HybridConnectionListener(ConfigurationManager.ConnectionStrings["DebugConnectionString"].ConnectionString);

            Listener.Connecting += (_, _) => { Console.WriteLine("Listener is connecting to Azure…"); };
            Listener.Offline += (_, _) => { Console.WriteLine("Listener is about to go offline…"); };
            Listener.Online += (_, _) => { Console.WriteLine("Listener is online…"); };

            Listener.RequestHandler = RequestHandler;
        }

        private HybridConnectionListener Listener { get; }

        private void RequestHandler(RelayedHttpListenerContext context)
        {
            var streamReader = new StreamReader(context.Request.InputStream);
            // reading the body
            var requestContent = streamReader.ReadToEnd();

            // Get the message object through deserialization
            var message = JsonConvert.DeserializeObject<RemoteDebuggerMessage>(requestContent);
            
            SessionRecorder?.AddMessage(message);
            
            // Cache it
            CurrentResponseCache.AddOrUpdate(message.PluginExecutionId, context.Response, (_, _) => context.Response);


            switch (message.MessageType)
            {
                case RemoteDebuggerMessageType.Ping:
                    SendMessage(new RemoteDebuggerMessage(RemoteDebuggerMessageType.Ping, null, message.PluginExecutionId));
                    break;
                case RemoteDebuggerMessageType.Context:
                    {
                        // Get context info
                        var remoteContext = message.GetContext<RemoteDebugExecutionContext>();

                        OnContextReceived(remoteContext);

                        // The plugin is done executing, we can send back the Context
                        SendMessage(new RemoteDebuggerMessage(RemoteDebuggerMessageType.Context, remoteContext, remoteContext.Id));
                        break;
                    }
                case RemoteDebuggerMessageType.Response:
                    {
                        // Cache it
                        MessageReceiveCache.TryAdd(message.PluginExecutionId, message);

                        RemoteDebuggerMessage response;
                        // Loop while until it is possible to remove the response from cache which means it is ready to be sent
                        while (!MessageSendCache.TryRemove(message.PluginExecutionId, out response))
                        {
                            // Waiting for the response to come
                            Thread.Sleep(50);
                        }

                        SendMessage(response);
                        break;
                    }
            }
        }

        public Task SendMessage(RemoteDebuggerMessage message)
        {
            if (!CurrentResponseCache.TryGetValue(message.PluginExecutionId, out var currentResponse))
                return Task.CompletedTask;
            
            SessionRecorder?.AddMessage(message);
            
            var bytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            try
            {
                currentResponse.OutputStream.Write(bytes, 0, bytes.Length);
                currentResponse.Close();
            }
            catch (Exception)
            {
                // erreur ignorée
            }

            return Task.CompletedTask;
        }

        public Task<RemoteDebuggerMessage> SendMessageWithResponse(RemoteDebuggerMessage message)
        {
            SendMessage(message);

            RemoteDebuggerMessage response;

            while (!MessageReceiveCache.TryRemove(message.PluginExecutionId, out response))
            {
                // Waiting for the response to come
                Thread.Sleep(50);
            }

            return Task.FromResult(response);
        }

        public void SetSessionRecorder(ISessionRecorder sessionRecorder)
        {
            SessionRecorder = sessionRecorder;
        }

        public void RunAndBlock()
        {
            Listener.OpenAsync().GetAwaiter().GetResult();

            Console.In.ReadLineAsync().GetAwaiter().GetResult();

            Listener.CloseAsync().GetAwaiter().GetResult();
        }

        protected virtual void OnContextReceived(RemoteDebugExecutionContext obj)
        {
            ContextReceived?.Invoke(obj);
        }
    }
}