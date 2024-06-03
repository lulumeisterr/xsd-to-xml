using br.com.dev.xsd.Application.Domain.Services.Implementations;
using br.com.dev.xsd.Application.Domain.Services.Interface;
using System.Xml;
using System.Xml.Schema;

namespace br.com.dev.xsd.Application.Domain.Model
{
    public class ComplexTypeWriterStrategy : WriteElementContext, IElementWriterStrategy
    {
        /// <summary>
        /// Escreve o elemento XML com base no tipo de esquema complexo fornecido.
        /// </summary>
        /// <param name="writer">O escritor XML.</param>
        /// <param name="element">O elemento de esquema XML.</param>
        /// <param name="schemaSet">O conjunto de esquemas XML.</param>
        /// <example>
        /// O seguinte código demonstra como usar o método Write para escrever um elemento XML:
        /// <code>
        /// var writer = new XmlTextWriter("output.xml", Encoding.UTF8);
        /// var schemaSet = new XmlSchemaSet();
        /// schemaSet.Add("http://example.com", "schema.xsd");
        /// var element = new XmlSchemaElement(new XmlQualifiedName("elementName", "namespace"));
        /// var strategy = new ComplexTypeWriterStrategy();
        /// strategy.Write(writer, element, schemaSet);
        /// writer.Close();
        /// </code>
        /// </example>
        public XmlWriter Write(XmlWriter writer, XmlSchemaElement element, XmlSchemaSet schemaSet)
        {
            XmlSchemaType schemaType = schemaSet.GlobalTypes[element.SchemaTypeName] as XmlSchemaType;
            if (schemaType is XmlSchemaComplexType complexType)
                WriteComplexType(writer, complexType, schemaSet, element.QualifiedName.Name, element.QualifiedName.Namespace);
            return writer;
        }

        public ComplexTypeWriterStrategy()
        {
            _strategy = this;
        }

        /// <summary>
        /// Escreve um tipo de esquema complexo no XML.
        /// </summary>
        /// <param name="writer">O escritor XML.</param>
        /// <param name="complexType">O tipo de esquema complexo.</param>
        /// <param name="schemaSet">O conjunto de esquemas XML.</param>
        private void WriteComplexType(XmlWriter writer, XmlSchemaComplexType complexType, XmlSchemaSet schemaSet, string elementName, string ns)
        {
            writer.WriteStartElement(elementName, ns);
            WriteParticle(writer, complexType.Particle, schemaSet);
            writer.WriteEndElement();
        }
        /// <summary>
        /// Escreve a partícula do esquema no XML.
        /// </summary>
        /// <param name="writer">O escritor XML.</param>
        /// <param name="particle">A partícula do esquema XML.</param>
        /// <param name="schemaSet">O conjunto de esquemas XML.</param>
        private void WriteParticle(XmlWriter writer, XmlSchemaParticle particle, XmlSchemaSet schemaSet)
        {
            if (particle is XmlSchemaSequence sequence)
            {
                WriteSequence(writer, sequence, schemaSet);
            }
            else if (particle is XmlSchemaChoice choice)
            {
                WriteChoice(writer, choice, schemaSet);
            }
        }
        /// <summary>
        /// Escreve uma sequência de elementos XML no XML.
        /// </summary>
        /// <param name="writer">O escritor XML.</param>
        /// <param name="sequence">A sequência de elementos XML.</param>
        /// <param name="schemaSet">O conjunto de esquemas XML.</param>
        private void WriteSequence(XmlWriter writer, XmlSchemaSequence sequence, XmlSchemaSet schemaSet)
        {
            foreach (XmlSchemaObject item in sequence.Items)
            {
                if (item is XmlSchemaElement subElement)
                {
                    WriteElement(writer, subElement, schemaSet);
                }
            }
        }
        /// <summary>
        /// Escreve uma escolha de elementos XML no XML.
        /// </summary>
        /// <param name="writer">O escritor XML.</param>
        /// <param name="choice">A escolha de elementos XML.</param>
        /// <param name="schemaSet">O conjunto de esquemas XML.</param>
        private void WriteChoice(XmlWriter writer, XmlSchemaChoice choice, XmlSchemaSet schemaSet)
        {
            XmlSchemaObject firstChoiceItem = choice.Items[0];
            if (firstChoiceItem is XmlSchemaElement choiceElement)
            {
                WriteElement(writer, choiceElement, schemaSet);
            }
        }
    }
}
