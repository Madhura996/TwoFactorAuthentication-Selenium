using OpenQA.Selenium;

namespace TwoFactorAuthentication_Selenium.PageObjectModel.Pages
{
    public class MicrosoftAccountLoginPage : BasePage
    {
        public readonly By Useremail;
        public readonly By Next;

        public MicrosoftAccountLoginPage(IWebDriver driver) : base(driver, string.Empty)
        {
            Useremail = By.CssSelector("#i0116");
            Next = By.CssSelector("#idSIButton9");
        }
    }
}
