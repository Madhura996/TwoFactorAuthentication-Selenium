using OpenQA.Selenium;

namespace TwoFactorAuthentication_Selenium.PageObjectModel.Pages
{
    public class LandingPage : BasePage
    {        
        public readonly By SignInButton;

        public LandingPage(IWebDriver driver, string url) : base(driver, url)
        {
            SignInButton = By.CssSelector("button[data-bi-id='signedout.hero.signIn']");
        }
    }
}
