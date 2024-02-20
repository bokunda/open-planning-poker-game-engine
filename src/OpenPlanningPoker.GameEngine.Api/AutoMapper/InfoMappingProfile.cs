namespace OpenPlanningPoker.GameEngine.Api.AutoMapper;

public class InfoMappingProfile : Profile
{
    public InfoMappingProfile()
    {
        CreateMap<Models.Info.GetInfoQuery, GetInfoQuery>().ReverseMap();
        CreateMap<Models.Info.GetInfoResponse, GetInfoResponse>().ReverseMap();
    }
}