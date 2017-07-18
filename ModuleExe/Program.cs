using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ModuleExe
{
    public class Module
    {
        private int moduleCount = 0;

        public void InitializeModule(int initCount)
        {
            this.moduleCount = initCount;
            System.Diagnostics.Debug.WriteLine("Module InitializeModule() moduleCount = " + this.moduleCount);
            System.Console.WriteLine("Module InitializeModule() moduleCount = " + this.moduleCount);
        }

        public void AddModule()
        {
            this.moduleCount++;
            System.Diagnostics.Debug.WriteLine("Module incrementCount() moduleCount = " + this.moduleCount);
            System.Console.WriteLine("Module incrementCount() moduleCount = " + this.moduleCount);

        }

        public int GetCount()
        {
            return this.moduleCount;
        }

        public static void Main(string[] args)
        {
            int beginCount = 5;
            Module module = new Module();

            module.InitializeModule(beginCount);
            module.AddModule();
            System.Diagnostics.Debug.WriteLine("Main() moduleCount = " + module.GetCount());
            System.Console.WriteLine("Main() moduleCount = " + module.GetCount());

            using (var mmf = MemoryMappedFile.CreateFromFile(Path.GetTempFileName(), FileMode.Open, "ImgA", 1024))
            {
                using (var accessor = mmf.CreateViewAccessor(1, 16))
                {
                    VegasObject vegas = new VegasObject()
                    {
                        Count = 42 // The answer to everything
                    };

                    accessor.Write(0, ref vegas);
                    accessor.Flush();

                    Console.ReadLine();
                }
            }
        }
    }

    public struct VegasObject
    {
        public int Count;
    }
}
