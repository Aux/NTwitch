# NTwitch
[![NuGet](https://img.shields.io/nuget/v/NTwitch.svg?label=release)](https://www.nuget.org/packages/NTwitch) [![NuGet Pre Release](https://img.shields.io/nuget/vpre/NTwitch.svg?label=pre-release)](https://www.nuget.org/packages/NTwitch) [![MyGet Pre Release](https://img.shields.io/myget/aux/vpre/NTwitch.svg?label=dev)](https://www.myget.org/feed/Packages/aux) [![Build status](https://ci.appveyor.com/api/projects/status/3druvy47ds3uld47/branch/dev?svg=true)](https://ci.appveyor.com/project/Aux/ntwitch/branch/dev) [![Discord](https://discordapp.com/api/guilds/257698577894080512/widget.png)](https://discord.gg/yd8x2wM)

A .net core implementation of the https://twitch.tv/ version 5 api, strongly based on [Discord.Net](https://github.com/RogueException/Discord.Net).

## Builds
Release and pre-release builds can be found on Nuget under [NTwitch](https://www.nuget.org/packages/NTwitch/).

#### Releases
Stable builds can be found on Nuget. The contents of these packages reflect what is shown in the [master](https://github.com/Aux/NTwitch/tree/master) branch.
- [NTwitch](https://www.nuget.org/packages/NTwitch/)
- [NTwitch.Rest](https://www.nuget.org/packages/NTwitch.Rest/)
- [NTwitch.Chat](https://www.nuget.org/packages/NTwitch.Chat/)
- [NTwitch.Pubsub](https://www.nuget.org/packages/NTwitch.Pubsub/)
#### Pre-Release
Unstable builds can also be found on Nuget with the pre-release option checked. The contents of these packages reflect what is shown in the [nightly](https://github.com/Aux/NTwitch/tree/nightly) branch, and are updated from the dev branch once a week.
#### Indev
You can access the latest builds via the myget feed listed below. The contents of these packages are built automatically as new code is committed to the [dev](https://github.com/Aux/NTwitch/tree/dev) branch.
- `https://www.myget.org/F/aux/api/v3/index.json`

## Documentation
There is not currently any documentation for NTwitch. If you have any questions or want status updates about the library, please join the [NTwitch Discord Guild](https://discord.gg/yd8x2wM).

## Compiling
To compile NTwitch yourself, you will need either of the following:

##### Visual Studio
- [Visual Studio 2017](https://www.microsoft.com/net/core#windowsvs2017)

##### Command Line Tools
- [.Net Core 1.1 SDK](https://www.microsoft.com/net/download/core)
