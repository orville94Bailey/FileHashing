using CommandLineParsingTesting.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandLineParsingTesting
{
    public class SharedStateObject
    {
        public bool ContinueProcess
        {
            get
            {
                return TotalFiles == FinishedFiles;
            }
        }
        public int TotalFiles { get; set; }
        public int FinishedFiles { get; set; }
        internal ParsedArguments Arguments { get; set; }
    }
}
