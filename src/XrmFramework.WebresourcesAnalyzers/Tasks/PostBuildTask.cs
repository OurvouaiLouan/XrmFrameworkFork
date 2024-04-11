using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Task = Microsoft.Build.Utilities.Task;


namespace XrmFramework.WebresourcesAnalyzers.Tasks;

public class PostBuildTask : Task
{
	[Required]
	public string GeneratedFilesPath { get; set; }
	
	public override bool Execute()
	{
		Log.LogWarning("Task Execute");
		Log.LogWarning($"Generated files path: {GeneratedFilesPath}");

		return true;
	}
}
