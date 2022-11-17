# Introduction

Small sample project as part of a coding assessment for ChannelEngine.

In case you are wondering why all the projects are prefixed with Dnw, it's because my Dutch company is called DotNetWorks (and Dnw sounds like a good abbreviation).

# Running locally

The base url for the ChannelEngine API can be set in the different appsettings.json files:

```json
{
  "ChannelEngine": {
    "BaseUrl": "https://api-dev.channelengine.net"
  }
}
```

For running the UI, Console App and the tests marked with the "ExternalApi" trait,
a ChannelEngine api key is required in your secrets.json:

```json
{
  "ChannelEngine": {
    "ApiKey": "xxx"
  }
}
```

When running the UI project make sure to set the ASPNETCORE_ENVIRONMENT environment variable to Development. 
If it's not set to Development, appsettings.Development.json will not be loaded on startup and the ChannelEngine API base url will not be available.

# Running tests

The tests for the ChannelEngine API have been marked with an attribute so they can be easily excluded from test runs:

```
[Trait("Category", "ExternalApi")]
```

To run all tests, excluding the "ExternalApi" category use: 

```
dotnet test --filter Category!=ExternalApi
```

Or configure your IDE to exclude them.