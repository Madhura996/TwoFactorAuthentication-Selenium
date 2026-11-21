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
<img width="788" height="104" alt="image" src="https://github.com/user-attachments/assets/de578148-5c50-4ddc-bb82-0447d1fa1f49" />

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
2. Sign in to the account and open **Security**.
3. Select **Account security** and then **Manage how I sign in**.
4. <img width="1045" height="256" alt="image" src="https://github.com/user-attachments/assets/7bba20c2-6263-4ffd-b854-c5e6ea2b9b4f" />
5. Under **Additional security**, select **Turn on two-step verification**.
   <img width="863" height="150" alt="image" src="https://github.com/user-attachments/assets/0ed22cc8-30ac-4a5f-b633-51a4cfa016c0" />
6. Select **Next**.
   <img width="1125" height="237" alt="image" src="https://github.com/user-attachments/assets/0ed03760-f420-4520-bb94-80cc0638fdf5" />
7. Choose **Authenticator app** as the verification method.
8. A QR code will appear. Choose **I can't scan the QR code**.
9. Microsoft will display a secret key. Copy and securely save this key, then set it as `TOTP_SECRET` in your system environment.
10. Generate the current TOTP in the test from the secret key. Use the generated code to complete the Microsoft setup flow.
   ```csharp
   using OtpNet;
   var secret = Environment.GetEnvironmentVariable("TOTP_SECRET")
       ?? throw new InvalidOperationException("Missing TOTP_SECRET environment variable.");
   var totp = new Totp(Base32Encoding.ToBytes(secret));
   var code = totp.ComputeTotp();
   ```
11. Your Microsoft account will then be configured for two-step verification.

## Browser configuration
`local.runsettings` configures the account URL and browser. Chrome is the default:
```xml
<TestRunParameters>
  <Parameter name="Url" value="https://account.microsoft.com/" />
  <Parameter name="Platform" value="Chrome" />
</TestRunParameters>
```
To use another browser, change `Platform` to `Edge`, `Firefox`, or `Safari`. When running from the command line, retain `--settings local.runsettings`.
