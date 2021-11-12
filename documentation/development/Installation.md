# Downloading and installing the required programs (Windows)

## Git (optional)
It is highly recommended to use git because it makes it much easier to stay up to date with the latest improvements.
Download Git from https://git-scm.com/.

## NodeJS
Download the LTS version from https://nodejs.org/en/download/.

## Installing Angular CLI
The Angular CLI is a command-line interface tool used to maintain Angular applications directly from the command shell.

To get started open a command prompt by typing `cmd` in your windows search bar.

Type `npm install -g @angular/cli` to install the Angular CLI.

## ASP .NET Core 5
- Download the SDK from https://dotnet.microsoft.com/download/dotnet/5.0
  OR
- Download [Visual Studio IDE](https://visualstudio.microsoft.com/downloads/)
- Select the `ASP.NET and web development` workload while following the installation process.

## Downloading the application
Now that you have all the required programs installed you can start with downloading the project source.

To get started open a command prompt by typing `cmd` in your windows search bar.

Navigate to your desired folder using the `cd` command.

Use `git clone https://github.com/rareseanu/Kastel-Planner --branch master` to clone the repository.

## Frontend

### Installing modules
You need to install the npm modules used by the frontend project. Do this by navigating to the `Kastel-Planner\Frontend`
folder and using the `npm install` command.

## Backend

### Installing Nuget packages
This step is required only if you're not using the Visual Studio IDE.

Opend a command prompt, navigated to the `Kastel-Planner\Backend` folder and type `dotnet restore`.

<hr/>

## All done?
Time to [configure the application](Configuration.md).
