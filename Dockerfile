FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build

COPY . /source

WORKDIR /source/EXE201_BE_ThrivoHR.API

ARG TARGETARCH


RUN --mount=type=cache,id=nuget,target=/root/.nuget/packages \
    dotnet publish -a ${TARGETARCH/amd64/x64} --use-current-runtime --self-contained false -o /app


FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
RUN apk add icu-libs
WORKDIR /app

COPY --from=build /app .
RUN mkdir -p /app/TrainedFaces \
    && chmod -R 777 /app/TrainedFaces
    
USER $APP_UID

ENTRYPOINT ["dotnet", "EXE201_BE_ThrivoHR.API.dll"]
