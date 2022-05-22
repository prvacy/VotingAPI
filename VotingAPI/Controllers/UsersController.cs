using Microsoft.AspNetCore.Mvc;
using VotingAutomation;
using VotingModels;

namespace VotingAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<UsersController> _logger;
    private readonly IVotingAutomator _automator;

    public UsersController(ILogger<UsersController> logger, IVotingAutomator automator)
    {
        _logger = logger;
        _automator = automator;
    }

    [HttpPost("Index")]
    public void Post(Voter voter)
    {
        _automator.RegisterVoter(voter);
    }
    
    


}