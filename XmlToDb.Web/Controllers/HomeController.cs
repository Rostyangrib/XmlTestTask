// Контроллер: загрузка xml-файлов в базу, список файлов и выдача карточки в виде html.

using System.Text;
using Microsoft.AspNetCore.Mvc;
using XmlToDb.Core;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View(Database.GetDocuments());
    }

    [HttpPost]
    public IActionResult Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            TempData["Error"] = "Выберите xml-файл.";
            return RedirectToAction("Index");
        }

        try
        {
            using var stream = new MemoryStream();
            file.CopyTo(stream);
            var content = stream.ToArray();

            CardXmlParser.Parse(content, file.FileName); // разбор до сохранения
            Database.AddDocument(Path.GetFileName(file.FileName), content);
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Не удалось загрузить файл: " + ex.Message;
        }

        return RedirectToAction("Index");
    }

    // Возвращает html карточки; страница подгружает его скриптом без перезагрузки.
    public IActionResult Card(long id)
    {
        var (fileName, content) = Database.GetDocument(id);
        if (content == null)
            return NotFound();

        var card = CardXmlParser.Parse(content, fileName);
        return Content(CardRenderer.RenderToHtml(card), "text/html", Encoding.UTF8);
    }
}
