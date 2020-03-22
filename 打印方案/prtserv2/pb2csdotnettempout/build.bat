setlocal

if "%1" == "" goto usage 
if "%PBNET_HOME%" == "" goto sethome

md bin
md install

if "%1" == "c-" goto notcopy
if "%1" == "C-" goto notcopy
goto copyfile

rem ************************************************************************
rem		For release build, not copy assemblies to the folder bin 
rem ************************************************************************
:notcopy
reg import "%PBNET_HOME%\bin\PBExceptionTrace.reg"


xcopy /D /Y "y:\c#\bin2019\prtserv2\prtserv2.pbd" bin\

xcopy /D /Y "y:\c#\bin2019\prtserv2\prtserv2.pbd" install\


if "%~2" == "" (
csc /t:library /optimize+ /nowarn:1591 /doc:"bin\PrtServ.xml"   /platform:x86  /linkres:bin\prtserv2.pbd "/r:%PBNET_HOME%\bin\Sybase.PowerBuilder.Core.dll;%PBNET_HOME%\bin\Sybase.PowerBuilder.Interop.dll;%PBNET_HOME%\bin\Sybase.PowerBuilder.Common.dll;%PBNET_HOME%\bin\Sybase.PowerBuilder.Win.dll;%PBNET_HOME%\bin\com.sybase.ejb.net.dll;%PBNET_HOME%\bin\com.sybase.iiop.net.dll" /out:"bin\PrtServ.dll" *.cs
) else (
csc /t:library /optimize+ /nowarn:1591 /doc:"bin\PrtServ.xml"   /platform:x86  /linkres:bin\prtserv2.pbd "/r:%PBNET_HOME%\bin\Sybase.PowerBuilder.Core.dll;%PBNET_HOME%\bin\Sybase.PowerBuilder.Interop.dll;%PBNET_HOME%\bin\Sybase.PowerBuilder.Common.dll;%PBNET_HOME%\bin\Sybase.PowerBuilder.Win.dll;%PBNET_HOME%\bin\com.sybase.ejb.net.dll;%PBNET_HOME%\bin\com.sybase.iiop.net.dll" /out:"bin\PrtServ.dll" *.cs > "%~2"
)

@if errorlevel 1 (goto builderror) else echo Build Succeeded.
goto end

rem ************************************************************************
rem		For debug build, copy assemblies
rem ************************************************************************
:copyfile
xcopy /D /Y "%PBNET_HOME%\bin\Sybase.Data.AseClient.dll" bin\
xcopy /D /Y "%PBNET_HOME%\bin\Sybase.PowerBuilder.Ado.dll" bin\
xcopy /D /Y "%PBNET_HOME%\bin\Sybase.PowerBuilder.Db*" bin\

xcopy /D /Y "%PBNET_HOME%\bin\Sybase.PowerBuilder.Core.dll" bin\
if exist "%PBNET_HOME%\bin\Sybase.PowerBuilder.Core.pdb" xcopy /D /Y "%PBNET_HOME%\bin\Sybase.PowerBuilder.Core.pdb" bin\

xcopy /D /Y /E "%PBNET_HOME%\bin\de\*.*" bin\de\
xcopy /D /Y /E "%PBNET_HOME%\bin\fr\*.*" bin\fr\

xcopy /D /Y "%PBNET_HOME%\bin\Sybase.PowerBuilder.Interop.dll" bin\
if exist "%PBNET_HOME%\bin\Sybase.PowerBuilder.Interop.pdb" xcopy /D /Y "%PBNET_HOME%\bin\Sybase.PowerBuilder.Interop.pdb" bin\

xcopy /D /Y "%PBNET_HOME%\bin\Sybase.PowerBuilder.Common.dll" bin\
if exist "%PBNET_HOME%\bin\Sybase.PowerBuilder.Common.pdb" xcopy /D /Y "%PBNET_HOME%\bin\Sybase.PowerBuilder.Common.pdb" bin\

xcopy /D /Y "%PBNET_HOME%\bin\Sybase.PowerBuilder.Win.dll" bin\
if exist "%PBNET_HOME%\bin\Sybase.PowerBuilder.Win.pdb" xcopy /D /Y "%PBNET_HOME%\bin\Sybase.PowerBuilder.Win.pdb" bin\

xcopy /D /Y "%PBNET_HOME%\bin\Sybase.PowerBuilder.DataWindow.Win.dll" bin\
if exist "%PBNET_HOME%\bin\Sybase.PowerBuilder.DataWindow.Win.pdb" xcopy /D /Y "%PBNET_HOME%\bin\Sybase.PowerBuilder.DataWindow.Win.pdb" bin\
xcopy /D /Y "%PBNET_HOME%\bin\Sybase.PowerBuilder.DataWindow.Win.tlb" bin\

xcopy /D /Y "%PBNET_HOME%\bin\Sybase.PowerBuilder.DataWindow.Interop.*" bin\

xcopy /D /Y "%PBNET_HOME%\bin\Sybase.PowerBuilder.EditMask.Win.*" bin\
xcopy /D /Y "%PBNET_HOME%\bin\Sybase.PowerBuilder.EditMask.Interop.*" bin\

xcopy /D /Y "%PBNET_HOME%\bin\Sybase.PowerBuilder.Graph*" bin\

xcopy /D /Y "%PBNET_HOME%\bin\Sybase.PowerBuilder.WebService.Runtime*.dll" bin\
if exist "%PBNET_HOME%\bin\Sybase.PowerBuilder.WebService.Runtime*.pdb" xcopy /D /Y "%PBNET_HOME%\bin\Sybase.PowerBuilder.WebService.Runtime*.pdb" bin\

xcopy /D /Y "%PBNET_HOME%\bin\Microsoft.Ink*.dll" bin\

xcopy /D /Y "%PBNET_HOME%\bin\com.sybase.ejb.net.dll" bin\
xcopy /D /Y "%PBNET_HOME%\bin\com.sybase.iiop.net.dll" bin\

reg import "%PBNET_HOME%\bin\PBExceptionTrace.reg"


xcopy /D /Y "y:\c#\bin2019\prtserv2\prtserv2.pbd" bin\

xcopy /D /Y "y:\c#\bin2019\prtserv2\prtserv2.pbd" install\


if "%~2" == "" (
csc /t:library /optimize+ /nowarn:1591 /doc:"bin\PrtServ.xml"   /platform:x86  /linkres:bin\prtserv2.pbd "/r:bin\Sybase.PowerBuilder.Core.dll;bin\Sybase.PowerBuilder.Interop.dll;bin\Sybase.PowerBuilder.Common.dll;bin\Sybase.PowerBuilder.Win.dll;bin\Sybase.PowerBuilder.DataWindow.Win.dll;%PBNET_HOME%\bin\com.sybase.ejb.net.dll;%PBNET_HOME%\bin\com.sybase.iiop.net.dll" /out:"bin\PrtServ.dll" *.cs
) else (
csc /t:library /optimize+ /nowarn:1591 /doc:"bin\PrtServ.xml"   /platform:x86  /linkres:bin\prtserv2.pbd "/r:bin\Sybase.PowerBuilder.Core.dll;bin\Sybase.PowerBuilder.Interop.dll;bin\Sybase.PowerBuilder.Common.dll;bin\Sybase.PowerBuilder.Win.dll;%PBNET_HOME%\bin\com.sybase.ejb.net.dll;%PBNET_HOME%\bin\com.sybase.iiop.net.dll" /out:"bin\PrtServ.dll" *.cs > "%~2"
)

@if errorlevel 1 (goto builderror) else echo Build Succeeded.
goto end

:builderror
@echo.
@echo Build Failed.
goto errorend

:usage
@echo Usage: build c+/c- [tempfile]
goto errorend

:sethome
@echo The PBNET_HOME system environment variable is not set. Please set it to the location of your PowerBuilder 11.5\DotNET directory.

:errorend
exit /b 1

:end
xcopy /D /Y "bin\PrtServ.dll" install\

endlocal