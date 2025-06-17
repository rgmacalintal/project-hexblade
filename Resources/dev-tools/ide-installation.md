# IDE Setup

## Installation
The preferrable IDE for development is Visual Studio 2022 as it would be easy to install workloads needed.

VSCode can also be used but we will have to make note of the workloads/dependencies/extensions that are needed more specifically at another time.

### Steps
When installing Visual Studio 2022, check the following workloads:

Under Web & Cloud
- ASP.NET and web development
- Python development
- Node.js development

![Web & Cloud](../images/dev-tools/web-and-cloud.png)

Under Desktop & Mobile
- .NET Multi-platform App UI development
- .NET desktop development
- Desktop development with C++
- Mobile development with C++

![Desktop & Mobile](../images/dev-tools/desktop-and-mobile.png)

Optionally, although highly recommended, you can also select the following under Other Toolsets

- Data storage and processing
- Visual Studio extension development
- Linux, Mac, and embedded development with C++

![Other Toolsets](../images/dev-tools/other-toolsets.png)

Once selected, continue installation as normal.

### Check Workload Selections
If you think you forgot to select any workload, open Visual Studio and go to Tools > Get Tools and Features...

![Tools](../images/dev-tools/tools.png)

After this, you should see the Workloads screen once again and can check which packages you have selected. If by chance you missed one, you can select it and select Modify with the option to Install while downloading or Download all, then install - either choice should do the job.

![Modify](../images/dev-tools/modify.png)

Installation should now proceed as seen

![Install](../images/dev-tools/install.png)
