using CommandLineParsingTesting.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommandLineParsingTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            var parameters = ArgumentParser.ParseArguments(args);

            try
            {
                var fileList = Directory.GetFiles(parameters.DirectoryPath).ToList();

                /*split the file list into four pieces
                 * on each small list start a thread 
                 * thread runs the hashing algorithm
                 */

                var listToProcess = fileList.SplitList(4);

                ThreadPool.QueueUserWorkItem(new WaitCallback(), );

                using (FileStream fs = File.Open(listToProcess.ElementAt(0).ElementAt(0), FileMode.Open, FileAccess.Read))
                using (BufferedStream bs = new BufferedStream(fs))
                using (StreamReader sr = new StreamReader(bs))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {

                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            Console.Read();
        }
    }
}
