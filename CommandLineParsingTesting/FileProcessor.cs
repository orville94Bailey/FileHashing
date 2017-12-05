using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineParsingTesting
{
    internal class FileProcessor
    {
        internal static void ProcessFiles(SharedStateObject sharedState, List<string> fileList, StreamWriter outputFile)
        {
            foreach (var file in fileList)
            {
                using (FileStream fs = File.Open(file, FileMode.Open, FileAccess.Read))
                using (BufferedStream bs = new BufferedStream(fs))
                using (StreamReader sr = new StreamReader(bs))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        //split row
                        var splitLine = line.Split('|');
                        //validate the number of columns

                        //grab the id
                        //concatenate row
                        //hash the row
                        //output the id and hash
                        lock (outputFile)
                        {
                            outputFile.WriteAsync()
                        }
                    }
                }
            }
        }
    }
}
