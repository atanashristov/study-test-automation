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
