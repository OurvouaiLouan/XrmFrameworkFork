using Microsoft.Build.Framework;
using System.IO;
using System.Linq;
using Task = Microsoft.Build.Utilities.Task;

namespace WebresourcesAnalyzers.Tasks;

public class MoveGeneratedJson : Task
{
    [Required] public string FilesToConvertPath { get; set; }
    [Required] public string ConvertedFilesPath { get; set; }

    public override bool Execute()
    {
        Directory.CreateDirectory(ConvertedFilesPath);

        var files = Directory.GetFiles(FilesToConvertPath);
        for (int i = 0; i < files.Length; i++)
        {
            var file = files[i];
            Log.LogMessage($"Processing File {i + 1}: {file}");

            var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(file);
            var outputPath = Path.Combine(ConvertedFilesPath, $"{fileNameWithoutExtension}.js");
            var fileContent = File.ReadLines(file).Skip(1).TakeWhile((line, index) => index < File.ReadLines(file).Count() - 2);
            
            File.WriteAllLines(outputPath, fileContent);
            Log.LogMessage($"Output File {i + 1}: {outputPath}");
        }

        return true;
    }
}