using OpenQA.Selenium;

namespace TwoFactorAuthentication_Selenium.PageObjectModel.Pages
{
    public class MicrosoftAccountLoginPage(IWebDriver driver) : BasePage(driver, string.Empty)
    {
        public readonly By Useremail = By.CssSelector("#i0116");
        public readonly By Next = By.CssSelector("#idSIButton9");
    }
}
