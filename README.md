# Data Structures

Supplemental code for [https://stevethescholar.com/data-structures/](https://stevethescholar.com/data-structures/)

Simple data structures implemented in .net core with c#

* Dynamic Arrays
* Stacks & Queues
* Linked Lists
* Hash Tables
* Binary Heaps

## Build

`dotnet build`

## Test

`dotnet test`

## View Test Coverage

First, install the global dotnet reportgenerator tool
`dotnet tool install -g dotnet-reportgenerator-globaltool`

Then run the following commands
- `dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:Exclude=[xunit.*]*`
- `reportgenerator -reports:Library/test/coverage.opencover.xml -targetdir:Library/test/reports`

Finally, your report will be available here
`Library/test/reports/index.htm`
