using OpenQA.Selenium;

namespace TwoFactorAuthentication_Selenium.PageObjectModel.Pages
{
    public class AccountHomePage : BasePage
    {
        public readonly By Account;

        public AccountHomePage(IWebDriver driver) : base(driver, string.Empty)
        {
            Account = By.CssSelector("#home-breadcrumb-account");
        }
    }
}
