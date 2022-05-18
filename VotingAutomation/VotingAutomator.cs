using VotingModels;

namespace VotingAutomation;

public class VotingAutomator
{
    private readonly RegistrarPage _page;
    private readonly string _username;
    private readonly string _pass;

    public VotingAutomator(VotingAutomationOptions options)
    {
        _page = new RegistrarPage(options.WebDriver, options.RegistrarPageUrl);
        _username = options.RegistrarUsername;
        _pass = options.RegistrarPassword;
    }

    public ResultCodes RegisterVoter(Voter voter)
    {
        _page.Navigate();

        _page.EnterCredentials(_username, _pass);

        _page.SelectAction(RegistrarActions.Register);

        _page.AcceptAlert();

        //Enter voter id
        _page.AcceptAlert(voter.Id);

        var confirmationText = _page.AcceptAlert();
        if (confirmationText == "Цього виборця вже введено.\r\n")
        {
            return ResultCodes.UserExists;
        }

        _page.FillVoterData(voter);
        _page.SendDataButton.Click();

        //Wait until data is accepted
        _page.AcceptAlert();

        return ResultCodes.Success;
    }

    public ResultCodes SoftDeleteVoter(string voterId)
    {
        _page.Navigate();

        _page.EnterCredentials(_username, _pass);

        _page.SelectAction(RegistrarActions.SoftDelete);

        _page.AcceptAlert();

        //Enter voter id
        _page.AcceptAlert(voterId);

        var result = _page.AcceptAlert();
        return result == "Дані про виборця видалено з реєстру.\r\n" ? ResultCodes.Success : ResultCodes.Error;
    }

    public IEnumerable<Voter> GetVotersList()
    {
        _page.Navigate();
        
        _page.EnterCredentials(_username, _pass);
        
        _page.SelectAction(RegistrarActions.VotersList);

        _page.AcceptAlert();

        var result = _page.AcceptAlert();
        //Skip the first informing row
        var rows = result.Split("\r\n", StringSplitOptions.RemoveEmptyEntries).Skip(1);
        var votersList = new List<Voter>();
        foreach (var row in rows)
        {
            var voterData = row.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var voter = new Voter(voterData[0]);
            voter = voter with {Surname = voterData[1], Name = voterData[2], MiddleName = voterData[3]};
            votersList.Add(voter);
        }

        return votersList;
    }
    
    
}