using System;
using System.Collections;
using NetSharedMemory;

namespace Process2
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
            System.Console.WriteLine("Process 2 Called!!");
            SharedMemory<MyData> shmem = new SharedMemory<MyData>("Count", 32);

            if (!shmem.Open()) return;

            MyData data = new MyData();
            //SET The Count Value

            data.Value = 5;
            shmem.Data = data;
        
            System.Console.WriteLine("Process 2 set value :" + data.Value);
            System.Threading.Thread.Sleep(60000);
        }
        public struct MyData
        {
            public int Value;
			// Cannot share ref object such as string without serialization of the object
			//public string s;
        }
    }
}