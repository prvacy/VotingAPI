using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using VotingModels;

namespace VotingAutomation;

public class RegistrarPage
{
    //private readonly string _loginName;
    private const string LoginSelectId = "REGI";   
    private const string ActionSelectId = "REJIM";

    private const string PasswordInputId = "PWREG";
    private const string ConfirmActionButtonXPath = "//input[@value='Пароль введено']";
    
    private const string SendDataButtonXPath = "//input[@value='Відправити дані до реєстру']";

    private const string EmailInputId = "EMAIL";
    private const string SurnameInputId = "FAMIL";
    private const string NameInputId = "IMJA";
    private const string MiddleNameInputId = "OTCH";
    private const string YearInputId = "RIK";
    private const string MonthSelectId = "MIS";
    private const string DayInputId = "CHISLO";
    
    [FindsBy(How.Id, LoginSelectId)]
    public IWebElement LoginSelect { get; set; }
    
    [FindsBy(How.Id, ActionSelectId)]
    public IWebElement ActionSelect { get; set; }
    
    [FindsBy(How.Id,PasswordInputId)]
    public IWebElement PasswordInput { get; set; }
    
    [FindsBy(How.XPath, ConfirmActionButtonXPath)]
    public IWebElement ConfirmActionButton { get; set; }
    
    
    [FindsBy(How.Id, EmailInputId)]
    public IWebElement EmailInput { get; set; }

    [FindsBy(How.Id, SurnameInputId)]
    public IWebElement SurnameInput { get; set; }    
    
    [FindsBy(How.Id, NameInputId)]
    public IWebElement NameInput { get; set; }    
    
    [FindsBy(How.Id, MiddleNameInputId)]
    public IWebElement MiddleNameInput { get; set; }    
    
    [FindsBy(How.Id, YearInputId)]
    public IWebElement YearInput { get; set; }    
    
    [FindsBy(How.Id, DayInputId)]
    public IWebElement DayInput { get; set; }   
    
    [FindsBy(How.Id, MonthSelectId)]
    public IWebElement MonthSelect { get; set; }

    private readonly IEnumerable<IWebElement?> _formInputs;



    [FindsBy(How.XPath, SendDataButtonXPath)]
    public IWebElement SendDataButton { get; set; }
    

    private readonly IWebDriver _driver;
    private readonly string _pageUrl;
    private readonly WebDriverWait _wait;

    public RegistrarPage(IWebDriver webDriver, string pageUrl, int waitTimeout = 10)
    {
        _driver = webDriver;
        _pageUrl = pageUrl;
        PageFactory.InitElements(_driver, this);
        _wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(waitTimeout));

        _formInputs = new List<IWebElement?>()
        {
            EmailInput,
            SurnameInput,
            NameInput,
            MiddleNameInput,
            YearInput,
            DayInput
        };
    }

    public void Navigate()
    {
        _driver.Navigate().GoToUrl(_pageUrl);
    }

    public void EnterCredentials(string loginName, string password)
    {
        var selectElement = new SelectElement(LoginSelect);
        selectElement.SelectByText(loginName);
        
        PasswordInput.Clear();
        PasswordInput.SendKeys(password);
    }

    public void SelectAction(RegistrarActions action)
    {
        var select = new SelectElement(ActionSelect);
        select.SelectByValue(((int)action).ToString());
        
        ConfirmActionButton.Click();
    }

    public string AcceptAlert(string value = "")
    {
        _wait.Until(ExpectedConditions.AlertIsPresent());
        var alert = _driver.SwitchTo().Alert();
        
        if (!string.IsNullOrEmpty(value))
        {
            alert.SendKeys(value);
        }

        var text = alert.Text;
        alert.Accept();
        return text;
    }

    public void ClearFormData()
    {
        foreach (var element in _formInputs)
        {
            element?.Clear();
        }
    }

    public void FillVoterData(Voter voter)
    {
        ClearFormData();
        
        EmailInput.SendKeys(voter.Email);
        SurnameInput.SendKeys(voter.Surname);
        NameInput.SendKeys(voter.Name);
        MiddleNameInput.SendKeys(voter.MiddleName);
        YearInput.SendKeys(voter.BirthdayDate?.Year.ToString());
        DayInput.SendKeys(voter.BirthdayDate?.Day.ToString());
        
        var selectElement = new SelectElement(MonthSelect);
        selectElement.SelectByValue(voter.BirthdayDate?.Month.ToString());
    }
    
}