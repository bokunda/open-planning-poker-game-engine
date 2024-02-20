namespace OpenPlanningPoker.GameEngine.Api.AutoMapper;

public class GameMappingProfile : Profile
{
    public GameMappingProfile()
    {
        CreateMap<Models.Features.Games.CreateGameResponse, CreateGameResponse>().ReverseMap();
        CreateMap<Models.Features.Games.CreateGameCommand, CreateGameCommand>().ReverseMap();

        CreateMap<Models.Features.Games.DeleteGameResponse, DeleteGameResponse>().ReverseMap();
        CreateMap<Models.Features.Games.DeleteGameCommand, DeleteGameCommand>().ReverseMap();

        CreateMap<Models.Features.Games.GetGameResponse, GetGameResponse>().ReverseMap();
        CreateMap<Models.Features.Games.GetGameQuery, GetGameQuery>().ReverseMap();
    }
}