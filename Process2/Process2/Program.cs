using System;
using System.Collections;
using NetSharedMemory;
using System.Runtime.InteropServices;
using System.IO.MemoryMappedFiles;


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
			// Use system (RAM) memory to store MMF

            System.Console.WriteLine("Process 2 Called!!");

            int size = 2048;
            MemoryMappedFile mmf = MemoryMappedFile.CreateOrOpen("Object", size);

            // Create accessors to MMF
            MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor(0, size, MemoryMappedFileAccess.ReadWrite);
            Sequence seq = new Sequence();

            seq.Name = "Shared String 1234";
            seq.Current = 1;
			// Uncommenting the following line will cause a runtimeexception because the TestObject class is not serializable
			//seq.testObject = new TestObject();

            MMFOperation.WriteSequence(accessor, seq);
        
            System.Console.WriteLine("Process 2 set value :" + seq.Name);
            System.Threading.Thread.Sleep(60000);
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct MyData
        {
            public int Value;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
            public string name;
        }
    }
}