namespace OpenPlanningPoker.GameEngine.Api.AutoMapper;

public class TicketMappingProfile : Profile
{
    public TicketMappingProfile()
    {
        CreateMap<Models.Features.Tickets.CreateTicketResponse, CreateTicketResponse>().ReverseMap();
        CreateMap<Models.Features.Tickets.CreateTicketCommand, CreateTicketCommand>().ReverseMap();

        CreateMap<Models.Features.Tickets.UpdateTicketResponse, UpdateTicketResponse>().ReverseMap();
        CreateMap<Models.Features.Tickets.UpdateTicketCommand, UpdateTicketCommand>().ReverseMap();

        CreateMap<Models.Features.Tickets.DeleteTicketResponse, DeleteTicketResponse>().ReverseMap();
        CreateMap<Models.Features.Tickets.DeleteTicketCommand, DeleteTicketCommand>().ReverseMap();

        CreateMap<Models.Features.Tickets.GetTicketResponse, GetTicketResponse>().ReverseMap();
        CreateMap<Models.Features.Tickets.GetTicketQuery, GetTicketQuery>().ReverseMap();
        
        CreateMap<Models.Features.Tickets.GetTicketsItem, GetTicketsItem>().ReverseMap();
        CreateMap<Models.Features.Tickets.GetTicketsQuery, GetTicketsQuery>().ReverseMap();

        CreateMap<Models.Features.Tickets.ImportTicketItem, ImportTicketItem>().ReverseMap();
        CreateMap<Models.Features.Tickets.ImportTicketsCommand, ImportTicketsCommand>().ReverseMap();
    }
}