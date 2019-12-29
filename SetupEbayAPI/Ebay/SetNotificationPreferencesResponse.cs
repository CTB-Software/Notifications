/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace Xml2CSharp
{
	[XmlRoot(ElementName = "SetNotificationPreferencesResponse", Namespace = "urn:ebay:apis:eBLBaseComponents")]
	public class SetNotificationPreferencesResponse : SetupEbayAPI.EbayAPI<SetNotificationPreferencesResponse>
	{
		[XmlElement(ElementName = "Timestamp", Namespace = "urn:ebay:apis:eBLBaseComponents")]
		public string Timestamp { get; set; }
		[XmlElement(ElementName = "Ack", Namespace = "urn:ebay:apis:eBLBaseComponents")]
		public string Ack { get; set; }
		[XmlElement(ElementName = "Version", Namespace = "urn:ebay:apis:eBLBaseComponents")]
		public string Version { get; set; }
		[XmlElement(ElementName = "Build", Namespace = "urn:ebay:apis:eBLBaseComponents")]
		public string Build { get; set; }
		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }
	}

}
