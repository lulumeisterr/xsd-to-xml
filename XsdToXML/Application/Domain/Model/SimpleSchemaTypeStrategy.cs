using br.com.dev.xsd.Application.Domain.Services.Interface;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;

namespace br.com.dev.xsd.Application.Domain.Model
{
    public class SimpleSchemaTypeStrategy : IElementWriterStrategy
    {
        public XmlWriter Write(XmlWriter writer, XmlSchemaElement element, XmlSchemaSet schemaSet)
        {
            writer.WriteStartElement(element.QualifiedName.Name, element.QualifiedName.Namespace);
            XmlSchemaSimpleType? simpleType = element.ElementSchemaType as XmlSchemaSimpleType;
            writer.WriteString(GenerateExampleValue(simpleType, writer));
            writer.WriteEndElement();
            return writer;
        }

        private string GenerateExampleValue(XmlSchemaSimpleType simpleType, XmlWriter writer)
        {
            var restriction = simpleType.Content as XmlSchemaSimpleTypeRestriction;
            if (restriction != null)
            {
                int? minLength = null;
                int? maxLength = null;
                List<string> patterns = new List<string>();
                List<string> enumerations = new List<string>();

                foreach (XmlSchemaFacet facet in restriction.Facets)
                {
                    if (facet is XmlSchemaMaxLengthFacet maxLengthFacet)
                    {
                        maxLength = int.Parse(maxLengthFacet.Value);
                    }
                    else if (facet is XmlSchemaMinLengthFacet minLengthFacet)
                    {
                        minLength = int.Parse(minLengthFacet.Value);
                    }
                    else if (facet is XmlSchemaPatternFacet patternFacet)
                    {
                        patterns.Add(patternFacet.Value);
                    }
                    else if (facet is XmlSchemaEnumerationFacet enumerationFacet)
                    {
                        enumerations.Add(enumerationFacet.Value);
                    }
                }

                if (enumerations.Count > 0)
                {
                    foreach (var value in enumerations)
                    {
                        writer.WriteStartElement("Value");
                        writer.WriteString(value);
                        writer.WriteEndElement();
                    }
                    return string.Empty;
                }

                if (patterns.Count > 0)
                {
                    // Gerar valor que satisfaça o primeiro padrão dinamicamente
                    return GeneratePatternValue(patterns, minLength, maxLength);
                }

                int length = minLength ?? 1;
                if (maxLength.HasValue)
                {
                    length = Math.Min(length, maxLength.Value);
                }
                return new string('X', length);
            }
            return "exampleValue";
        }
        static string GeneratePatternValue(List<string> patterns, int? minLength, int? maxLength)
        {
            // Implementação simples para gerar uma string que corresponda ao padrão dinamicamente
            // Para uma implementação completa, é necessário usar uma biblioteca de geração de strings a partir de regex
            // Aqui geramos uma string simples que se ajusta ao comprimento especificado e ao padrão

            int length = minLength ?? 1; // Define um comprimento padrão de 1 se minLength não for especificado
            if (maxLength.HasValue)
            {
                length = Math.Min(length, maxLength.Value);
            }

            // Gerar string baseada no primeiro padrão
            foreach (var pattern in patterns)
            {
                if (Regex.IsMatch(new string('1', length), pattern))
                {
                    return new string('1', length); // Exemplo para dígitos
                }
                if (Regex.IsMatch(new string('a', length), pattern))
                {
                    return new string('a', length); // Exemplo para letras
                }

                // Adicione mais lógica para outros tipos de padrões conforme necessário
            }
            // Se não houver correspondência, retornar um valor padrão que satisfaça minLength e maxLength
            return new string('X', length);
        }
    }
}
