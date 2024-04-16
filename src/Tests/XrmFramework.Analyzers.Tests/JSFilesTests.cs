using System;
using System.IO;
using System.Threading.Tasks;
using VerifyXunit;
using Xunit;

namespace XrmFramework.Analyzers.Tests;

[UsesVerify]
public class JSFilesTests
{
    [Fact]
    public async Task TestDefinitionJS()
    {


        string pathOutput = "../../../../../JsonTests/GeneratedJson/Output/";
        string[] files = Directory.GetFiles(pathOutput);
        Console.WriteLine($"Output Folder: {pathOutput}");
        int index = 0;
        foreach (var file in files) 
        {
            index++;
            Console.WriteLine("________________________");
            Console.WriteLine($"File {index}: {file}");

            Xunit.Assert.DoesNotContain("#if COMPILE_JSON", File.ReadAllText(file));
            Console.WriteLine($"Does not Contain: #if COMPILE_JSON");

            Xunit.Assert.DoesNotContain("#endif", File.ReadAllText(file));
            Console.WriteLine($"Does not Contain: #endif");

            Xunit.Assert.Equal(".js", file.Substring(file.Length - 3));
            Console.WriteLine($"The extension is: .js");
        }



    }
}
