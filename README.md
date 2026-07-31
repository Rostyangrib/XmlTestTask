# XmlToDb - тестовое задание


| Проект / папка  | Что внутри                                                      |
| --------------- | --------------------------------------------------------------- |
| `XmlToDb.Core/` | Разбор xml и XSLT-преобразование — общая часть обоих приложений |
| `XmlToDb/`      | Десктопное приложение WinForms (задание 1)                      |
| `XmlToDb.Web/`  | Веб-приложение ASP.NET Core MVC (задание на Web)                |
| `Setup/`        | Скрипт WiX и собранный `XmlToDb.msi` (задание 2)                |
| `Sql/`          | Скрипт `UpdateXmlTagValue.sql` (задание 3)                      |


## Какие атрибуты читаются


| Поле               | Путь в xml                                              |
| ------------------ | ------------------------------------------------------- |
| Имя сотрудника     | `Data/CardDocument/MainInfo/@FirstName`                 |
| Должность          | `Data/CardDocument/Performers/PerformersRow/@Performer` |
| Дата регистрации   | `Data/CardDocument/MainInfo/@RegDate`                   |
| Тема               | `Data/CardDocument/MainInfo/@Content`                   |
| Вид                | `Data/CardDocument/System/@Kind_Name`                   |
| Ссылка на карточку | `Data/CardDocument/MainInfo/@ReferenceList`             |
| ИД автора          | `Data/CardDocument/MainInfo/@Author`                    |


## Десктопное приложение

Главное окно приложения

```
dotnet build XmlToDb\XmlToDb.csproj -c Release
XmlToDb\bin\Release\net472\XmlToDb.exe
```

База создаётся при первом запуске в `%LOCALAPPDATA%\XmlToDb\XmlToDb.db`.

## Веб-приложение

Страница веб-приложения

```
dotnet run --project XmlToDb.Web
```

Адрес - [http://localhost:5080](http://localhost:5080)

## Msi-пакет  
Пересобрать:

```
dotnet tool install --global wix
Setup\build-msi.cmd
```

## Sql-скрипт

`Sql\UpdateXmlTagValue.sql` - процедура для Microsoft SQL Server с четырьмя параметрами: путь к тегу, тип данных, новое значение и идентификатор строки.

```sql
EXEC dbo.UpdateXmlTagValue N'/Data/CardDocument/MainInfo/@Content', N'nvarchar', N'новая тема', 1;
```

