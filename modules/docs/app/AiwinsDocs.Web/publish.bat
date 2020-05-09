@echo off

dotnet clean
dotnet restore
dotnet build

DEL /F/Q/S "C:\Publishes\AiwinsDocs" > NUL && RMDIR /Q/S "C:\Publishes\AiwinsDocs"

dotnet publish -c Release -r win-x64   --self-contained true -o "C:\Publishes\AiwinsDocs\win-x64\Web"
dotnet publish -c Release -r win-x86   --self-contained true -o "C:\Publishes\AiwinsDocs\win-x86\Web"
dotnet publish -c Release -r osx-x64   --self-contained true -o "C:\Publishes\AiwinsDocs\osx-x64\Web"
dotnet publish -c Release -r linux-x64 --self-contained true -o "C:\Publishes\AiwinsDocs\linux-x64\Web"

cd..\AiwinsDocs.Migrator 

dotnet publish -c Release -r win-x64   --self-contained true -o "C:\Publishes\AiwinsDocs\win-x64\Migrator"
dotnet publish -c Release -r win-x86   --self-contained true -o "C:\Publishes\AiwinsDocs\win-x86\Migrator"
dotnet publish -c Release -r osx-x64   --self-contained true -o "C:\Publishes\AiwinsDocs\osx-x64\Migrator"
dotnet publish -c Release -r linux-x64 --self-contained true -o "C:\Publishes\AiwinsDocs\linux-x64\Migrator"
