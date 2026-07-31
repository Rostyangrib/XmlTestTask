// Оборачивает результат XSLT-преобразования в html-страницу и сохраняет её
// во временный файл, который показывает WebBrowser на форме.

using System.IO;
using System.Text;
using System.Xml.Linq;
using XmlToDb.Core;

namespace XmlToDb
{
    internal static class CardPage
    {
        private const string Head =
            "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"><style>" +
            "body { font-family: Segoe UI, sans-serif; font-size: 13px; margin: 12px; }" +
            "h2 { font-size: 15px; }" +
            "table { border-collapse: collapse; width: 100%; }" +
            "th, td { border: 1px solid #c8c8c8; padding: 6px 8px; text-align: left; }" +
            "th { width: 210px; background: #f0f3f7; font-weight: normal; }" +
            "</style></head><body>";

        public static string Save(XDocument card, long documentId)
        {
            var folder = Path.Combine(Path.GetTempPath(), "XmlToDb");
            Directory.CreateDirectory(folder);

            var filePath = Path.Combine(folder, "card_" + documentId + ".html");
            File.WriteAllText(filePath, Head + CardRenderer.RenderToHtml(card) + "</body></html>", Encoding.UTF8);

            return filePath;
        }
    }
}
