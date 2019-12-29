using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace EbayNotification
{
    class Program
    {
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        private static Settings Setting { get; set; }

        static void Main(string[] args)
        {
            var Setting = new Settings();
            //ShortCutCreator.TryCreateShortcut("ConsoleToast.App", "ConsoleToast");
            if (File.Exists("Setting.db"))
            {
                Setting = Newtonsoft.Json.JsonConvert.DeserializeObject<Settings>(File.ReadAllText("Setting.db"));
            }
            else
            {
                Setting = new Settings();
                File.WriteAllText("Setting.db", Newtonsoft.Json.JsonConvert.SerializeObject(Setting));
            }

            try
            {
                FileVersionInfo OrgDLL = null;
                if (File.Exists(@"Notification.dll"))
                {
                    OrgDLL = FileVersionInfo.GetVersionInfo(@"Notification.dll");
                }

                FileVersionInfo UpdateDLL = FileVersionInfo.GetVersionInfo(Setting.UpdateDir+ "Notification.dll");

                if (OrgDLL == null || OrgDLL.ProductVersion != UpdateDLL.ProductVersion)
                {
                    Console.WriteLine("Updating App");
                    foreach (var item in Directory.GetFiles(Setting.UpdateDir))
                    {
                        if (!item.Contains(".pdb"))
                        {
                            File.Copy(item, item.Substring(Setting.UpdateDir.Length), true);
                        }
                    }


                }
            }
            catch (Exception E)
            {
                Console.WriteLine("Failed To Check For Updates: "+E.Message);
            }

            //myFileVersionInfo.FileVersion 

            var DLL = Assembly.LoadFile(AssemblyDirectory + @"\Notification.dll");
            var theType = DLL.GetType("Ebay.Notification");
            var method = theType.GetMethod("Start");
            method.Invoke(null, new object[] { Setting.Port });
            //StartListener();
            Console.Read();
        }


        
    }

}