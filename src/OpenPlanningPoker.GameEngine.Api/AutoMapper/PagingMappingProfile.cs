namespace OpenPlanningPoker.GameEngine.Api.AutoMapper;

public class PagingMappingProfile<T> : Profile
{
    public PagingMappingProfile()
    {
        CreateMap<Models.Paging.PagedRequest<T>, PagedRequest<T>>().ReverseMap();
        CreateMap<Models.Paging.PagedResponse<T>, PagedResponse<T>>().ReverseMap();
    }
}