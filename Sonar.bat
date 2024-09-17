dotnet sonarscanner begin /k:"EXE201_BE_THRIVOHR_EXE201_BE_THRIVOHR_8f1b6a3a-2d02-48e6-ae35-ce16c4915992" /d:sonar.host.url="http://localhost:9000"  /d:sonar.token="sqp_7f57bbb40413afa4bd731cf6358afe1b653ada5a"
dotnet build
dotnet sonarscanner end /d:sonar.token="sqp_7f57bbb40413afa4bd731cf6358afe1b653ada5a"