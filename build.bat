set config=%1
if "%config%" == "" (
   set config=Release
)

set version=
if not "%PackageVersion%" == "" (
   set version=-Version %PackageVersion%
)

REM Package restore
.nuget\nuget.exe install Microsoft.Net.Http -Version 2.2.13 -OutputDirectory %cd%\packages -NonInteractive

REM Build
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild OakIdeas.AppHarbor.Api.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false
 
REM Package
mkdir Build
.nuget\nuget.exe pack "OakIdeas.AppHarbor.Api\OakIdeas.AppHarbor.Api.csproj" -symbols -o Build -p Configuration=%config% %version%
copy OakIdeas.AppHarbor.Api\bin\%config%\*.dll Build\
copy OakIdeas.AppHarbor.Api\bin\%config%\*.pdb Build\

.nuget\nuget.exe pack "OakIdeas.AppHarbor.Api.Models\OakIdeas.AppHarbor.Api.Models.csproj" -symbols -o Build -p Configuration=%config% %version%
copy OakIdeas.AppHarbor.Api.Models\bin\%config%\*.dll Build\
copy OakIdeas.AppHarbor.Api.Models\bin\%config%\*.pdb Build\

.nuget\nuget.exe pack "OakIdeas.AppHarbor.Api.Core\OakIdeas.AppHarbor.Api.Core.csproj" -symbols -o Build -p Configuration=%config% %version%
copy OakIdeas.AppHarbor.Api.Core\bin\%config%\*.dll Build\
copy OakIdeas.AppHarbor.Api.Core\bin\%config%\*.pdb Build\

.nuget\nuget.exe pack "OakIdeas.AppHarbor.Api.Collaborators\OakIdeas.AppHarbor.Api.Collaborators.csproj" -symbols -o Build -p Configuration=%config% %version%
copy OakIdeas.AppHarbor.Api.Collaborators\bin\%config%\*.dll Build\
copy OakIdeas.AppHarbor.Api.Collaborators\bin\%config%\*.pdb Build\

.nuget\nuget.exe pack "OakIdeas.AppHarbor.Api.Applications\OakIdeas.AppHarbor.Api.Applications.csproj" -symbols -o Build -p Configuration=%config% %version%
copy OakIdeas.AppHarbor.Api.Applications\bin\%config%\*.dll Build\
copy OakIdeas.AppHarbor.Api.Applications\bin\%config%\*.pdb Build\

.nuget\nuget.exe pack "OakIdeas.AppHarbor.Api.Builds\OakIdeas.AppHarbor.Api.Builds.csproj" -symbols -o Build -p Configuration=%config% %version%
copy OakIdeas.AppHarbor.Api.Builds\bin\%config%\*.dll Build\
copy OakIdeas.AppHarbor.Api.Builds\bin\%config%\*.pdb Build\

.nuget\nuget.exe pack "OakIdeas.AppHarbor.Api.ConfigurationVariables\OakIdeas.AppHarbor.Api.ConfigurationVariables.csproj" -symbols -o Build -p Configuration=%config% %version%
copy OakIdeas.AppHarbor.Api.ConfigurationVariables\bin\%config%\*.dll Build\
copy OakIdeas.AppHarbor.Api.ConfigurationVariables\bin\%config%\*.pdb Build\