using OtpNet;
using TwoFactorAuthentication_Selenium.PageObjectModel.Pages;

namespace TwoFactorAuthentication_Selenium
{
    public class TwoFactorAuthenticationTest : BaseTest
    {
        [Test]
        public void LoginToMicrosoftAccountAndVerifyTwoFactorAuthentication()
        {
            // Go to the Microsoft Account landing page
            LandingPage landingPage = new(driver, BaseUrl);
            landingPage.GoToPage();
            landingPage.WindowMaximize();
            Assert.That(landingPage.GetUrl(), Contains.Substring(BaseUrl));
            landingPage.Click(landingPage.SignInButton);

            // Enter the user email on the login page
            MicrosoftAccountLoginPage msAccountLoginPage = new(driver);
            Assert.That(msAccountLoginPage.GetUrl(), Contains.Substring("https://login.microsoftonline.com/"));
            msAccountLoginPage.SetText(msAccountLoginPage.Useremail, Useremail);
            msAccountLoginPage.Click(msAccountLoginPage.Next);

            // Enter the user password on the login live page
            MicrosoftLoginLivePage msLoginLivePage = new(driver);
            Assert.That(msLoginLivePage.GetUrl(), Contains.Substring("https://login.live.com/"));
            msLoginLivePage.SetText(msLoginLivePage.Password, Password);
            msLoginLivePage.Click(msLoginLivePage.Login);

            // Generate and Enter the two-factor authentication code
            msLoginLivePage.WaitUntilLocatorVisible(msLoginLivePage.Title);
            var code = getTwoFactorCode();
            msLoginLivePage.SetText(msLoginLivePage.Code, code);
            msLoginLivePage.Click(msLoginLivePage.Submit);

            // Verify that the user is logged in and redirected to the account home page
            AccountHomePage accountHomePage = new(driver);
            accountHomePage.WaitUntilLocatorVisible(accountHomePage.Account);
            Assert.That(accountHomePage.GetUrl(), Contains.Substring("https://account.microsoft.com/"));
        }

        private static string getTwoFactorCode()
        {
            TestContext.Out.WriteLine("Retrieving security code");
            var secretKey = Environment.GetEnvironmentVariable("AUTH_SECRET_KEY");
            var totp = new Totp(Base32Encoding.ToBytes(secretKey));
            var otp = totp.ComputeTotp();
            TestContext.Out.WriteLine(otp);
            return otp;
        }
    }
}
