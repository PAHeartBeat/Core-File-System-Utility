@REM Build project for Relapse
ECHO Building project for Release configuration.
dotnet clean Src/. -c Debug
dotnet clean Src/. -c Release
dotnet build Src/. -c Release

@REM Creating NuGet packages
ECHO Creating NuGet Package
dotnet pack Src/. --include-symbols --force -c Release --output ./Packages/NuGet/.

@REM Creating Unity NPM Project
ECHO Step 1: Publishing Code for Unity Package
export LOCATION=com.iPAHeartBeat.Core.FileSystemUtility
rm Unity/Packages/$LOCATION/Runtime/* -r
dotnet publish Src/$LOCATION.csproj -c Release --no-dependencies --framework net471 --output ./Unity/Packages/$LOCATION/Runtime/.

@REM Removing Extra DLL for Which Code Will available via Unity package registries.
ECHO Step 2: Removing Extra DLLs
rm -f ./Unity/Packages/%LOCATION%/Runtime/Newtonsoft.Json.dll
rm -f ./Unity/Packages/%LOCATION%/Runtime/iPAHeartBeat.Core.Abstraction.dll
rm -f ./Unity/Packages/%LOCATION%/Runtime/iPAHeartBeat.Core.Extensions.dll
rm -f ./Unity/Packages/%LOCATION%/Runtime/iPAHeartBeat.Core.Dependency.dll

@REM Create Node package for Unity
ECHO Step 3: Creating Node Package for Unity Package Manager.
cd Unity/Packages/$LOCATION
npm pack --pack-destination="../../../Packages/UnityNPM/"
cd ..
cd ..
cd ..
ECHO Packages are Created.
