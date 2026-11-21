using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;

namespace TwoFactorAuthentication_Selenium
{
    public abstract class BaseTest
    {
        protected static IWebDriver driver;
        protected static string BaseUrl =>
            Environment.GetEnvironmentVariable("URL")
            ?? TestContext.Parameters["Url"]
            ?? throw new InvalidOperationException("Missing test run parameter 'Url'. Ensure that local.runsettings is selected.");
        protected static string Useremail => 
            Environment.GetEnvironmentVariable("USEREMAIL")
            ?? throw new Exception("Missing Microsoft account email address");
        protected static string Password =>
            Environment.GetEnvironmentVariable("USERPASSWORD")
            ?? throw new Exception("Missing Microsoft account password");              

        [SetUp]
        public void TestSetup()
        {
            var platform = TestContext.Parameters["Platform"]
                ?? throw new InvalidOperationException("Missing test run parameter 'Platform'. Ensure that local.runsettings is selected.");
            
            driver = GetLocalDriver(platform);
        }

        [TearDown]
        public void TestCleanup()
        {
            if (driver != null)
            {
                driver.Close();
                driver.Quit();
            }
        }
       
        private IWebDriver GetLocalDriver(string platform)
        {            
            switch (platform)
            {
                case "Chrome":
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--incognito");
                    return new ChromeDriver(chromeOptions);                

                case "Edge":
                    EdgeOptions edgeOptions = new EdgeOptions();
                    edgeOptions.AddArgument("--inprivate");
                    return new EdgeDriver(edgeOptions);

                case "Safari":
                    return new SafariDriver();

                case "Firefox":
                    return new FirefoxDriver();

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
