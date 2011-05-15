SET FILENAME=PostBuildEvents.bat

IF "%~1" == "" GOTO bad_parameters
IF "%~2" == "" GOTO bad_parameters
IF "%~3" == "" GOTO bad_parameters
IF "%~4" == "" GOTO bad_parameters
IF "%~5" == "" GOTO bad_parameters
IF "%~6" == "" GOTO parameters_ok
:bad_parameters
ECHO Usage:
ECHO     %FILENAME% ^<FrameworkTarget^> ^<ConfigurationName^> ^<ProjectName^> ^<TargetName^> ^<TargetFileName^>
ECHO     FrameworkTarget Examples:
ECHO         NET2        Indicates the build is for the .NET Framework v2.0
ECHO         NET3.5CP    Indicates the build is for the .NET Framework v3.5 Client Profile
ECHO         NET3.5      Indicates the build is for the .NET Framework v3.5
ECHO         NET4CP      Indicates the build is for the .NET Framework v4.0 Client Profile
ECHO         NET4        Indicates the build is for the .NET Framework v4.0
ECHO.
EXIT /B 1
:parameters_ok

SET frameworkTarget=%~1
SET configurationName=%~2
SET projectName=%~3
SET targetName=%~4
SET targetFileName=%~5

ECHO.
ECHO --- Executing %FILENAME% with Parameters ---
ECHO FrameworkTarget=%frameworkTarget%
ECHO ConfigurationName=%configurationName%
ECHO ProjectName=%projectName%
ECHO TargetName=%targetName%
ECHO TargetFileName=%targetFileName%

IF "%configurationName:Release=%" == "%configurationName%" GOTO debug

:release
ECHO.
ECHO --- Running Post-Build Events For Release ---

IF "%configurationName:Register in GAC=%" NEQ "%configurationName%" (
	ECHO.
	ECHO --- Registering Assembly in the GAC ---
	ECHO gacutil -I "%targetFileName%"
	gacutil -I "%targetFileName%"
	IF %ERRORLEVEL% NEQ 0 EXIT /B 2
)

ECHO.
ECHO --- Ensuring Output Path Exists ---
IF NOT EXIST ..\..\..\Output (
	ECHO MKDIR ..\..\..\Output
	MKDIR ..\..\..\Output
	IF %ERRORLEVEL% NEQ 0 EXIT /B 3
)
IF EXIST ..\..\..\Output\%frameworkTarget% (
	ECHO MKDIR ..\..\..\Output\%frameworkTarget%
	MKDIR ..\..\..\Output\%frameworkTarget%
	IF %ERRORLEVEL% NEQ 0 EXIT /B 4
)

ECHO.
ECHO --- Copying to Output ---
ECHO COPY /Y "%targetFileName%" ..\..\..\Output\%frameworkTarget%
COPY /Y "%targetFileName%" ..\..\..\Output\%frameworkTarget%
IF %ERRORLEVEL% NEQ 0 EXIT /B 5

IF "%configurationName:Build Documentation=%" NEQ "%configurationName%" (
	ECHO.
	ECHO --- Generating Documentation ---
	ECHO %SystemRoot%\Microsoft.NET\Framework\v3.5\MSBuild.exe "..\..\%projectName%.shfbproj"
	%SystemRoot%\Microsoft.NET\Framework\v3.5\MSBuild.exe "..\..\%projectName%.shfbproj"
	IF %ERRORLEVEL% NEQ 0 EXIT /B 6

	ECHO.
	ECHO --- Moving CHM File to Bin Output ---
	ECHO MOVE /Y ..\..\..\Output\OnlineDocumentation\%frameworkTarget%\%targetName%\Documentation.chm ..\..\..\Output\%frameworkTarget%\%targetName%.chm
	MOVE /Y ..\..\..\Output\OnlineDocumentation\%frameworkTarget%\%targetName%\Documentation.chm ..\..\..\Output\%frameworkTarget%\%targetName%.chm
	IF %ERRORLEVEL% NEQ 0 EXIT /B 7
)

ECHO.
ECHO --- Finished Post-Build Events For Release ---
ECHO.

GOTO :exit

:debug
ECHO.
ECHO --- Running Post-Build Events For Debug ---

ECHO.
ECHO --- Finished Post-Build Events For Debug ---
ECHO.

:exit
