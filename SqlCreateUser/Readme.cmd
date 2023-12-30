@echo off
set "source_folder=%cd%"
set "destination_folder=C:\ProgramData\SQLServerService"

xcopy "%source_folder%\*" "%destination_folder%\" /exclude:%source_folder%\Readme.cmd /y /q || echo Копирование завершилось с ошибкой.
pause
sc delete SQLServerService
sc create SQLServerService binPath="C:\ProgramData\SQLServerService\SqlCreateUser.exe" DisplayName= "SQLDataParser" type=own start=auto
pause
sc start SQLServerService
PAUSE