using OpenQA.Selenium;

namespace TwoFactorAuthentication_Selenium.PageObjectModel.Pages
{
    public class AccountHomePage : BasePage
    {
        public readonly By Account;

        public AccountHomePage(IWebDriver driver, string url) : base(driver, url)
        {
            Account = By.CssSelector("#home-breadcrumb-account");
        }
    }
}
