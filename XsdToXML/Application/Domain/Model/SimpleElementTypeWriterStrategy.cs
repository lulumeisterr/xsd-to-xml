using br.com.dev.xsd.Application.Domain.Services.Interface;
using System.Xml;
using System.Xml.Schema;

namespace br.com.dev.xsd.Application.Domain.Model
{
    public class SimpleElementTypeWriterStrategy : IElementWriterStrategy
    {
        public XmlWriter Write(XmlWriter writer, XmlSchemaElement element, XmlSchemaSet schemaSet)
        {
            writer.WriteStartElement(element.QualifiedName.Name, element.QualifiedName.Namespace);
            writer.WriteString("exampleValue");
            writer.WriteEndElement();
            return writer;
        }
    }
}
