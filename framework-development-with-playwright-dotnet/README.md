# Automation Framework Development with Playwright in C# .NET

Contains code and notes from studying [Automation Framework Development with Playwright in C# .NET](https://www.udemy.com/course/framework-development-with-playwright-dotnet/)

## Understand the basics of Playwright

### Create Playwright project

We can create new "NUnit Playwright Test Project" from CLI. Check the available Playwright templates for NUnit:

```sh
dotnet new list
...
NUnit 3 Test Item                             nunit-test                  [C#],F#,VB  Test/NUnit
NUnit 3 Test Project                          nunit                       [C#],F#,VB  Test/NUnit/Desktop/Web
NUnit Playwright Test Project                 nunit-playwright            [C#]        Test/NUnit/Playwright/Desktop/Web
```

In this course we just create "NUnit 3 Test Project": `dotnet new nunit -n PlaywrightDemo -o PlaywrightDemo`

Add the following dependency packages:

```sh
dotnet add package Microsoft.Playwright --version 1.51.0
```

Create a test:

```csharp
using Microsoft.Playwright;

namespace PlaywrightDemo;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test1()
    {
        var playwrightDriver = await Playwright.CreateAsync();
        var chromium = await playwrightDriver.Chromium.LaunchAsync();
        var browserContext = await chromium.NewContextAsync();
        var page = await browserContext.NewPageAsync();

        await page.GotoAsync("http://eaapp.somee.com");
    }
}
```

If you try to run the test, it will explain that PLaywright was just installed and we have to:

```sh
dotnet test --filter "Test1"

      ╔════════════════════════════════════════════════════════════╗
      ║ Looks like Playwright was just installed or updated.       ║
      ║ Please run the following command to download new browsers: ║
      ║                                                            ║
      ║     pwsh bin/Debug/netX/playwright.ps1 install             ║
```

Run `pwsh bin/Debug/net9.0/playwright.ps1 install` as it suggested:

```sh
pwsh bin/Debug/net9.0/playwright.ps1 install

Downloading Chromium 134.0.6998.35 (playwright build v1161) from https://cdn.playwright.dev/dbazure/download/playwright/builds/chromium/1161/chromium-win64.zip
141.8 MiB [====================] 100% 0.0s
Chromium 134.0.6998.35 (playwright build v1161) downloaded to C:\Users\atanas.hristov\AppData\Local\ms-playwright\chromium-1161
Downloading Chromium Headless Shell 134.0.6998.35 (playwright build v1161) from https://cdn.playwright.dev/dbazure/download/playwright/builds/chromium/1161/chromium-headless-shell-win64.zip
87.8 MiB [====================] 100% 0.0s
Chromium Headless Shell 134.0.6998.35 (playwright build v1161) downloaded to C:\Users\atanas.hristov\AppData\Local\ms-playwright\chromium_headless_shell-1161
Downloading Firefox 135.0 (playwright build v1475) from https://cdn.playwright.dev/dbazure/download/playwright/builds/firefox/1475/firefox-win64.zip
91.5 MiB [====================] 100% 0.0s
Firefox 135.0 (playwright build v1475) downloaded to C:\Users\atanas.hristov\AppData\Local\ms-playwright\firefox-1475
Downloading Webkit 18.4 (playwright build v2140) from https://cdn.playwright.dev/dbazure/download/playwright/builds/webkit/2140/webkit-win64.zip
52.8 MiB [====================] 100% 0.0s
Webkit 18.4 (playwright build v2140) downloaded to C:\Users\atanas.hristov\AppData\Local\ms-playwright\webkit-2140
Downloading FFMPEG playwright build v1011 from https://cdn.playwright.dev/dbazure/download/playwright/builds/ffmpeg/1011/ffmpeg-win64.zip
1.3 MiB [====================] 100% 0.0s
FFMPEG playwright build v1011 downloaded to C:\Users\atanas.hristov\AppData\Local\ms-playwright\ffmpeg-1011
Downloading Winldd playwright build v1007 from https://cdn.playwright.dev/dbazure/download/playwright/builds/winldd/1007/winldd-win64.zip
0.1 MiB [====================] 100% 0.0s
Winldd playwright build v1007 downloaded to C:\Users\atanas.hristov\AppData\Local\ms-playwright\winldd-1007
```

Then repeat `dotnet test --filter "Test1"`:

```sh
dotnet test --filter "Test1"

Restore complete (0.3s)
  PlaywrightDemo succeeded (0.3s) → bin\Debug\net9.0\PlaywrightDemo.dll
NUnit Adapter 4.6.0.0: Test execution started
Running selected tests in C:\Users\atanas.hristov\Projects\study-test-automation\framework-development-with-playwright-dotnet\PlaywrightDemo\bin\Debug\net9.0\PlaywrightDemo.dll
   NUnit3TestExecutor discovered 1 of 1 NUnit test cases using Current Discovery mode, Non-Explicit run
NUnit Adapter 4.6.0.0: Test execution complete
  PlaywrightDemo test succeeded (7.2s)

Test summary: total: 1, failed: 0, succeeded: 1, skipped: 0, duration: 7.1s
Build succeeded in 8.0s

Workload updates are available. Run `dotnet workload list` for more information.

```

### Different ways to Launch a browser

By default Chromium runs headless. We can specify to open the Chromium window with `BrowserTypeLaunchOptions`:

```csharp
        var browserOption = new BrowserTypeLaunchOptions
        {
            Headless = false,
        };
        var chromium = await playwrightDriver.Chromium.LaunchAsync(browserOption);

```

Also, we can run the test with attached [dev tools](https://playwright.dev/dotnet/docs/debug):

```sh
$env:PWDEBUG=1 && dotnet test --filter "Test1"
```

We can make the instantiation more generic by using indexer on `IPlaywright`:

```csharp
var browser = await playwrightDriver["firefox"].LaunchAsync(browserOption);
```

If we want to open the installed Edge or Chrome, then we have to use channel:

```csharp
string? BrowserTypeLaunchOptions.Channel { get; set; }
Browser distribution channel.

Use "chromium" to opt in to new headless mode.

Use "chrome", "chrome-beta", "chrome-dev", "chrome-canary", "msedge", "msedge-beta", "msedge-dev", or "msedge-canary" to use branded Google Chrome and Microsoft Edge.
'Channel' is not null here.
```

and we have to use "chromium" as driver:

```csharp
        var browserOption = new BrowserTypeLaunchOptions
        {
            Headless = false,
            Channel = "msedge" // or "chrome", or "" for headless
        };
        var browser = await playwrightDriver["chromium"].LaunPchAsync(browserOption);
```

More information on channels is on the [Playwright BrowserType documentation page](https://playwright.dev/dotnet/docs/api/class-browsertype).

You can also run with [Playwright Inspector from Debugging Tools](https://playwright.dev/dotnet/docs/debug).
To do so, set `PWDEBUG=1` before you run the tests:

Bash: ```PWDEBUG=1 dotnet test```

PWSH:

```pwsh
$env:PWDEBUG=1
dotnet test
```

Here are the different ways to launch a browser:

```csharp
using Microsoft.Playwright;

namespace PlaywrightDemo;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task TestWithHeadlessChromium()
    {
        var playwrightDriver = await Playwright.CreateAsync();
        var chromium = await playwrightDriver.Chromium.LaunchAsync();
        var browserContext = await chromium.NewContextAsync();
        var page = await browserContext.NewPageAsync();

        await page.GotoAsync("http://eaapp.somee.com");
    }

    [Test]
    public async Task TestWithChromium()
    {
        var playwrightDriver = await Playwright.CreateAsync();
        var browserOption = new BrowserTypeLaunchOptions
        {
            Headless = false,
        };
        var chromium = await playwrightDriver.Chromium.LaunchAsync(browserOption);
        var browserContext = await chromium.NewContextAsync();
        var page = await browserContext.NewPageAsync();

        await page.GotoAsync("http://eaapp.somee.com");
    }

    [Test]
    public async Task TestWithFirefox()
    {
        var playwrightDriver = await Playwright.CreateAsync();
        var browserOption = new BrowserTypeLaunchOptions
        {
            Headless = false,
        };
        var browser = await playwrightDriver["firefox"].LaunchAsync(browserOption);
        var browserContext = await browser.NewContextAsync();
        var page = await browserContext.NewPageAsync();

        await page.GotoAsync("http://eaapp.somee.com");
    }

    [Test]
    public async Task TestWithChromeOrMsEdge()
    {
        var playwrightDriver = await Playwright.CreateAsync();
        var browserOption = new BrowserTypeLaunchOptions
        {
            Headless = false,
            Channel = "msedge" // "chrome", "chrome-beta", "chrome-dev", "chrome-canary", "msedge", "msedge-beta", "msedge-dev", or "msedge-canary"
        };
        var browser = await playwrightDriver["chromium"].LaunchAsync(browserOption);
        var browserContext = await browser.NewContextAsync();
        var page = await browserContext.NewPageAsync();

        await page.GotoAsync("http://eaapp.somee.com");
    }
}
```

