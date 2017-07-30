using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;
using System.IO.MemoryMappedFiles;
using NetSharedMemory;
using ManagedMMF;

namespace WebApplication2
{
	public partial class ModulePortal : Page
	{
        Module module = new Module();
		protected String countMsg = "";
		protected void Page_Load(object sender, EventArgs e)
		{
            
		}

		protected void StartModule(object sender, System.EventArgs e)
		{
            System.Diagnostics.Process p1 = System.Diagnostics.Process.Start(@"C:\ws\VS\Projects\WebAppCounterTest-MMF-on-disk-serializable-objects\WebAppCounterTest-MMF-on-disk-serializable-objects\Process2\Process2\bin\Debug\Process2.exe");
			System.Diagnostics.Process p2 = System.Diagnostics.Process.Start(@"C:\ws\VS\Projects\WebAppCounterTest-MMF-on-disk-serializable-objects\WebAppCounterTest-MMF-on-disk-serializable-objects\Process3\bin\Debug\Process3.exe");

			Sequence seq;
            // Get size of struct
            int size = Marshal.SizeOf(typeof(Sequence));
            byte[] data = new byte[size];

            // Initialize unmanaged memory.
            IntPtr p = Marshal.AllocHGlobal(size);

            try
            {
                MemoryMappedFile mmf = MemoryMappedFile.CreateOrOpen("Object", size);

                // Create accessors to MMF
                MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor(0, size, MemoryMappedFileAccess.ReadWrite);
                // Read from memory mapped file.
                accessor.ReadArray<byte>(0, data, 0, data.Length);

                // Copy from byte array to unmanaged memory.
                Marshal.Copy(data, 0, p, size);

                // Copy unmanaged memory to struct.
                seq = (Sequence)Marshal.PtrToStructure(p, typeof(Sequence));
				string name = seq.Name;
            }
            finally
            {
                // Free unmanaged memory.
                Marshal.FreeHGlobal(p);
                p = IntPtr.Zero;
            }
			this.countMsg = "<br>Process 2 started with the value : " + seq.Name;


			// Build a sample object using a system file to store the MMF and report records
			ManagedMMF.TestObject obj = new ManagedMMF.TestObject();
			// Read new object from MMF and report records
			obj = ManagedMMF.Program.ReadObjectFromMMF("C:\\TEMP\\TEST.MMF") as ManagedMMF.TestObject;
			Console.WriteLine("obect re-loaded from MMF with " + obj.test1 + "& " + obj.test2);
			System.Console.WriteLine("obect re-loaded from MMF with " + obj.test1 + "& " + obj.test2);


		}

		protected void AddModule(object sender, System.EventArgs e)
		{
                    
            module.AddModule();
            int a = module.GetCount();
            this.countMsg += "<br>Process 1 added value : " + a;
            //System.Diagnostics.Debug.WriteLine("ModulePortal AddModule() moduleCount = " + a);

            int size = 100;

            MemoryMappedFile mmf = MemoryMappedFile.CreateOrOpen("Object", size);

            // Create accessors to MMF
            MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor(0, size, MemoryMappedFileAccess.ReadWrite);
            Sequence seq = new Sequence();

			seq.Name = "test_at_ModulePortal";

		}

     

      
	}
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct MyData
    {
        public int Value;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
        public string name;
    }
}