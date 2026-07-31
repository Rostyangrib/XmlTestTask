// Читает из xml-файла карточки нужные атрибуты и собирает из них небольшой xml-документ.

using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace XmlToDb.Core
{
    public static class CardXmlParser
    {
        public static XDocument Parse(byte[] fileContent, string fileName)
        {
            XDocument source;
            using (var stream = new MemoryStream(fileContent))
                source = XDocument.Load(stream); // определяется кодировка 

            var card = source.Root?.Element("CardDocument");
            if (card == null)
                throw new InvalidOperationException("В файле не найден узел Data/CardDocument.");

            var main = card.Element("MainInfo");
            var system = card.Element("System");
            var performer = card.Element("Performers")?.Element("PerformersRow");

            return new XDocument(new XElement("Card",
                new XAttribute("FileName", fileName),
                new XElement("EmployeeName", Attr(main, "FirstName")),
                new XElement("Position", FindPosition(source, Attr(performer, "Performer"))),
                new XElement("RegDate", FormatDate(Attr(main, "RegDate"))),
                new XElement("Content", Attr(main, "Content")),
                new XElement("Kind", Attr(system, "Kind_Name")),
                new XElement("ReferenceList", Attr(main, "ReferenceList")),
                new XElement("AuthorId", Attr(main, "Author"))));
        }

        // Performer хранит идентификатор записи справочника, поэтому должность ищется
        // в этом же файле по совпадению RowID.
        private static string FindPosition(XDocument source, string performerId)
        {
            if (string.IsNullOrEmpty(performerId))
                return string.Empty;

            var employee = source.Descendants().FirstOrDefault(
                e => e.Attribute("PositionName") != null &&
                     string.Equals(Attr(e, "RowID"), performerId, StringComparison.OrdinalIgnoreCase));

            return Attr(employee, "PositionName");
        }

        private static string Attr(XElement element, string name)
        {
            var attribute = element?.Attribute(name);
            return attribute == null ? string.Empty : attribute.Value;
        }

        private static string FormatDate(string value)
        {
            return DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date)
                ? date.ToString("dd.MM.yyyy HH:mm:ss")
                : value;
        }
    }
}
