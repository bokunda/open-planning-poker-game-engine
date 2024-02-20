namespace OpenPlanningPoker.GameEngine.Api.AutoMapper;

public class VoteMappingProfile : Profile
{
    public VoteMappingProfile()
    {
        CreateMap<Models.Features.Votes.CreateVoteResponse, CreateVoteResponse>().ReverseMap();
        CreateMap<Models.Features.Votes.CreateVoteCommand, CreateVoteCommand>().ReverseMap();

        CreateMap<Models.Features.Votes.GetVotesResponse, GetVotesResponse>().ReverseMap();
        CreateMap<Models.Features.Votes.GetVotesVoteItem, GetVotesVoteItem>().ReverseMap();
        CreateMap<Models.Features.Votes.GetVotesQuery, GetVotesQuery>().ReverseMap();

        CreateMap<Models.Features.Votes.UpdateVoteResponse, UpdateVoteResponse>().ReverseMap();
        CreateMap<Models.Features.Votes.UpdateVoteCommand, UpdateVoteCommand>().ReverseMap();
    }
}