namespace br.com.dev.xsd.Application.Services.XsdServices.Interface
{
    public interface IReadXSDService
    {
        Task<string> ReadXSD(string xsdText);
    }
}