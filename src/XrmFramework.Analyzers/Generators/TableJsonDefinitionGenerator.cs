using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using XrmFramework.Analyzers.Definitions;
using XrmFramework.Core;

namespace XrmFramework.Analyzers.Generators;

[Generator]
public class TableJsonDefinitionGenerator : BaseTableDefinitionGenerator
{
	protected override bool GenerateTableFiles => true;

	protected override void WriteTable(SourceProductionContext productionContext, TableCollection tables, Table table, HashSet<string> alreadyCreatedEnums)
	{

		/*
		 * On créer une liste représentant la section Columns d'un Json
		 * JsonColumn représente un élément de la section Columns
		 */

		List<JsonColumn> jsonColumns = new List<JsonColumn>();

		table.Columns.Take(table.Columns.Count)
			.ToList()
			.ForEach(delegate (Column column)
            {
				JsonColumn jsonColumn = new JsonColumn(column.Name,column.LogicalName);

				jsonColumns.Add(jsonColumn);

            });

		/*
         * On créer une liste représentant la section Enums d'un Json
		 * JsonEnum représente un élément de la section Enums
		 */
        List<JsonEnum> jsonEnums = new List<JsonEnum>();

		table.Enums.Take(table.Enums.Count)
			.ToList()
			.ForEach(delegate (OptionSetEnum optionSetEnum)
            {
				/*
                 * JsonEnumValue représente un sous-élément d'un élément de la section Enums
				 */
                List<JsonEnumValue> jsonEnumValues = new List<JsonEnumValue>();

				optionSetEnum.Values.Take(optionSetEnum.Values.Count)
					.ToList()
					.ForEach(delegate (OptionSetEnumValue o)
					{
						JsonEnumValue jsonEnumValue = new JsonEnumValue(ParseName(o.Name),o.Value);

						jsonEnumValues.Add(jsonEnumValue);
					});

                JsonEnum jsonEnum = new JsonEnum(optionSetEnum.Name, jsonEnumValues);

				jsonEnums.Add(jsonEnum);
			});
		/*
		 * On construit un JsonDefinition, une class représentant la structure d'un fichier Json
		 */
        JsonDefinition jsonDefinition = new JsonDefinition(table.LogicalName,
                                                            table.Name,
                                                            table.CollectionName,
                                                            table.CollectionName,
                                                            table.Columns.FirstOrDefault(c => c.PrimaryType == PrimaryType.Id)?.LogicalName,
                                                            table.Columns.FirstOrDefault(c => c.PrimaryType == PrimaryType.Name)?.LogicalName,
															jsonColumns,
															jsonEnums);
		/*
         * On remplie le string builder avec les informations du JsonDefinition
		 */
        var sb = jsonDefinition.WriteJson();


        // File.WriteAllText($"../../../../../JsonTests/GeneratedJson/Input/{table.Name}Definition.js", sb.ToString());


        /*
         * On crée le chemin pour cette table
         */
        productionContext.AddSource($"{table.Name}Definition.json", SourceText.From(sb.ToString(), System.Text.Encoding.UTF8));

	}

	private string ParseAllCharacterOfAWord(string word, string valideCharacters)
	{
		if ( String.IsNullOrEmpty(word)) return word;

		string finalWord = string.Concat(
			word
			.Where(charac => valideCharacters.Contains(charac))
			);

		return finalWord;
	}


    private string ParseFirstCharacterOfAWord(string word, string valideCharacters)
	{
		if (String.IsNullOrEmpty(word)) return "_";

		if (valideCharacters.Contains(word[0])) return word;
		else return "_" + word;
	}

	private string ParseName(string name)
	{
		const string valideFirstCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

		const string valideCharacters = "0123456789_";



		var valideCharacters2 = valideFirstCharacters + valideCharacters;

		name = ParseFirstCharacterOfAWord(name, valideFirstCharacters);

		name = ParseAllCharacterOfAWord(name, valideCharacters2);


        return name;
	}
}
