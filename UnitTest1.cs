using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;

namespace PlaywrightTests;

[TestClass]
public class ExampleTest : PageTest
{
    private readonly string RenameHeaderFunc = @"function changeMainHeaderName() {document.querySelector('H2').innerHTML = 'NEWNAME';}";

    [TestMethod]
    public async Task AddInitScript()
    {
        await Page.AddInitScriptAsync(script: RenameHeaderFunc);
        await Page.GotoAsync("https://openmaqs.github.io/TestingSite/Automation/");
        await Page.AddInitScriptAsync(script: RenameHeaderFunc);
        await Page.ReloadAsync();
        await Page.EvaluateAsync("changeMainHeaderName();");
    }

    [TestMethod]
    public async Task AddSciptTag()
    {
        await Page.GotoAsync("https://openmaqs.github.io/TestingSite/Automation/");
        await Page.AddScriptTagAsync(new PageAddScriptTagOptions() { Content = RenameHeaderFunc });
        await Page.EvaluateAsync("changeMainHeaderName();");
        Assert.AreEqual("NEWNAME", await Page.InnerTextAsync("H2"));
    } 
}