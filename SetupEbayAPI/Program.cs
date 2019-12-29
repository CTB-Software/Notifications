using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SetupEbayAPI
{
    class Program
    {


        static Settings Setting = null;
        static void Main(string[] args)
        {
            if (File.Exists("Setting.db"))
            {
                Setting = Newtonsoft.Json.JsonConvert.DeserializeObject<Settings>(File.ReadAllText("Setting.db"));
            }
            else
            {
                Setting = new Settings();
                File.WriteAllText("Setting.db", Newtonsoft.Json.JsonConvert.SerializeObject(Setting));
            }


            if (args.Length == 0)
            {
                Console.WriteLine(System.AppDomain.CurrentDomain.FriendlyName + " -NewUserToken");
                Console.WriteLine("     Create New User Token");
                Console.WriteLine("");
                Console.WriteLine(System.AppDomain.CurrentDomain.FriendlyName + " -SetupUserAccount IsEnabled \"Email Address\" \"Web Notification Address\"");
                Console.WriteLine("   IsEnabled = 1 or 0");
                Console.WriteLine("     Setup a ebay user account");
                Console.WriteLine("     Make Sure you have the user token in \"UserToken.txt\"");
                Console.WriteLine("");
                Console.WriteLine(System.AppDomain.CurrentDomain.FriendlyName + " -TokenStatus");
                Console.WriteLine("     Make Sure you have the user token in \"UserToken.txt\"");


                return;
            }

            if (args[0].ToLower() == "-newusertoken")
            {

                using (HttpClient client = new HttpClient())
                {
                    var Data = TalkToEbay<Xml2CSharp.GetSessionIDResponse>(Setting.RuName);

                    System.Diagnostics.Process.Start("https://signin.ebay.com/ws/eBayISAPI.dll?SignIn&runame=" + Setting.RuName + "&SessID=" + Data.SessionID);

                    Console.WriteLine("Press Any Key To Continue (After Complating Ebay Login)");
                    Console.ReadLine();



                    var TokenResponse = TalkToEbay<Xml2CSharp.FetchTokenResponse>(Data.SessionID);

                    System.IO.File.WriteAllText("UserToken.txt", TokenResponse.EBayAuthToken);

                    Console.WriteLine("Token Info Saved To \"Token.txt\"");

                    return;

                }
            }

            else if (args[0].ToLower() == "-setupuseraccount")
            {
                if (args.Length != 4)
                {
                    Console.WriteLine(System.AppDomain.CurrentDomain.FriendlyName + " -SetupUserAccount IsEnabled \"Email Address\" \"Web Notification Address\"");
                    Console.WriteLine("     Setup a ebay user account");
                    Console.WriteLine("     Make Sure you have the user token in \"UserToken.txt\"");
                    return;
                }
                if (System.IO.File.Exists("UserToken.txt"))
                {

                    var Data = TalkToEbay<Xml2CSharp.SetNotificationPreferencesResponse>(System.IO.File.ReadAllText("UserToken.txt"), (args[1] == "1") ? "Enable" : "Disable", args[2], args[3]);

                    if (Data.Ack == "Failure")
                    {
                        Console.WriteLine("Failed To Setup Notifications");
                    }
                }
                return;
            }
            else if (args[0].ToLower() == "-tokenstatus")
            {
                if (System.IO.File.Exists("UserToken.txt"))
                {
                    var Data = TalkToEbay<Xml2CSharp.GetTokenStatusResponse>(System.IO.File.ReadAllText("UserToken.txt"));
                    Console.Clear();

                    Console.WriteLine($"Token Status: {Data.TokenStatus.Status}");
                    Console.WriteLine($"Expire Date: {Data.TokenStatus.ExpirationTime.ToLongDateString()}");
                    Console.WriteLine($"Expire Time: {Data.TokenStatus.ExpirationTime.ToLongTimeString()}");
                    Console.ReadLine();
                }
            }
           // Data.SessionID
            

        }

        public static T TalkToEbay<T>(params object[] args) where T : SetupEbayAPI.EbayAPI<T>
        {
            Console.WriteLine($"Call To Ebay (Start) with \"{EbayAPI<T>.CallName}\"");
            using (HttpClient client = new HttpClient())
            {
                var DataToSend = string.Format(File.ReadAllText(EbayAPI<T>.CallName + ".xml"), args);
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(Setting.EbayAPI),
                    Method = HttpMethod.Post,
                    Content = new StringContent(DataToSend, Encoding.UTF8, "application/xml")
                };
                request.Headers.Add("cache-control", "no-cache");
                request.Headers.Add("X-EBAY-API-SITEID", "3");
                request.Headers.Add("X-EBAY-API-COMPATIBILITY-LEVEL", "967");
                request.Headers.Add("X-EBAY-API-CALL-NAME", EbayAPI<T>.CallName);
                request.Headers.Add("X-EBAY-API-APP-NAME", Setting.AppID);
                request.Headers.Add("X-EBAY-API-DEV-NAME", Setting.DevID);
                request.Headers.Add("X-EBAY-API-CERT-NAME", Setting.CartID);
                var a = client.SendAsync(request);
                Console.WriteLine("Sent xml to Ebay waiting for response");

                a.Wait();
                Console.WriteLine("Converting xml");
                var b = a.Result.Content.ReadAsStringAsync();
                b.Wait();
                Console.WriteLine("Call To Ebay (Complate)");
                Console.WriteLine();
                var XML = b.Result;
                return SetupEbayAPI.EbayAPI<T>.Deserialize(XML);
            }
            
        }
       
    }
}
