using OpenQA.Selenium;

namespace TwoFactorAuthentication_Selenium.PageObjectModel.Pages
{
    public abstract class BasePage
    {
        protected IWebDriver driver { get; set; }
        protected string url { get; set; }

        public BasePage(IWebDriver driver, string url)
        {
            this.driver = driver;
            this.url = url;
        }

        protected void GoToPage()
        {
            driver.Navigate().GoToUrl(url);
        }

        protected void WindowMaximize()
        {
            driver.Manage().Window.Maximize();
        }

    }
}
