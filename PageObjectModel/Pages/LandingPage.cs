using OpenQA.Selenium;

namespace TwoFactorAuthentication_Selenium.PageObjectModel.Pages
{
    public class LandingPage : BasePage
    {        
        public readonly By SignInButton;

        public LandingPage(IWebDriver driver, string url) : base(driver, "https://account.microsoft.com/")
        {
            SignInButton = By.CssSelector("button[data-bi-id='signedout.hero.signIn']");
        }
    }
}
