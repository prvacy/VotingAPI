using OpenQA.Selenium.Chrome;
using VotingAutomation;
using VotingModels;


var driver = new ChromeDriver();

var mainUrl = "http://localhost:11607/BD001607.html";

var automator = new VotingAutomator(
    new VotingAutomationOptions("Володимир Вишняков", "z1234567", driver, mainUrl));

var voter = new Voter("AA000078", "КСМ-41", "Новий", "Виборець",
    "Володимирович", "vyborets@gamil.com", DateTime.Now.AddYears(-25));

automator.GetVotersList();

automator.SoftDeleteVoter("AA000078");

automator.RegisterVoter(voter);