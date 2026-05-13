# CSL-SimpleMetrics

![Cities: Skylines](https://img.shields.io/badge/Game-Cities%3A%20Skylines-2F6DB5)
![C#](https://img.shields.io/badge/C%23-Mod-239120)
![.NET Framework](https://img.shields.io/badge/.NET-3.5-512BD4)

Lightweight metrics bar mod for Cities: Skylines. It adds a compact, draggable overlay that shows service capacity vs. usage with color-coded indicators on the in-game UI.

Additionaly it might be a great CSL modding entry point as I tried to build it with clean code standards.

It's my first approach to game modding. Hope it would be helpful for you.

## Steam workshop

Not deployed yet.

## Development Notes

- Load hook: `Extensions/LoadingExtension.cs`
- Manager component: `Behaviours/Manager.cs`
- UI: `UI/Window.cs`
- Metrics calculation: `Services/MetricsService.cs`

## Build

Modding CSL requires `.NET Framework 3.5` and references the Cities: Skylines managed assemblies directly from a local install.

TODO here

Check [credits](#credits) for documentation references and other cool modders which codebases gave me a lot of information.

## Credits and kudos

Other modders whos codebases were helpful while building this mod:

- keallu's [CSL-WatchIt](https://github.com/keallu/CSL-WatchIt) mod
- rob-williams [CityVitalsWatchMod](https://github.com/rob-williams/CityVitalsWatchMod)

Thanks guys for your great work and passive contribution to this project!

Documentation references and other sources:

- Cities: Skylines in-game sprite and API references
  - TODO link to docs
- Icons and visual assets: https://www.flaticon.com
