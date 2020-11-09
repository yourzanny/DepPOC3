using MyHelperLib;
using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DepPOC3
{
    public class MySvcManager
    {
        static readonly string servicePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")) + @"DemoServicePub\DemoService.exe";
        const string serivceName = "MyDemoSvcServiceName";

        public bool IsServiceInstalled() => ServiceController.GetServices().Any(x => x.ServiceName.Equals(serivceName));

        public void InstallService()
        {
            try
            {
                //requires service installer to install - case failed
                //ManagedInstallerClass.InstallHelper(new string[] { servicePath });

                //Problem: UAC window
                //ClickOnce doesnt support manifest file's:
                //< requestedExecutionLevel  level = "requireAdministrator" uiAccess = "false" />
                //< requestedExecutionLevel  level = "highestAvailable" uiAccess = "false" />
                //only supports:
                //< requestedExecutionLevel  level = "asInvoker" uiAccess = "false" />

                //UserShellExecute = true
                //UseShellExecute is false because we're specifying an executable directly and in this case depending on it being in a PATH folder
                //When you use the operating system shell to start processes, you can start any document(which is any registered file type associated with an executable that has a default open action) and perform operations on the file, such as printing, by using the Process object.When UseShellExecute is false, you can start only executables by using the Process object.
                //p.StartInfo.UseShellExecute = true;
                //p.StartInfo.FileName = "www.google.co.uk";

                Process processTemp = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = "/c " + MyExtensionMethods.Quote(servicePath) +" install",
                        //RedirectStandardOutput = true,
                        //RedirectStandardError = true,
                        UseShellExecute = true,
                        CreateNoWindow = true,
                        Verb = "runas",
                        WindowStyle = ProcessWindowStyle.Hidden
                    }
                };
                processTemp.Start();
                //string result = processTemp.StandardOutput.ReadToEnd();

                //// Display the command output.
                //DepPOC3HelperMethods.M(result);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
