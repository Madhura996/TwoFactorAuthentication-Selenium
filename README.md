# Microsoft Two-Factor Authentication Selenium Test
This repository contains a .NET/NUnit Selenium project for automating Microsoft account sign-in and two-factor authentication. It uses the Page Object Model (POM) pattern to separate test logic from Microsoft page locators and browser setup.
> **Security warning:** Keep account passwords and TOTP secret keys out of source code and commit history. The secret key can generate authentication codes, so treat it like a password.
## Project structure
```text
TwoFactorAuthentication-Selenium/
|-- PageObjectModel/
|   `-- Pages/
|       |-- BasePage.cs                 # Common page properties and navigation
|       |-- LandingPage.cs             # Microsoft account landing page
|       |-- MicrosoftAccountLoginPage.cs
|       |-- MicrosoftLoginLivePage.cs
|       `-- AccountHomePage.cs
|-- UI Tests/
|   |-- BaseTest.cs                    # Browser setup and environment variables
|   `-- TwoFactorAuthenticationTest.cs # NUnit test
|-- local.runsettings                   # Test URL and browser configuration
|-- TwoFactorAuthentication-Selenium.csproj
`-- README.md
```
### Page Object Model
- `BasePage` stores the Selenium `IWebDriver` and target URL. Shared behavior such as navigation and window maximization belongs here.
- Classes under `PageObjectModel/Pages` represent a page or step in the Microsoft sign-in flow. Each class stores locators such as CSS selectors and remains independent of NUnit test setup.
- `BaseTest` creates the selected local browser, exposes test settings, and cleans up the browser after each test.
- `TwoFactorAuthenticationTest` contains the NUnit test scenario. Additional login actions and assertions should be implemented in this class or, for larger flows, in dedicated page actions.
## Prerequisites
- .NET 10 SDK
- Google Chrome, Microsoft Edge, Firefox, or Safari
- A Microsoft/Outlook account
- The matching browser driver available in your `PATH` or installed by Selenium Manager
The project uses Selenium WebDriver, NUnit, and Otp.NET. Restore and run them with:
```powershell
dotnet restore
dotnet test --settings local.runsettings
```
## Environment variables
Set the following system environment variables before running the test:
| Variable       | Required       | Description                     |
| `USEREMAIL`    | Yes            | Microsoft account email address |
| `USERPASSWORD` | Yes            | Microsoft account password      |
| `TOTP_SECRET`  | For TOTP login | Base32 secret key obtained while configuring Microsoft Authenticator |
| `URL`          | No             | Overrides the `Url` run setting. The default is `https://account.microsoft.com/` |
Example for PowerShell:
```powershell
[Environment]::SetEnvironmentVariable("USEREMAIL", "your-email@outlook.com", "User")
[Environment]::SetEnvironmentVariable("USERPASSWORD", "your-password", "User")
[Environment]::SetEnvironmentVariable("TOTP_SECRET", "your-base32-secret", "User")
```
Close and reopen the terminal or Visual Studio after setting user-level environment variables. Verify them in the current PowerShell session:
```powershell
echo $env:USEREMAIL
echo $env:TOTP_SECRET
```
Do not include secret values in `local.runsettings` or commit them to the repository.
## Microsoft account two-factor authentication setup
1. Create a Microsoft/Outlook account with an email address and password.
   ![Microsoft account creation screenshot placeholder](./screenshots/01-create-account.png)
2. Sign in to the account and open **Security**.
   ![Account security screenshot placeholder](./screenshots/02-open-security.png)
3. Select **Account security** and then **Manage how I sign in**.
   ![Manage sign-in methods screenshot placeholder](./screenshots/03-manage-sign-in.png)
4. Under **Additional security**, select **Turn on two-step verification**.
   ![Turn on two-step verification screenshot placeholder](./screenshots/04-turn-on-verification.png)
5. Select **Next**.
   ![Next screenshot placeholder](./screenshots/05-next.png)
6. Choose **Authenticator app** as the verification method.
   ![Authenticator app setup screenshot placeholder](./screenshots/06-authenticator-app.png)
7. A QR code will appear. Choose **I can't scan the QR code**.
   ![Cannot scan QR code screenshot placeholder](./screenshots/07-cannot-scan.png)
8. Microsoft will display a secret key. Copy and securely save this key, then set it as `TOTP_SECRET` in your system environment.
   ![Secret key screenshot placeholder](./screenshots/08-secret-key.png)
9. Generate the current TOTP in the test from the secret key. Use the generated code to complete the Microsoft setup flow.
   ```csharp
   using OtpNet;
   var secret = Environment.GetEnvironmentVariable("TOTP_SECRET")
       ?? throw new InvalidOperationException("Missing TOTP_SECRET environment variable.");
   var totp = new Totp(Base32Encoding.ToBytes(secret));
   var code = totp.ComputeTotp();
   ```
10. Your Microsoft account will then be configured for two-step verification.
    ![Two-factor verification complete screenshot placeholder](./screenshots/10-verification-complete.png)
The screenshots directory is intentionally included as a placeholder. Add the images and keep the existing filenames, or update the links with your own filenames.
## Browser configuration
`local.runsettings` configures the account URL and browser. Chrome is the default:
```xml
<TestRunParameters>
  <Parameter name="Url" value="https://account.microsoft.com/" />
  <Parameter name="Platform" value="Chrome" />
</TestRunParameters>
```
To use another browser, change `Platform` to `Edge`, `Firefox`, or `Safari`. When running from the command line, retain `--settings local.runsettings`.