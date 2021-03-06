﻿set config=%1
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

.nuget\nuget.exe pack "OakIdeas.AppHarbor.Api.Errors\OakIdeas.AppHarbor.Api.Errors.csproj" -symbols -o Build -p Configuration=%config% %version%
copy OakIdeas.AppHarbor.Api.Errors\bin\%config%\*.dll Build\
copy OakIdeas.AppHarbor.Api.Errors\bin\%config%\*.pdb Build\

.nuget\nuget.exe pack "OakIdeas.AppHarbor.Api.Hostnames\OakIdeas.AppHarbor.Api.Hostnames.csproj" -symbols -o Build -p Configuration=%config% %version%
copy OakIdeas.AppHarbor.Api.Hostnames\bin\%config%\*.dll Build\
copy OakIdeas.AppHarbor.Api.Hostnames\bin\%config%\*.pdb Build\

.nuget\nuget.exe pack "OakIdeas.AppHarbor.Api.ServiceHooks\OakIdeas.AppHarbor.Api.ServiceHooks.csproj" -symbols -o Build -p Configuration=%config% %version%
copy OakIdeas.AppHarbor.Api.ServiceHooks\bin\%config%\*.dll Build\
copy OakIdeas.AppHarbor.Api.ServiceHooks\bin\%config%\*.pdb Build\

.nuget\nuget.exe pack "OakIdeas.AppHarbor.Api.Users\OakIdeas.AppHarbor.Api.Users.csproj" -symbols -o Build -p Configuration=%config% %version%
copy OakIdeas.AppHarbor.Api.Users\bin\%config%\*.dll Build\
copy OakIdeas.AppHarbor.Api.Users\bin\%config%\*.pdb Build\

.nuget\nuget.exe pack "OakIdeas.AppHarbor.Api.Tests\OakIdeas.AppHarbor.Api.Tests.csproj" -symbols -o Build -p Configuration=%config% %version%
copy OakIdeas.AppHarbor.Api.Tests\bin\%config%\*.dll Build\
copy OakIdeas.AppHarbor.Api.Tests\bin\%config%\*.pdb Build\

.nuget\nuget.exe pack "OakIdeas.AppHarbor.Api.LogSessions\OakIdeas.AppHarbor.Api.LogSessions.csproj" -symbols -o Build -p Configuration=%config% %version%
copy OakIdeas.AppHarbor.Api.LogSessions\bin\%config%\*.dll Build\
copy OakIdeas.AppHarbor.Api.LogSessions\bin\%config%\*.pdb Build\

.nuget\nuget.exe pack "OakIdeas.AppHarbor.Api.Drains\OakIdeas.AppHarbor.Api.Drains.csproj" -symbols -o Build -p Configuration=%config% %version%
copy OakIdeas.AppHarbor.Api.Drains\bin\%config%\*.dll Build\
copy OakIdeas.AppHarbor.Api.Drains\bin\%config%\*.pdb Build\