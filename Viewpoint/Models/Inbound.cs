using DotLiquid;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System;
using System.IO;
using System.Text;

namespace Viewpoint.Models
{
	[XmlRoot(ElementName = "Vehicle")]
	public class Vehicle
	{
		[XmlElement(ElementName = "Year")]
		public int Year { get; set; }
		[XmlElement(ElementName = "Make")]
		public string Make { get; set; }
		[XmlElement(ElementName = "Model")]
		public string Model { get; set; }
	}

	[XmlRoot(ElementName = "Vehicles")]
	public class Vehicles
	{
		[XmlElement(ElementName = "Vehicle")]
		public List<Vehicle> Vehicle { get; set; }
	}

	[XmlRoot(ElementName = "LoanInfo")]
	public class LoanInfo
	{
		[XmlAttribute(AttributeName = "loan_type")]
		public string loan_type { get; set; }
		[XmlAttribute(AttributeName = "program")]
		public string program { get; set; }
		[XmlAttribute(AttributeName = "percentAmount")]
		public string percentAmount { get; set; }
		[XmlAttribute(AttributeName = "flatAmount")]
		public string flatAmount { get; set; }
	}

	[XmlRoot(ElementName = "Loans")]
	public class Loans
	{
		[XmlElement(ElementName = "LoanInfo")]
		public List<LoanInfo> LoanInfo { get; set; }
	}

	[XmlRoot(ElementName = "fin")]
	public class Fin
	{
		[XmlElement(ElementName = "mth_st_dt")]
		public string mth_st_dt { get; set; }
		[XmlElement(ElementName = "mth_pay_amt")]
		public string mth_pay_amt { get; set; }
		[XmlElement(ElementName = "term")]
		public string term { get; set; }
		[XmlAttribute(AttributeName = "total")]
		public string total { get; set; }
	}

	[XmlRoot(ElementName = "Case")]
	public class Case
	{
		[XmlElement(ElementName = "Vehicles")]
		public Vehicles Vehicles { get; set; }
		[XmlElement(ElementName = "Loans")]
		public Loans Loans { get; set; }
		[XmlElement(ElementName = "fin")]
		public Fin fin { get; set; }
	}

	[XmlRoot(ElementName = "Inbound")]
	public class Inbound
	{
		public Inbound()
		{
			transmissionDateTime = "2020-04-20T11:19:37-06:00";
			//xsi = "http://www.w3.org/2001/XMLSchema-instance";
			fn = "http://www.w3.org/TR/xpath-functions/";
			schemaVersion = "1.0";

		}

		[XmlElement(ElementName = "Case")]
		public Case Case { get; set; }
		[XmlAttribute(AttributeName = "sourceCode")]
		public string sourceCode { get; set; }
		[XmlAttribute(AttributeName = "transmissionDateTime")]
		public string transmissionDateTime { get; set; }
		[XmlAttribute(AttributeName = "metaDataFilePath")]
		public string metaDataFilePath { get; set; }
		[XmlAttribute(AttributeName = "sourceBatchID")]
		public string sourceBatchID { get; set; }
		[XmlAttribute(AttributeName = "applicationNumber")]
		public string applicationNumber { get; set; }
		[XmlAttribute(AttributeName = "ssn")]
		public string ssn { get; set; }
		[XmlAttribute(AttributeName = "pageCount")]
		public string pageCount { get; set; }
		[XmlAttribute(AttributeName = "contractType")]
		public string contractType { get; set; }
		[XmlAttribute(AttributeName = "schemaVersion")]
		public string schemaVersion { get; set; }
		//[XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
		//public string xsi { get; set; }
		[XmlAttribute(AttributeName = "fn", Namespace = "http://www.w3.org/2000/xmlns/")]
		public string fn { get; set; }

		public string ToXML()
		{
			System.Xml.Serialization.XmlSerializer xml = new XmlSerializer(this.GetType());
			StringWriterUtf8 text = new StringWriterUtf8();
			xml.Serialize(text, this);
			return text.ToString();
		}
	}

	public class StringWriterUtf8 : System.IO.StringWriter
	{
		public override Encoding Encoding
		{
			get
			{
				return Encoding.UTF8;
			}
		}
	}

}
