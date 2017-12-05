using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineParsingTesting
{
    internal class FileProcessor
    {
        internal static void ProcessFiles(SharedStateObject sharedState, List<string> fileList, StreamWriter outputFile, StreamWriter errorFile)
        {
            foreach (var file in fileList)
            {
                Console.WriteLine("Processing File: " + file);
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                using (FileStream fs = File.Open(file, FileMode.Open, FileAccess.Read))
                using (BufferedStream bs = new BufferedStream(fs))
                using (StreamReader sr = new StreamReader(bs))
                {
                    var errorOnLine = false;
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        errorOnLine = false;
                        //split row
                        var splitLine = line.Split('|');

                        //validate the number of columns
                        errorOnLine = ValidateColumns(splitLine, line, sharedState, errorFile);

                        //grab the id assume it's first
                        var ID = splitLine[0];
                        
                        //concatenate and hash the row
                        var hash = HashLine(string.Join("", splitLine));

                        if (sharedState.Arguments.HashIncluded)
                        {
                            //compare the hash generated to the one included assume it's last
                            errorOnLine = ValidateHash(splitLine, line, hash, errorFile);
                        }

                        //output the id and hash if there are no errors
                        if (!errorOnLine)
                        {
                            lock (outputFile)
                            {
                                outputFile.WriteLine(ID + "\t" + hash);
                            }
                        }
                    }
                }
                stopwatch.Stop();
                Console.WriteLine("Done Processing File: " + file + " in {0} seconds.", ((TimeSpan)stopwatch.Elapsed).Seconds);
                stopwatch.Reset();
            }
        }

        private static string HashLine(string line)
        {
            MD5 md5 = MD5.Create();
            var inputBytes = System.Text.Encoding.ASCII.GetBytes(line);
            var hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }

        private static bool ValidateColumns(string[] splitLine, string line, SharedStateObject sharedState, StreamWriter errorFile)
        {
            if (!splitLine.Count().Equals(sharedState.Arguments.Columns))
            {
                lock (errorFile)
                {
                    errorFile.WriteLine("Invalid number of columns expecting '" + sharedState.Arguments.Columns.ToString() + "' found '" + splitLine.Count().ToString() + "' in data \n\t" + line);
                }
                return true;
            }
            return false;
        }

        private static bool ValidateHash(string[] splitLine, string line, string calculatedHash, StreamWriter errorFile)
        {
            if (!splitLine[splitLine.Count()-1].Equals(calculatedHash))
            {

                lock (errorFile)
                {
                    errorFile.WriteLine("Hashes do not match calculated '" + splitLine[splitLine.Count() - 1] + "' given '" + splitLine[splitLine.Count() - 1] + "' in data \n\t + line");
                }
                return true;
            }
            return false;
        }
    }
}
