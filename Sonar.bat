dotnet sonarscanner begin /k:"ThrivoHR" /d:sonar.host.url="http://localhost:32768"  /d:sonar.token="sqp_24854a7f603bad174ed3e7e1687967eca5a09690"
dotnet build
dotnet sonarscanner end /d:sonar.token="sqp_24854a7f603bad174ed3e7e1687967eca5a09690"