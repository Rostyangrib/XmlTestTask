// Применяет XSLT-преобразование к данным карточки и возвращает готовый html.

using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;

namespace XmlToDb.Core
{
    public static class CardRenderer
    {
        private static XslCompiledTransform _transform;

        public static string RenderToHtml(XDocument card)
        {
            if (_transform == null)
            {
                var stream = typeof(CardRenderer).GetTypeInfo().Assembly
                    .GetManifestResourceStream("XmlToDb.Core.CardView.xslt");

                _transform = new XslCompiledTransform();
                _transform.Load(XmlReader.Create(stream));
            }

            using (var writer = new StringWriter())
            using (var reader = card.CreateReader())
            {
                _transform.Transform(reader, null, writer);
                return writer.ToString();
            }
        }
    }
}
