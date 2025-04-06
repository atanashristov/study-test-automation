
using System.Runtime.ConstrainedExecution;
using Microsoft.Playwright;
using PlaywrightDemo.Config;

namespace PlaywrightDemo.Driver
{
  class PlaywrightDriver
  {
    public async Task<IPage> InitializePlaywrightAsync(TestSettings testSettings)
    {
      var browser = await GetBrowserAsync(testSettings);
      var browserContext = await browser.NewContextAsync();
      var page = await browserContext.NewPageAsync();

      await page.GotoAsync("http://eaapp.somee.com");

      return page;
    }

    public async Task<IBrowser> GetBrowserAsync(TestSettings testSettings)
    {
      var playwrightDriver = await Playwright.CreateAsync();
      var channel = testSettings.DriverType switch
      {
        DriverType.Chrome => "chrome",
        DriverType.Edge => "msedge",
        _ => null,
      };

      var browserOption = new BrowserTypeLaunchOptions
      {
        Channel = channel,
        Headless = testSettings.Headless,
        SlowMo = testSettings.SlowMo,
      };

      var browserType = testSettings.DriverType switch
      {
        DriverType.Chrome => playwrightDriver.Chromium,
        DriverType.Chromium => playwrightDriver.Chromium,
        DriverType.Edge => playwrightDriver.Chromium,
        DriverType.Firefox => playwrightDriver.Firefox,
        DriverType.WebKit => playwrightDriver.Webkit,
        _ => playwrightDriver.Chromium,
      };

      var browser = await browserType.LaunchAsync(browserOption);
      return browser;
    }

  }
}


