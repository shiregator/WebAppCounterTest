using System;
using System.Collections;
using System.Web.UI;

namespace WebApplication2
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

			System.Threading.Thread.Sleep(60000);
		}
	}
}