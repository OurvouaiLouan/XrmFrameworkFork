using System;
using System.Threading.Tasks;
using VerifyXunit;
using XrmFramework.Analyzers.Generators;
using Xunit;

namespace XrmFramework.Analyzers.Tests;

[UsesVerify]
public class TableJsonDefinitionGeneratorTests
{
	[Fact]
	public async Task CalculateTableFiles()
	{
		// The source code to test
		var source = @"

";

        // Pass the source code to our helper and snapshot test the output

        /*await TestHelper.Verify<TableJsonDefinitionGenerator>(source,
            ("Account.table", TableFiles.Account)
            );*/

        /*await TestHelper.Verify<TableJsonDefinitionGenerator>(source,
            ("Contratdelocation.table", TableFiles.Contratdelocation)
            );*/

        await TestHelper.Verify<TableJsonDefinitionGenerator>(source,
            ("OptionSet.table", TableFiles.OptionSet)
            );

        /*await TestHelper.Verify<TableJsonDefinitionGenerator>(source,
            ("Particulier.table", TableFiles.Particulier)
            );*/

    }
}
