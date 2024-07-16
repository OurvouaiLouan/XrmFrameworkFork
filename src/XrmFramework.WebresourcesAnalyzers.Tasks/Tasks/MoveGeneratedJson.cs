using Microsoft.Build.Framework;
using Task = Microsoft.Build.Utilities.Task;

namespace WebresourcesAnalyzers.Tasks;

public class MoveGeneratedJson : Task
{
    [Required] public string FilesToConvertPath { get; set; }

    [Required] public string ConvertedFilesPath { get; set; }

    public override bool Execute()
    {
        if (!Directory.Exists(ConvertedFilesPath)) Directory.CreateDirectory(ConvertedFilesPath);
        
        Log.LogMessage($"Files To Convert : {FilesToConvertPath}");
        
        Log.LogMessage($"___Input Files___");
        var files = Directory.GetFiles(FilesToConvertPath);
        var index = 0;
        foreach (var file in files)
        {
            index++;
            Log.LogMessage($"Input File {index}: {file}");
            var pathOutput = $"{ConvertedFilesPath}/{file.Split('\\').Last().Split('.').First()}.js";
            var rawContent = File.ReadAllLines(file);
            var listRawContent = rawContent.ToList();
            listRawContent.RemoveAt(0);
            listRawContent.RemoveAt(listRawContent.Count - 1);
            var contentOutput = listRawContent;
            File.WriteAllLines(pathOutput, contentOutput);
        }

        Log.LogMessage($"Converted Files : {ConvertedFilesPath}");
        Log.LogMessage($"___Output Files___");

        files = Directory.GetFiles(ConvertedFilesPath);
        index = 0;
        foreach (var file in files)
        {
            index++;
            Log.LogMessage($"Output File {index}: {file}");
        }
        
        return true;
    }
}