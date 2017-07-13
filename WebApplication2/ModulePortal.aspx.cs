using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ModuleExe;

namespace WebApplication2
{
	public partial class ModulePortal : Page
	{
		Module module;
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
		    var p = new System.Diagnostics.Process
		    {
		        StartInfo = {FileName = @"C:\dev\WebAppCounterTest\ModuleExe\bin\Debug\ModuleExe.exe"}
		    };

		    using (AnonymousPipeServerStream pipeServer = new AnonymousPipeServerStream(PipeDirection.In, HandleInheritability.Inheritable))
		    {
		        // Pass the client process a handle to the server.
		        p.StartInfo.Arguments =
		            pipeServer.GetClientHandleAsString();
		        p.StartInfo.UseShellExecute = false;
		        p.Start();

		        pipeServer.DisposeLocalCopyOfClientHandle();

		        using (StreamReader sr = new StreamReader(pipeServer))
		        {
		            string temp;

                    // We would obviously not peg the CPU in production. This is just to illustrate waiting.
		            while (sr.ReadLine() != "SYNC");

		            while ((temp = sr.ReadLine()) != null)
		            {
		                Debug.WriteLine("Message from external application: " + temp);
		            }
		        }
            }

		    // I assume the reason you're doing this is so that each user has their own process?
                // If not, I would suggest placing the process into a static variable or, depending on the load
                if ((System.Diagnostics.Process)Session["moduleProcess"] == null)
			{
				Session["moduleProcess"] = p;
			}
			System.Diagnostics.Debug.WriteLine("ModulePortal StartModule() moduleCount = " + module.GetCount());
			this.countMsg = "Added Module: moduleCount = " + module.GetCount();
		}

		protected void AddModule(object sender, System.EventArgs e)
		{
			module.AddModule();
			System.Diagnostics.Debug.WriteLine("ModulePortal AddModule() moduleCount = " + module.GetCount());
			this.countMsg = "Added Module: moduleCount = " + module.GetCount();
		}
	}
}