﻿
using BoDi;
using System;
using XrmFramework.Utils;

namespace XrmFramework
{
    // ReSharper disable once PartialTypeWithSinglePart
    public static partial class InternalDependencyProvider
    {
        static partial void RegisterServices(IObjectContainer container);
        static partial void RegisterCustomService(IObjectContainer container);


        public static void RegisterDefaults(IObjectContainer container)
        {
            container.RegisterTypeAs<DateTimeProvider, IDateTimeProvider>();

            RegisterServices(container);
            RegisterCustomService(container);
        }

        // ReSharper disable once UnusedMember.Local
        private static void RegisterService<TIService, TImplementation, TLoggedService>(IObjectContainer container)
            where TIService : IService
            where TImplementation : class, TIService
            where TLoggedService : class, TIService
        {
            container.RegisterTypeAs<TImplementation, TImplementation>();

            container.RegisterFactoryAs(objectContainer =>
            {
                var context = objectContainer.Resolve<IServiceContext>();
                var service = objectContainer.Resolve<TImplementation>();

                // ReSharper disable once SuspiciousTypeConversion.Global
                if (service is IInitializableService serviceWithSettings)
                {
                    serviceWithSettings.Init();
                }

                return (TIService)Activator.CreateInstance(typeof(TLoggedService), context, service);
            });
        }
    }
}
