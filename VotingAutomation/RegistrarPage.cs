using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using VotingModels;

namespace VotingAutomation;

public class RegistrarPage
{
    private readonly string _loginName;
    private string _loginButtonXPath = "//*[@id='REGI']/option[text()='{}']";

    private const string PasswordInputXPath = "//input[@value='Пароль введено']";
    
    public IWebElement LoginButton { get; set; }
    
    [FindsBy(How.XPath, PasswordInputXPath)]
    public IWebDriver PasswordInput { get; set; }

    private readonly IWebDriver _driver;
    
    public RegistrarPage(string loginName, IWebDriver webDriver, string pageUrl)
    {
        _driver = webDriver;
        
        _loginName = loginName;
        _loginButtonXPath = $"//*[@id='REGI']/option[text()='{loginName}']";

        
        _driver.Navigate().GoToUrl(pageUrl);
        
        LoginButton = _driver.FindElement(By.XPath($"//*[@id='REGI']/option[text()='{loginName}']"));
        
        PageFactory.InitElements(_driver, this);
    }
    
    public void RegisterVoter(Voter voter)
    {
        var driver = new ChromeDriver();

        var mainUrl = "http://localhost:11607/BD001607.html";
        
        driver.Navigate().GoToUrl(mainUrl);

        var login = "Володимир Вишняков";
        var pass = "z1234567";
        
        var loginItem = driver.FindElement(By.XPath($"//*[@id='REGI']/option[text()='{login}']"));
        loginItem.Click();
        
        var passInput = driver.FindElement(By.Id("PWREG"));
        passInput.SendKeys(pass);

        var confirmButton = driver.FindElement(By.XPath("//input[@value='Пароль введено']"));
        confirmButton.Click();
        //Click ok on alert

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        wait.Until(ExpectedConditions.AlertIsPresent());
        driver.SwitchTo().Alert().Accept();
        
        //Enter Id
        wait.Until(ExpectedConditions.AlertIsPresent());
        var alert = driver.SwitchTo().Alert();
        alert.SendKeys(voter.Id);
        alert.Accept();
        
        //wait for confirmation
        wait.Until(ExpectedConditions.AlertIsPresent());
        driver.SwitchTo().Alert().Accept();
        
        //Fill additional data
        var email = driver.FindElement(By.Id("EMAIL"));
        email.Clear();
        email.SendKeys(voter.Email);

        var sName = driver.FindElement(By.Id("FAMIL"));
        sName.Clear();
        sName.SendKeys(voter.Surname);

        var name = driver.FindElement(By.Id("IMJA"));
        name.Clear();
        name.SendKeys(voter.Name);

        var mName = driver.FindElement(By.Id("OTCH"));
        mName.Clear();
        mName.SendKeys(voter.MiddleName);

        var year = driver.FindElement(By.Id("RIK"));
        year.Clear();
        year.SendKeys("2000");

        var day = driver.FindElement(By.Id("CHISLO"));
        day.Clear();
        day.SendKeys("01");

        //Send data
        var sendDataButton = driver.FindElement(By.XPath("//input[@value='Відправити дані до реєстру']"));
        sendDataButton.Click();
        
        //Wait until data is written to register
        wait.Until(ExpectedConditions.AlertIsPresent());
        alert = driver.SwitchTo().Alert();
        //console.log(alert.getText());
        alert.Accept();
        //"Дані відхілено, бо така електронна пошта вже є."
    }
}