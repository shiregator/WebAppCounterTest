using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
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
                StartInfo = { FileName = @"D:\depthinfinity\shiregator\WebAppCounterTest\ModuleExe\bin\Debug\ModuleExe.exe" }
            };
            p.Start();

            bool mappedFileExists = false;

            while (!mappedFileExists)
            {
                try
                {
                    using (var mmf = MemoryMappedFile.OpenExisting("ImgA"))
                    {
                        mappedFileExists = true;

                        using (var accessor = mmf.CreateViewAccessor(1, 16))
                        {
                            VegasObject color;

                            accessor.Read(0, out color);

                            Debug.WriteLine(color.Count);
                        }
                    }

                }
                catch (FileNotFoundException)
                {
                    // File is not ready yet
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