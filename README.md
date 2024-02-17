# WebView2Example

This repo serves as an example and/or template on setting op a desktop app using [Edge WebView2](https://developer.microsoft.com/en-us/microsoft-edge/webview2). This allows you to combine a native app and a web-based application, much like how [Electron](https://www.electronjs.org/) works.

This example uses: 
- WPF to host the WebView2 control
- An ASP.NET API project to host an API and serve static content.
- React for the actual frontend itself.
  - This is easily swapped out for the front-end library / framework of your choice.


## How it works:
- The ASP.NET app is modified in order to function as a library instead of an executable.
  - What is normally in program.cs is in WebApp.cs, modified to be contained within a class instead of as top-level statements
  - Because it is used as a library [`.AddControllers()`](https://github.com/NanoBob/WebView2Example/blob/main/ExampleApp.Web/WebApp.cs#L16) doesn't work as it normally does, we have to specifically reference the assembly.
  - Default and static files are hosted, unlike with a normal API project.
  - The project can be started on a specific port by argument, meaning the WPF project can determine what port to host it on, in order to open it in the WebView.
- The WPF app only has one control, the Edge WebView2 itself.
  - The WPF app starts the API project
  - The WPF app navigates the WebView2 to the API project's URL

### Caveats
- Due to the Web project being modified to run as a library, you can no longer run it directly. If you wish to run the API without running the WPF app you could create a separate console app that does: `new WebApp.Run(portNumber);`
- Since the API just hosts static files for the frontend you need to run `npm build` in order for any changes to take place in the WPF UI. If you want to use the React dev server you can change the URI that the WebView loads in [`MainWindow.xaml.cs`](https://github.com/NanoBob/WebView2Example/blob/main/ExampleApp.Ui/MainWindow.xaml.cs)

## How to expand:
You can develop on top of this as you normally would with a ASP.NET API project, and a separate frontend, as you normally would. You can also add more to the WPF project if you wish, but the general idea is that any UI logic can be done in your web frontend.

If you prefer to use ASP.NET MVC you can do so too, the general setup remains the same.
