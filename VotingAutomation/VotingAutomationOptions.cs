using OpenQA.Selenium;

namespace VotingAutomation;

public record VotingAutomationOptions(string RegistrarUsername, string RegistrarPassword, IWebDriver WebDriver,
    string RegistrarPageUrl);