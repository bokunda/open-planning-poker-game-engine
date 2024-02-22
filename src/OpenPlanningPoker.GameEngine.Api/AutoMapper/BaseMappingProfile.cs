namespace OpenPlanningPoker.GameEngine.Api.AutoMapper;

public class BaseMappingProfile : Profile
{
    public BaseMappingProfile()
    {
        CreateMap(typeof(ApiCollection<>), typeof(Models.ApiCollection<>));
    }
}