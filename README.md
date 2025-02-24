# electrifier
## integrated Desktop Enhancement suite ![electrifier Logo](./assets/Electrifier%20Logo%20-%20Unflashed.png)

Classic electrifier was an open-source project which extended Windows File Explorer :registered: by
- [ ] Apache License, Version 2.0 (the "License")
- [x] Full-featured multi-tab experience

Classic electrifier was in early development stages - most featureswerw work in progress or just plans for the future
- [ ] Built-in file viewers and plain editors
- [ ] Session management and history functionality
- [ ] PowerShell integration, automation and extensibility

👫 If you want to get involved in its development feel free to contribute

# Requirements 
Originally, the project was started back in 2004, using .NET-framework 1.0 and Windows XP. However, due to a system crash, I lost all my data, including the source-code, which was stored on a software RAID-5.

Meanwhile I managed to restore those lost artefacts, converted them to Visual Studio 2019 and .NET 4.6 and relaunched the development cycle.

## Release History
Released | Version | Details
:------: | :-----: | :------
04/05/19 | 0.1.1 | An internal alpha test is currently in progress


## Docs and Scrrens

![electrifier Main Form](./docs/Pictures/MainForm.png)

# Contribute
## Development

electrifier was developed using Visual Studio Community 2019.
It used .NET-framework 4.6 and WinForms.

electrifier makes use of the following third party components:

Name | Author(s) | License
---- | --------- | -------
[Vanara](https://github.com/dahall/Vanara) | [David Hall](https://github.com/dahall) | MIT License
[DockPanel Suite](https://github.com/dockpanelsuite/dockpanelsuite) | [Lex Li](https://github.com/lextm), [Ryan Rastedt](https://github.com/roken) & others | MIT License

electrifier makes use of the following third party components:

Name | Author(s) | License
---- | --------- | -------
[Knob Buttons Toolbar icons](https://www.deviantart.com/itweek/art/Knob-Buttons-Toolbar-icons-73463960) | [iTweek](https://www.deviantart.com/itweek) aka Miles Ponson| [Custom]

## License and Contributing

⚠️ Classic electrifier is orphaned!
- Apache License 2.0
- Feel free to play around with that code
- Contributions are still wellcome

# Pre-Requisites

To run electrifier, you supposedly need nothing except a running Windows Operating System, Vista SP1 or above. The .NET-framework should be installed due normal Windows-Update cycle.

## Building electrifier

* Install [Visual Studio Community 2019](https://visualstudio.microsoft.com/de/vs/community/) using at least the following Workloads:
  * `.NET desktop development`
  * `Desktop development with C++` (Required for the Ribbon Ressource Compiler)
* Download the sources to extract them into an empty folder
* From there, use `electrifier/src/electrifier.sln` to open the project solution in Visual Studio
* In Solution Explorer, right-click `Solution 'electrifier' (3 projects)` to `Restore NuGet Packages`
* In Solution Explorer, right-click project `electrifier` to `Set as StartUp Project`
* Hit `F6`-key to `Build Solution`
* Violá! In case you reached so far without ciritical errors, you've just successfully built electrifier :+1:
* Hit `F5`-key to `Start Debugging`. Since electrifier will start with a blank UI at present, click `New Panel` from the ribbon to open a fresh electrifier Shell Browser window

:warning: To successfully build `Setup`, further steps have to be taken!

# License


    Copyright 2017-19 Thorsten Jung, www.electrifier.org
 
    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at
 
        http://www.apache.org/licenses/LICENSE-2.0
 
    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.


:pencil2: Last updated :calendar: May the 4th, 2019
