﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Xrm.Sdk;
using System.Threading.Tasks;
using VerifyXunit;

namespace XrmFramework.Analyzers.Tests
{
    public static class TestHelper
    {
        public static Task Verify<TGenerator>(string source, params (string path, byte[] content)[] additionalTexts) where TGenerator : IIncrementalGenerator, new()
        {
            // Parse the provided string into a C# syntax tree
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);

            // Create a Roslyn compilation for the syntax tree.
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName: "Tests",
                syntaxTrees: new[] { syntaxTree },
                references: new[] {
                    MetadataReference.CreateFromFile(typeof(Task).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(Entity).Assembly.Location)
                });


            // Create an instance of our EnumGenerator incremental source generator
            var generator = new TGenerator();

            // The GeneratorDriver is used to run our generator against a compilation
            GeneratorDriver driver = CSharpGeneratorDriver
                .Create(generator);

            // Run the source generator!
            driver = driver.RunGenerators(compilation);

            // Use verify to snapshot test the source generator output!
            return Verifier
                    .Verify(driver)
                    .UseDirectory("TestData");
        }
    }
}
