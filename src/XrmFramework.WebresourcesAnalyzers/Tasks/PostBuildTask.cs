using Microsoft.Build.Framework;
using Task = Microsoft.Build.Utilities.Task;


namespace WebresourcesAnalyzers;

public class PostBuildTask : Task
{
	[Required]
	public string FilesToConvertPath { get; set; }
    public string ConvertedFilesPath { get; set; }

    public override bool Execute()
	{
        string[] files = Directory.GetFiles(FilesToConvertPath);
        Log.LogWarning("Task successfully executed.");
        Log.LogWarning($"Files To Convert : {FilesToConvertPath}");
        Log.LogWarning($"Converted Files : {ConvertedFilesPath}");
        int index = 0;
        foreach (string file in files)
        {
            index++;
            Log.LogWarning($"File from Director {index}: {file}");
        }
		

        return true;
	}
}
