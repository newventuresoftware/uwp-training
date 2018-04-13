# Telerik UI for UWP Training Materials
Presentation resources and demo materials for Telerik UI for UWP.

## Clone
```bash
git clone --recurse-submodules https://github.com/newventuresoftware/uwp-training.git
cd uwp-training
```

## Slides

Open slides/index.html in your browser or run the presentation on [RawGit](https://rawgit.com/newventuresoftware/uwp-training/master/slides/index.html).

## Running the Demos

### Prerequisites

* [Visual Studio](https://www.visualstudio.com/downloads/) - you need Visual Studio for Windows in order to compile and run UWP projects. 
* [NodeJS](https://nodejs.org/en/) - (optional) used for downloading the dependencies and running the demo REST service. Not required for UWP development;

After installing NodeJS, you can start the demo REST service through `npm` (Node Package Manager). Enter in the `Command Prompt` the following command:

```bash
cd service
npm install
npm start
```

As this is a service running on your machine, you need to obtain your local network ip address and update the following constant:
```
namespace NWApp.Services
{
    public class NorthwindService : INorthwindService
    {
        ...
        private static string baseServiceUrl = "<your ip>:3000";
        ...
    }
}
```

### XamlPerf

* Navigate to `\demos\complete\XamlPerf`
* All the needed files are already included and referenced, so simply build the project (CTRL + SHIFT + B) to get it ready to run.

### AdaptiveApp

* Navigate to `\demos\complete\AdaptiveApp`
* A simple build is sufficient.

### Sample Photo Lab [Repo](https://github.com/Microsoft/Windows-appsample-photo-lab)

* Navigate to `\demos\Windows-appsample-photo-lab`
* A simple build is sufficient.

### Customers Orders Database sample [Repo](https://github.com/Microsoft/Windows-appsample-customers-orders-database)

* Navigate to `\demos\Windows-appsample-customers-orders-database`
* A simple build is sufficient.

### NWApp

* Navigate to `\demos\complete\NWApp`
* A simple build is sufficient.

## Terms & Conditions of Use

Telerik UI for UWP can be obtained for free as its open-source version can be acquired from its [GitHub Repo](https://github.com/telerik/UI-For-UWP), however, it is only community-supported on Stack Overflow. Commercial support is also available [here](http://www.telerik.com/uwp) where you'll find a supported commercial trial and pricing.

All available Telerik UI for UWP commercial licenses may be obtained [here](https://www.telerik.com/universal-windows-platform-ui).