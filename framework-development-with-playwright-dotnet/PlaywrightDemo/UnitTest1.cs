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
    public async Task Test2()
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
    public async Task Test3()
    {
        var playwrightDriver = await Playwright.CreateAsync();
        var browserOption = new BrowserTypeLaunchOptions
        {
            Headless = false,
            Channel = "msedge" // or "chrome", or "" for headless
        };
        var browser = await playwrightDriver["chromium"].LaunchAsync(browserOption);
        var browserContext = await browser.NewContextAsync();
        var page = await browserContext.NewPageAsync();

        await page.GotoAsync("http://eaapp.somee.com");
    }
}
