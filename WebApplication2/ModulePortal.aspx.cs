using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
			System.Diagnostics.Process p = System.Diagnostics.Process.Start("C:\\Utilities\\Module.exe");
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