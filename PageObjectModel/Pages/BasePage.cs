using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace TwoFactorAuthentication_Selenium.PageObjectModel.Pages
{
    public abstract class BasePage(IWebDriver driver, string url)
    {
        protected IWebDriver driver { get; set; } = driver;
        protected string url { get; set; } = url;

        public void GoToPage()
        {
            driver.Navigate().GoToUrl(url);
        }

        public void WindowMaximize()
        {
            driver.Manage().Window.Maximize();
        }

        public string GetUrl()
        {
            return driver.Url;
        }

        public void WaitUntilLocatorVisible(By locator)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                wait.Until(ExpectedConditions.ElementIsVisible(locator));
            }
            catch (NoSuchElementException)
            {
                return;
            }
            catch (StaleElementReferenceException)
            {
                return;
            }
            catch (WebDriverTimeoutException)
            {
                return;
            }
        }   
        
        public void Click(By locator)
        {
            WaitUntilLocatorVisible(locator);
            driver.FindElement(locator).Click();
        }

        public void SetText(By locator, string text)
        {
            WaitUntilLocatorVisible(locator);
            driver.FindElement(locator).SendKeys(text);
        }

    }
}
