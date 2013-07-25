@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)

set version=
if not "%PackageVersion%" == "" (
   set version=-Version %PackageVersion%
)

REM Package restore
.nuget\nuget.exe install Microsoft.Net.Http.2.2.13 -OutputDirectory %cd%\packages -NonInteractive

REM Build
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild OakIdeas.AppHarbor.Api.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

REM Package
mkdir Build
.nuget\nuget.exe pack "OakIdeas.AppHarbor.Api\OakIdeas.AppHarbor.Api.csproj" -symbols -o Build\net40 -p Configuration=%config% %version%
copy OakIdeas.AppHarbor.Api\bin\%config%\*.dll Build\
copy OakIdeas.AppHarbor.Api\bin\%config%\*.pdb Build\
