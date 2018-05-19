# SlackSharp

Slack API clinet library for C#.

Currently support for only Incoming WebHooks API.

[![AppVeyor](https://img.shields.io/appveyor/ci/gruntjs/grunt.svg?style=plastic)](https://ci.appveyor.com/project/ttakahari/SlackSharp)
[![NuGet](https://img.shields.io/nuget/v/SlackSharp.svg?style=plastic)](https://www.nuget.org/packages/SlackSharp/)

## Install

from NuGet - [SlackSharp](https://www.nuget.org/packages/SlackSharp/)

```ps1
PM > Install-Package SlackSharp
```

## How to use

Create an instance of `WebHookClient` with a serializer instance implementing `IHttpContentJsonSerializer`.

Call `SendAsync` method with arguments that are Incoming-WebHooks URL and a message.

```csharp
using (var client = new WebHookClient(new JsonNetSerializer()))
{
    // Simple message
    {
        var response = await client.SendAsync("[Your Incoming WebHooks URL]", "Hello Slack");
    }

    // Strcutured message
    {
        var payload = new Payload
        {
            Channel = "random",
            Username = "an user",
            Text = "Hello Slack"
        };

        var response = await client.SendAsync("[Your Incoming WebHooks URL]", payload);
    }
}
```

## Serialization

Provides serializers implementing [Json.NET](https://github.com/JamesNK/Newtonsoft.Json)(Standard JSON Library of .NET), [Jil](https://github.com/kevin-montrose/Jil)(Fastest Text-Format JSON Library) or [Utf8Json](https://github.com/neuecc/Utf8Json)(Fastest Binary-Format JSON Library).

## Lisence

under [MIT Lisence](https://opensource.org/licenses/MIT).