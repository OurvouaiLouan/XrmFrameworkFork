using Microsoft.Build.Framework;
using Microsoft.CodeAnalysis;
using Task = Microsoft.Build.Utilities.Task;


namespace WebresourcesAnalyzers;

public class PostBuildTask : Task
{
	[Required]
	public string FilesToConvertPath { get; set; }
    public string ConvertedFilesPath { get; set; }

    public override bool Execute()
	{
        if(FilesToConvertPath == null || ConvertedFilesPath == null) return false;

        Log.LogWarning("Task successfully executed.");
        Log.LogWarning($"Files To Convert : {FilesToConvertPath}");
        Log.LogWarning($"Converted Files : {ConvertedFilesPath}");


        Log.LogWarning($"___Input Files___");
        string[] files = Directory.GetFiles(FilesToConvertPath);
        int index = 0;
        foreach (string file in files)
        {
            index++;
            Log.LogWarning($"Input File {index}: {file}");
            string pathOutput = $"{ConvertedFilesPath}/{file.Split('\\').Last().Split('.').First()}.js";
            string contentOutput = File.ReadAllText(file);
            if(contentOutput.Length > 27) 
                contentOutput = contentOutput.Remove(contentOutput.Count()-9,9).Remove(0,18);
            File.WriteAllText(pathOutput, contentOutput);
            
        }


        Log.LogWarning($"___Output Files___");
        files = Directory.GetFiles(ConvertedFilesPath);
        index = 0;
        foreach (string file in files)
        {

            index++;
            Log.LogWarning($"Output File {index}: {file}");
        }


        return true;
	}
}
