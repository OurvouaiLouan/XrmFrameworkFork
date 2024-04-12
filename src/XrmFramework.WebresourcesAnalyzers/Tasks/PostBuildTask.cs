using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Task = Microsoft.Build.Utilities.Task;


namespace WebresourcesAnalyzers;

public class PostBuildTask : Task
{
	[Required]
	public string GeneratedFilesPath { get; set; }
	
	public override bool Execute()
	{
		Log.LogWarning("Task successfully executed.");
		Log.LogWarning($"Task input : {GeneratedFilesPath}");

		return true;
	}
}
