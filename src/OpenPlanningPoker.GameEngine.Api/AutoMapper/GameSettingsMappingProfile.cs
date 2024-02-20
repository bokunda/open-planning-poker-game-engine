namespace OpenPlanningPoker.GameEngine.Api.AutoMapper;

public class GameSettingsMappingProfile : Profile
{
    public GameSettingsMappingProfile()
    {
        CreateMap<Models.Features.GameSettings.CreateGameSettingsResponse, CreateGameSettingsResponse>().ReverseMap();
        CreateMap<Models.Features.GameSettings.CreateGameSettingsCommand, CreateGameSettingsCommand>().ReverseMap();
        
        CreateMap<Models.Features.GameSettings.GetGameSettingsResponse, GetGameSettingsResponse>().ReverseMap();
        CreateMap<Models.Features.GameSettings.GetGameSettingsQuery, GetGameSettingsQuery>().ReverseMap();

        CreateMap<Models.Features.GameSettings.UpdateGameSettingsResponse, UpdateGameSettingsResponse>().ReverseMap();
        CreateMap<Models.Features.GameSettings.UpdateGameSettingsCommand, UpdateGameSettingsCommand>().ReverseMap();
    }
}