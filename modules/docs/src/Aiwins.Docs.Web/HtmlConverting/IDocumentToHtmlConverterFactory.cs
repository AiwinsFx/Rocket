namespace Aiwins.Docs.HtmlConverting
{
    public interface IDocumentToHtmlConverterFactory
    {
        IDocumentToHtmlConverter Create(string format);
    }
}