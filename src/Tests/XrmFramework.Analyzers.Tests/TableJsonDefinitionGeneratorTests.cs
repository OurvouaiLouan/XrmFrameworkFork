using System.Threading.Tasks;
using VerifyXunit;
using XrmFramework.WebresourcesAnalyzers.Generators;
using Xunit;

namespace XrmFramework.Analyzers.Tests;

[UsesVerify]
public class TableJsonDefinitionGeneratorTests
{
	[Fact]
	public async Task CalculateTableSansEnum()
	{
		// The source code to test
		var source = @"";

        // Pass the source code to our helper and snapshot test the output

        await TestHelper.Verify<TableJsonDefinitionGenerator>(source,
            ("SansEnum.table", TableFiles.SansEnum)
            );

    }

    [Fact]
    public async Task CalculateTableSansColSansEnum()
    {
        // The source code to test
        var source = @"";

        // Pass the source code to our helper and snapshot test the output

        await TestHelper.Verify<TableJsonDefinitionGenerator>(source,
            ("SansColSansEnum.table", TableFiles.SansColSansEnum)
            );

    }

    [Fact]
    public async Task CalculateTableAccount()
    {
        // The source code to test
        var source = @"";

        // Pass the source code to our helper and snapshot test the output

        await TestHelper.Verify<TableJsonDefinitionGenerator>(source,
            ("Account.table", TableFiles.Account)
            );

    }

    [Fact]
    public async Task CalculateTableContratdelocation()
    {
        // The source code to test
        var source = @"";

        // Pass the source code to our helper and snapshot test the output

        await TestHelper.Verify<TableJsonDefinitionGenerator>(source,
            ("Contratdelocation.table", TableFiles.Contratdelocation)
            );

    }

    [Fact]
    public async Task CalculateTableOptionSet()
    {
        // The source code to test
        var source = @"";

        // Pass the source code to our helper and snapshot test the output

        await TestHelper.Verify<TableJsonDefinitionGenerator>(source,
            ("OptionSet.table", TableFiles.OptionSet)
            );

    }

    [Fact]
    public async Task CalculateTableParticulier()
    {
        // The source code to test
        var source = @"";

        // Pass the source code to our helper and snapshot test the output

        await TestHelper.Verify<TableJsonDefinitionGenerator>(source,
            ("Particulier.table", TableFiles.Particulier)
            );

    }
}
