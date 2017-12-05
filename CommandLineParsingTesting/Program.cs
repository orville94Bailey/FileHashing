using CommandLineParsingTesting.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CommandLineParsingTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            var parameters = ArgumentParser.ParseArguments(args);
            const int numOfThreads = 4;
            stopwatch.Start();
            try
            {
                var fileList = Directory.GetFiles(parameters.DirectoryPath).ToList();

                /*split the file list into four pieces
                 * on each small list start a thread 
                 * thread runs the hashing algorithm
                 */

                var listToProcess = fileList.SplitList(numOfThreads);
                var sharedStateObj = new SharedStateObject() { Arguments = parameters };

                var threadList = new List<Thread>();

                var errorFile = new StreamWriter("error.txt", true);
                var outputFile = new StreamWriter("output.txt", true);

                foreach (var item in listToProcess)
                {
                    threadList.Add(new Thread(() => FileProcessor.ProcessFiles(sharedStateObj, item, outputFile, errorFile)));
                }
                

                foreach (var thread in threadList)
                {
                    thread.Start();
                }

                var stillRunning = false;
                do
                {
                    Thread.Sleep(5000);
                    stillRunning = false;
                    foreach (var thread in threadList)
                    {
                        if(thread.IsAlive)
                        {
                            stillRunning = true;
                        }
                    }
                } while (stillRunning);

                errorFile.Flush();
                errorFile.Close();
                outputFile.Flush();
                outputFile.Close();
            }
            catch (Exception e)
            {
                throw;
            }

            stopwatch.Stop();
            Console.WriteLine("Hashing finished in {0} seconds.", ((TimeSpan)stopwatch.Elapsed).Seconds);
            Console.Read();
        }
    }
}
