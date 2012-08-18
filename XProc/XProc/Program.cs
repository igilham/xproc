using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGilham.Xproc.Core;

// Apparently XProc is the name of a W3C tool. Never mind.
namespace IGilham.XProc
{
    class Program
    {
        static int Main(string[] args)
        {
            Program prog = new Program();
            return prog.Run(args);
        }

        /// <summary>
        /// Performs the work of running the application.
        /// </summary>
        /// <returns></returns>
        int Run(params string[] args)
        {
            if(!parameters_.Parse(args))
            {
                Console.Error.WriteLine(parameters_.HelpMessage);
                return -1;
            }
            var inputFiles = parameters_.InputPath.EnumerateFiles("*.xml");
            var batcher = new Batcher();
            batcher.ProcessBatch(parameters_.Stylesheet, parameters_.OutputPath, inputFiles);
            return 0;
        }

        private Parameters parameters_ = new Parameters();
    }
}
