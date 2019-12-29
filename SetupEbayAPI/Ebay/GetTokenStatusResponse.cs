/* 
 Licensed under the Apache License, Version 2.0

 http://www.apache.org/licenses/LICENSE-2.0
 */
using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace Xml2CSharp
{
	[XmlRoot(ElementName = "TokenStatus", Namespace = "urn:ebay:apis:eBLBaseComponents")]
	public class TokenStatus
	{
		[XmlElement(ElementName = "Status", Namespace = "urn:ebay:apis:eBLBaseComponents")]
		public string Status { get; set; }
		[XmlElement(ElementName = "EIASToken", Namespace = "urn:ebay:apis:eBLBaseComponents")]
		public string EIASToken { get; set; }
		[XmlElement(ElementName = "ExpirationTime", Namespace = "urn:ebay:apis:eBLBaseComponents")]
		public DateTime ExpirationTime { get; set; }
	}

	[XmlRoot(ElementName = "GetTokenStatusResponse", Namespace = "urn:ebay:apis:eBLBaseComponents")]
	public class GetTokenStatusResponse : SetupEbayAPI.EbayAPI<GetTokenStatusResponse>
	{
		[XmlElement(ElementName = "Timestamp", Namespace = "urn:ebay:apis:eBLBaseComponents")]
		public string Timestamp { get; set; }
		[XmlElement(ElementName = "Ack", Namespace = "urn:ebay:apis:eBLBaseComponents")]
		public string Ack { get; set; }
		[XmlElement(ElementName = "Version", Namespace = "urn:ebay:apis:eBLBaseComponents")]
		public string Version { get; set; }
		[XmlElement(ElementName = "Build", Namespace = "urn:ebay:apis:eBLBaseComponents")]
		public string Build { get; set; }
		[XmlElement(ElementName = "TokenStatus", Namespace = "urn:ebay:apis:eBLBaseComponents")]
		public TokenStatus TokenStatus { get; set; }
		[XmlAttribute(AttributeName = "xmlns")]
		public string Xmlns { get; set; }
	}

}
