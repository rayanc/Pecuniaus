using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Pecuniaus.Utilities
{
    public class XsltTransform
    {
        public string Transform(object data, string xslFileName)
        {
            XmlSerializer xs = new XmlSerializer(data.GetType());
            string xmlString;
            using (StringWriter swr = new StringWriter())
            {
                xs.Serialize(swr, data);
                xmlString = swr.ToString();
            }

            var xd = new XmlDocument();
            xd.LoadXml(xmlString);

            var xslt = new System.Xml.Xsl.XslCompiledTransform();
            xslt.Load(xslFileName);
            var stm = new MemoryStream();
            xslt.Transform(xd, null, stm);
            stm.Position = 1;
            var sr = new StreamReader(stm);
            //xtr.Close();
            return sr.ReadToEnd();
        }
    }
}
