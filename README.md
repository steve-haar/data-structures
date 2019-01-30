# Data Structures

Simple data structures implemented in .net core with c#

## Build

`dotnet build`

## Test

`dotnet test`

## View Test Coverage

First, install the global dotnet reportgenerator tool
`dotnet tool install -g dotnet-reportgenerator-globaltool`

Then run the following commands
`dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:Exclude=[xunit.*]*`
`reportgenerator -reports:Library/test/coverage.opencover.xml -targetdir:Library/test/reports`

Finally, your report will be available here
`Library/test/reports/index.htm`