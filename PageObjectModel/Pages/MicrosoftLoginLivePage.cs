using OpenQA.Selenium;

namespace TwoFactorAuthentication_Selenium.PageObjectModel.Pages
{
    public class MicrosoftLoginLivePage : BasePage
    {
        public readonly By Password;
        public readonly By Login;
        public readonly By Code;
        public readonly By Submit;

        public MicrosoftLoginLivePage(IWebDriver driver, string url) : base(driver, url)
        {
            Password = By.CssSelector("#passwordEntry");
            Login = By.CssSelector("button[data-testid='primaryButton']");
            Code = By.CssSelector("#floatingLabelInput5");
            Submit = By.CssSelector("button[data-testid='primaryButton']");
        }
    }
   
}
