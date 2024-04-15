using System.Threading.Tasks;
using VerifyXunit;
using XrmFramework.Analyzers.Generators;
using Xunit;

namespace XrmFramework.Analyzers.Tests;

[UsesVerify]
public class JSFilesTests
{
    [Fact]
    public async Task CalculateJSSansEnum()
    {
        // The source code to test
        var source = @"";

        // Pass the source code to our helper and snapshot test the output

        await TestHelper.Verify<TableJsonDefinitionGenerator>(source,
            ("SansEnum.table", TableFiles.SansEnum)
            );

    }
}
