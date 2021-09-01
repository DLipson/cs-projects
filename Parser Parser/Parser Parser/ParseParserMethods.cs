using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser_Parser
{
    static public class ParseParserMethods
    {
        static public void Parse()
        {
            string origParserPath = "H:\\Documents\\Parser_orig.txt";
            string newParserPath = "H:\\Documents\\Parser_new.txt";
            RecordMaker.MakeRecord(origParserPath, newParserPath);
        }

        static public string GetSubString(string item, string charsBefore, string charsAfter)
        {
            int startIndex = item.IndexOf(charsBefore);
            int endIndex = item.IndexOf(charsAfter);
            int subStringStart = startIndex + 1;
            int subStringLength = endIndex - startIndex;
            return item.Substring(subStringStart, subStringLength);
        }

        static public void AppendToFile(string path, string textToAppend)
        {

            using StreamWriter file = new(path, append: true);
            file.WriteLineAsync(textToAppend);

        }
    }

    static public class RecordMaker
    {
        public static void MakeRecord(string origParserPath, string newParserPath)
        {

            string csvFieldName;
            string recordProperty;
            bool previousLineWasCsvField = false;
            StreamReader reader = File.OpenText(origParserPath);

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains("CsvField"))
                {
                    csvFieldName = ParseCsvFieldName(line);
                   ParseParserMethods.AppendToFile(newParserPath, $"[Name = {csvFieldName}]");
                    previousLineWasCsvField = true;
                }
                else if (previousLineWasCsvField == true)
                {
                    recordProperty = line;
                    ParseParserMethods.AppendToFile(newParserPath, recordProperty);
                    previousLineWasCsvField = false;
                }

            }
        }
        static private string ParseCsvFieldName(string item)
        {
            string charsBefore = "= ";
            string charsAfter = ")]";

            return ParseParserMethods.GetSubString(item, charsBefore, charsAfter);
        }

    }
}