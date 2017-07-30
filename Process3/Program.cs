using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManagedMMF;

namespace Process3
{
	class Program
	{
		static void Main(string[] args)
		{
			// Build a sample object in MMF using a system file and serialized objects and report records
			TestObject obj = new TestObject();
			obj = ManagedMMF.Program.CreateTestObject();
			Console.WriteLine("object created with " + obj.test1 + "& " + obj.test2);

			// Write object to MMF
			ManagedMMF.Program.WriteObjectToMMF("C:\\TEMP\\TEST.MMF", obj);

			// Clear object and report
			obj = null;
			Console.WriteLine("Database object has been destroyed.");

			// Read new object from MMF and report records
			obj = ManagedMMF.Program.ReadObjectFromMMF("C:\\TEMP\\TEST.MMF") as TestObject;
			Console.WriteLine("obect re-loaded from MMF with " + obj.test1 + "& " + obj.test2);

			System.Console.WriteLine("Process 3 obect re-loaded from MMF with " + obj.test1 + "& " + obj.test2);
			System.Threading.Thread.Sleep(60000);

			// Wait for input and terminate
			Console.ReadLine();
		}
	}
}
