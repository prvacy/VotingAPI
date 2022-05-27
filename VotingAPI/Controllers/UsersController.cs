using Microsoft.AspNetCore.Mvc;
using VotingAutomation;
using VotingModels;

namespace VotingAPI.Controllers;

[ApiController]
[Route("[controller]/Index")]
public class UsersController : ControllerBase
{

    private readonly ILogger<UsersController> _logger;
    private readonly IVotingAutomator _automator;

    public UsersController(ILogger<UsersController> logger, IVotingAutomator automator)
    {
        _logger = logger;
        _automator = automator;
    }

    [HttpGet]
    public IEnumerable<Voter> Get()
    {
        return _automator.GetVotersList();
    }

    [HttpPost]
    public void Post(Voter voter)
    {
        _automator.RegisterVoter(voter);
    }

    [HttpDelete]
    public void Delete(string voterId)
    {
        _automator.SoftDeleteVoter(voterId);
    }
    
    


}