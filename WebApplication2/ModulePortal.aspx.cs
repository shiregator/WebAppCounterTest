using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NetSharedMemory;

namespace WebApplication2
{
	public partial class ModulePortal : Page
	{
        Module module = new Module();
		protected String countMsg = "";
		protected void Page_Load(object sender, EventArgs e)
		{
            module = (Module)Session["module"];
            if (module == null)
            {
                module = new Module();
                Session["module"] = module;
            }
            System.Diagnostics.Debug.WriteLine("Page_Load() moduleCount = " + module.GetCount());  
            
		}

		protected void StartModule(object sender, System.EventArgs e)
		{
            System.Diagnostics.Process p = System.Diagnostics.Process.Start(@"C:\ws\VS\Projects\WebAppCounterTest-reflection-enhancement\Process2\Process2\bin\Debug\Process2.exe");
            SharedMemory<MyData> shmem = new SharedMemory<MyData>("Count", 32);
            if (!shmem.Open()) return;
            MyData data = new MyData();
            // Read from shared memory
            data = shmem.Data;             
            
            this.countMsg = "<br>Process 2 started with the value : " + data.Value;
            
		}

		protected void AddModule(object sender, System.EventArgs e)
		{
                    
            module.AddModule();
            int a = module.GetCount();
            this.countMsg += "<br>Process 1 added value : " + a;
            //System.Diagnostics.Debug.WriteLine("ModulePortal AddModule() moduleCount = " + a);

            SharedMemory<MyData> shmem = new SharedMemory<MyData>("Count", 32);

            if (!shmem.Open()) return;
            MyData data = new MyData();

            //// Read from shared memory
            data = shmem.Data;

            data.Value = data.Value + a;
            this.countMsg += "<br>Final Value after addition process 1 & process 2 values : " + data.Value;

		}

     

      
	}
    public struct MyData
    {
        public int Value;
    }
}