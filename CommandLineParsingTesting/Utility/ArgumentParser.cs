using CommandLineParsingTesting.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CommandLineParsingTesting.Enums.ArgumentEnums;

namespace CommandLineParsingTesting.Utility
{
    public static class ArgumentParser
    {
        internal static ParsedArguments ParseArguments(string[] args)
        {
            var parsedArgs = new ParsedArguments();

            if (args.Length.Equals(0))
            {
                Console.WriteLine(@"You must include at least the '-d' tag followed by a path to a folder.");
                return null;
            }
            else if (args.Length.Equals(1))
            {
                parsedArgs.DirectoryPath = args[0];
            }

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-" + nameof(ArgumentTags.c):
                        if (args.Length >= i + 1)
                        {
                            try
                            {
                                parsedArgs.Columns = Convert.ToInt32(args[i + 1]);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(new Exception("-c parameter must be followed by an integer.", e).ToString());
                            }
                        }
                        break;
                    case "-" + nameof(ArgumentTags.d):
                        if (args.Length >= i + 1)
                        {
                            try
                            {
                                parsedArgs.DirectoryPath = args[i + 1];
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(new Exception("-d parameter must be followed by a string.", e).ToString());
                            }
                        }
                        break;
                    case "-" + nameof(ArgumentTags.e):
                        throw new NotImplementedException();
                        break;
                    case "-" + nameof(ArgumentTags.h):
                        if (args.Length >= i + 1)
                        {
                            try
                            {
                                parsedArgs.HashIncluded = Convert.ToBoolean(args[i + 1]);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(new Exception("-h parameter must be followed by 'true' or 'false'.", e).ToString());
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            return parsedArgs;
        }
    }

    internal class ParsedArguments
    {
        public string DirectoryPath { get; set; }
        public int Columns { get; set; }
        public bool HashIncluded { get; set; }

        public ParsedArguments()
        {
        }
    }
}
