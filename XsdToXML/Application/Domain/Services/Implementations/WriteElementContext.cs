using br.com.dev.xsd.Application.Domain.Model;
using br.com.dev.xsd.Application.Domain.Services.Interface;
using System.Xml;
using System.Xml.Schema;

namespace br.com.dev.xsd.Application.Domain.Services.Implementations
{
    /// <summary>
    /// Classe responsavel por implementar o padrao strategy para definir a leitura dos objetos do XSD
    /// </summary>
    public class WriteElementContext
    {
        protected IElementWriterStrategy _strategy;
        public WriteElementContext()
        {
        }
        public WriteElementContext(IElementWriterStrategy strategy)
        {
            _strategy = strategy;
        }
        /// <summary>
        /// Escrevendo os objetos por tipo de complexidade.
        /// </summary>
        /// <param name="writer">Escritor</param>
        /// <param name="element">Element XSD</param>
        /// <param name="schemaSet">Schema</param>
        public XmlWriter WriteElement(XmlWriter writer, XmlSchemaElement element, XmlSchemaSet schemaSet)
        {
            if (element == null || writer == null || schemaSet == null)
                throw new ArgumentNullException("Um ou mais parâmetros são nulos.");
            if (element.SchemaTypeName == null || element.SchemaTypeName.IsEmpty)
            {
                if (element.ElementSchemaType is XmlSchemaComplexType complexType)
                {
                    _strategy = new ComplexTypeWriterStrategy();
                    _strategy.Write(writer, element, schemaSet);
                }
                else if (element.ElementSchemaType is XmlSchemaSimpleType simpleType)
                {
                    _strategy = new SimpleSchemaTypeStrategy();
                    _strategy.Write(writer, element, schemaSet);
                }
                else
                {
                    writer.WriteStartElement(element.QualifiedName.Name, element.QualifiedName.Namespace);
                    writer.WriteEndElement();
                }
                return writer;
            }

            XmlSchemaType? schemaType = schemaSet.GlobalTypes[element?.SchemaTypeName] as XmlSchemaType;
            switch (schemaType)
            {
                case XmlSchemaSimpleType:
                    _strategy = new SimpleElementTypeWriterStrategy();
                    _strategy.Write(writer, element, schemaSet);
                    break;
                case XmlSchemaComplexType:
                    _strategy = new ComplexTypeWriterStrategy();
                    _strategy.Write(writer, element, schemaSet);
                    break;
            }
            return writer;
        }
    }
}