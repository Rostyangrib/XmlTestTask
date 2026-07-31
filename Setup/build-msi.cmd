@echo off

rem Переход в корень. Сборка приложения. Сборка msi-пакета.
cd /d "%~dp0.."
dotnet build XmlToDb\XmlToDb.csproj -c Release
wix build Setup\Product.wxs -d BinDir="%CD%\XmlToDb\bin\Release\net472" -o Setup\XmlToDb.msi
