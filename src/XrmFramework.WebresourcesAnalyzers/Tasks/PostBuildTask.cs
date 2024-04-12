using Microsoft.Build.Framework;
using XrmFramework.WebresourcesAnalyzers.Definitions;
using Task = Microsoft.Build.Utilities.Task;


namespace WebresourcesAnalyzers;

public class PostBuildTask : Task
{
	[Required]
	public string GeneratedFilesPath { get; set; }
	
	public override bool Execute()
	{


		JsonDefinition jsonDefinition = new JsonDefinition();

        var sb = jsonDefinition.WriteJson();

        Log.LogWarning("Task successfully executed.");
		Log.LogWarning($"Task input :");
		Log.LogWarning(sb.ToString());

		return true;
	}
}
