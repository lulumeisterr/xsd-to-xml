using System.Xml.Schema;
using System.Xml;

namespace br.com.dev.xsd.Application.Domain.Services.Interface
{
    public interface IElementWriterStrategy
    {
        XmlWriter Write(XmlWriter writer, XmlSchemaElement element, XmlSchemaSet schemaSet);
    }
}
