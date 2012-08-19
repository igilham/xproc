using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGilham.XProc.Core;

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
            if(!argParser_.Parse(args))
            {
                Console.Error.WriteLine(argParser_.HelpMessage);
                return -1;
            }
            var inputFiles = argParser_.InputPath.EnumerateFiles("*.xml");
            var batcher = new Batcher(new ClrXslTransformer(argParser_.Stylesheet));
            batcher.ProcessBatch(argParser_.OutputPath, inputFiles);
            return 0;
        }

        private ArgumentParser argParser_ = new ArgumentParser();
    }
}
