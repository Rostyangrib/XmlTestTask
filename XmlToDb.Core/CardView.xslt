<?xml version="1.0" encoding="utf-8"?>
<!-- XSLT-преобразование: из xml с данными карточки делает html-таблицу. -->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

  <xsl:output method="html" encoding="utf-8" indent="yes" omit-xml-declaration="yes" />

  <xsl:template match="/Card">
    <div class="card">
      <h2>Карточка из файла: <xsl:value-of select="@FileName" /></h2>
      <table class="card-table">
        <tr><th>Имя сотрудника</th><td><xsl:value-of select="EmployeeName" /></td></tr>
        <tr><th>Должность</th><td><xsl:value-of select="Position" /></td></tr>
        <tr><th>Дата регистрации</th><td><xsl:value-of select="RegDate" /></td></tr>
        <tr><th>Тема</th><td><xsl:value-of select="Content" /></td></tr>
        <tr><th>Вид</th><td><xsl:value-of select="Kind" /></td></tr>
        <tr><th>Ссылка на карточку</th><td><xsl:value-of select="ReferenceList" /></td></tr>
        <tr><th>ИД автора</th><td><xsl:value-of select="AuthorId" /></td></tr>
      </table>
    </div>
  </xsl:template>

</xsl:stylesheet>
