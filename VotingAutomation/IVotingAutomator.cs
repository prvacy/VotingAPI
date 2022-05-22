using VotingModels;

namespace VotingAutomation;

public interface IVotingAutomator
{
    ResultCodes RegisterVoter(Voter voter);
    ResultCodes SoftDeleteVoter(string voterId);
    IEnumerable<Voter> GetVotersList();
}