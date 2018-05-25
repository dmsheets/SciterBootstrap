@echo off

if "%1"=="Debug" exit

echo ######## Packing '/res' directory to '/Src/ArchiveResource.cs' ########
cd %~dp0
packfolder.exe ../res ../Src/ArchiveResource.cs -csharp -x "*IconBundler*;*sciter.dll"