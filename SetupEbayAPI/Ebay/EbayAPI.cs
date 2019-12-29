using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SetupEbayAPI
{
    public class EbayAPI<T>
    {
		public static string CallName
		{
			get
			{
				var Name = typeof(T).Name.ToString();
				return Name.Substring(0, Name.Length - 8);
			}
		}
		public static T Deserialize(string Data)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			using (TextReader reader = new StringReader(Data))
			{
				return (T)serializer.Deserialize(reader);
			}
		}
	}
}
