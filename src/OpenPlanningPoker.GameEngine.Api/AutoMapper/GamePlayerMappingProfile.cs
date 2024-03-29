﻿namespace OpenPlanningPoker.GameEngine.Api.AutoMapper;

public class GamePlayerMappingProfile : Profile
{
    public GamePlayerMappingProfile()
    {
        CreateMap<Models.Features.GamePlayer.JoinGameResponse, JoinGameResponse>().ReverseMap();
        CreateMap<Models.Features.GamePlayer.JoinGameCommand, JoinGameCommand>().ReverseMap();

        CreateMap<Models.Features.GamePlayer.LeaveGameResponse, LeaveGameResponse>().ReverseMap();
        CreateMap<Models.Features.GamePlayer.LeaveGameCommand, LeaveGameCommand>().ReverseMap();
        
        CreateMap<Models.Features.GamePlayer.ListPlayersItem, ListPlayersItem>().ReverseMap();
        CreateMap<Models.Features.GamePlayer.ListPlayersQuery, ListPlayersQuery>().ReverseMap();
    }
}