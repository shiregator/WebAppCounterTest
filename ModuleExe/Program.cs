using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
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

            // Send a random number to the web application
            var randomNumber = new Random();

            if (args.Length > 0)
            {
                using (PipeStream pipeClient = new AnonymousPipeClientStream(PipeDirection.Out, args[0]))
                {
                    using (StreamWriter sw = new StreamWriter(pipeClient))
                    {
                        do
                        {
                            sw.AutoFlush = true;
                            
                            sw.WriteLine("SYNC");
                            pipeClient.WaitForPipeDrain();

                            var randomMessage = randomNumber.Next();
                            sw.WriteLine(randomMessage.ToString());
                            Debug.WriteLine("Sent web application a message: " + randomMessage.ToString());

                            // Simulate some work being done
                            Thread.Sleep(1000);

                        } while (true);
                    }
                }
            }
        }
    }
}
