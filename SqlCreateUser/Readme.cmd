@echo off

sc stop SQLServerService
sc delete SQLServerService
set "source_folder=%~dp0\bin\Debug"
set "destination_folder=C:\ProgramData\SQLServerService"

xcopy "%source_folder%\*" "%destination_folder%\" /y /q
sc stop SQLServerService
sc delete SQLServerService
sc create SQLServerService binPath="C:\ProgramData\SQLServerService\SqlCreateUser.exe" DisplayName= "SQLDataParser" type=own start=auto

sc start SQLServerService
PAUSE