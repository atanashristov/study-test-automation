using Microsoft.Playwright;
using PlaywrightDemo.Config;
using PlaywrightDemo.Driver;

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
        var testSettings = new TestSettings
        {
            DriverType = DriverType.Chrome,
            Headless = false,
            SlowMo = 1500,
        };

        var playwrightDriver = new PlaywrightDriver();
        var page = await playwrightDriver.InitializePlaywrightAsync(testSettings);

        await page.ClickAsync("text=Login");
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
            Channel = "chrome" // "chrome", "chrome-beta", "chrome-dev", "chrome-canary", "msedge", "msedge-beta", "msedge-dev", or "msedge-canary"
        };
        var browser = await playwrightDriver["chromium"].LaunchAsync(browserOption);
        var browserContext = await browser.NewContextAsync();
        var page = await browserContext.NewPageAsync();

        await page.GotoAsync("http://eaapp.somee.com");
    }


}
