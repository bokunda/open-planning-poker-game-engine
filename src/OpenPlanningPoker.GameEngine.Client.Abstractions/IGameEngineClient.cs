namespace OpenPlanningPoker.GameEngine.Client.Abstractions;

public interface IGameEngineClient
{
    IGameResource GameResource { get; }
    IGameSettingsResource GameSettingsResource { get; }
    ITicketResource TicketResource { get; }
    IVoteResource VoteResource { get; }
}