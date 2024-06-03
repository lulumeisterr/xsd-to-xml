using br.com.dev.xsd.Application.Domain.Services.Implementations;
using br.com.dev.xsd.Application.Services.XsdServices.Interface;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
namespace br.com.dev.xsd.Services.XsdServices.Implementations
{
    public class ReadXSDService : IReadXSDService
    {
        private XmlWriterSettings _settings;

        public ReadXSDService()
        {
            _settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "\t",
                NewLineOnAttributes = false,
                Encoding = Encoding.UTF8,
                Async = true
            };
        }

        /// <summary>
        /// Lê o texto do XSD fornecido, compila-o e gera um XML correspondente com base no esquema XML fornecido.
        /// </summary>
        /// <param name="xsdText">O texto do XSD a ser lido.</param>
        /// <returns>Uma tarefa que representa a operação assíncrona. O resultado da tarefa é o XML gerado com base no XSD.</returns>
        public async Task<string> ReadXSD(string xsdText)
        {
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            using (StringReader stringReader = new StringReader(xsdText))
            {
                XmlSchema schema = await Task.Run(() => XmlSchema.Read(stringReader, (sender, args) => { throw args.Exception; }));
                schemaSet.Add(schema);
            }
            schemaSet.Compile();

            StringBuilder xmlOutput = new StringBuilder();
            using (StringWriter stringWriter = new StringWriter(xmlOutput))
            using (XmlWriter writer = XmlWriter.Create(stringWriter, _settings))
            {
                WriteElementContext writeElementContext = new WriteElementContext();
                writer.WriteStartDocument();
                foreach (XmlSchema schema in schemaSet.Schemas())
                    foreach (XmlSchemaElement element in schema.Elements.Values)
                        writeElementContext.WriteElement(writer, element, schemaSet);
                writer.WriteEndDocument();
            }
            return xmlOutput.ToString();
        }
    }
}

