CREATE OR ALTER PROCEDURE dbo.UpdateXmlTagValue
    @TagPath  nvarchar(500),   -- путь к тегу
    @DataType nvarchar(50),    -- тип данных нового значения
    @NewValue nvarchar(4000),  -- новое значение
    @RowId    int              -- идентификатор строки таблицы
AS
BEGIN
    -- Путь подставляется в запрос текстом.
    IF PATINDEX('%[^a-zA-Z0-9_/@:.-]%', @TagPath) > 0 THROW 50001, 'Недопустимый путь к тегу.', 1;

    -- Путь вида '.../@Name' указывает на атрибут, остальные - на текст внутри элемента.
    DECLARE @Target nvarchar(600) =
        '(' + @TagPath + CASE WHEN CHARINDEX('@', @TagPath) > 0 THEN ')[1]' ELSE '/text())[1]' END;

    DECLARE @Type nvarchar(30) =
        CASE LOWER(@DataType) WHEN 'int' THEN 'xs:integer' WHEN 'datetime' THEN 'xs:dateTime' ELSE 'xs:string' END;

    DECLARE @Sql nvarchar(max) =
        N'UPDATE dbo.XmlDocuments
          SET Content.modify(''replace value of ' + @Target +
              N' with (sql:variable("@NewValue") cast as ' + @Type + N'?)'')
          WHERE Id = @RowId;';

    EXEC sp_executesql @Sql, N'@NewValue nvarchar(4000), @RowId int', @NewValue, @RowId;
END;
