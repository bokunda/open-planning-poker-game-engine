// Global using directives

global using AutoMapper;
global using System.Globalization;
global using System.Reflection;
global using OpenPlanningPoker.GameEngine.Api.Extensions;
global using OpenPlanningPoker.GameEngine.Api.Health;
global using OpenPlanningPoker.GameEngine.Api.Middleware;
global using OpenPlanningPoker.GameEngine.Application;
global using OpenPlanningPoker.GameEngine.Application.Exceptions;
global using OpenPlanningPoker.GameEngine.Infrastructure;
global using HealthChecks.UI.Client;
global using MediatR;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Diagnostics.HealthChecks;
global using OpenPlanningPoker.GameEngine.Application.Features.Games;
global using OpenPlanningPoker.GameEngine.Application.Features.GamePlayer;
global using OpenPlanningPoker.GameEngine.Application.Features.GameSettings;
global using OpenPlanningPoker.GameEngine.Application.Features.Tickets;
global using OpenPlanningPoker.GameEngine.Application.Features.Votes;
global using OpenPlanningPoker.GameEngine.Application.Info.GetInfo;
global using OpenPlanningPoker.GameEngine.Application.Paging;
global using OpenPlanningPoker.GameEngine.Domain.Identity;
global using Serilog;

global using JoinGameResponseApi = OpenPlanningPoker.GameEngine.Api.Models.Features.GamePlayer.JoinGameResponse;
global using LeaveGameResponseApi = OpenPlanningPoker.GameEngine.Api.Models.Features.GamePlayer.LeaveGameResponse;
global using CreateGameResponseApi = OpenPlanningPoker.GameEngine.Api.Models.Features.Games.CreateGameResponse;
global using GetGameResponseApi = OpenPlanningPoker.GameEngine.Api.Models.Features.Games.GetGameResponse;
global using CreateGameSettingsCommandApi = OpenPlanningPoker.GameEngine.Api.Models.Features.GameSettings.CreateGameSettingsCommand;
global using CreateGameSettingsResponseApi = OpenPlanningPoker.GameEngine.Api.Models.Features.GameSettings.CreateGameSettingsResponse;
global using GetGameSettingsResponseApi = OpenPlanningPoker.GameEngine.Api.Models.Features.GameSettings.GetGameSettingsResponse;
global using UpdateGameSettingsCommandApi = OpenPlanningPoker.GameEngine.Api.Models.Features.GameSettings.UpdateGameSettingsCommand;
global using UpdateGameSettingsResponseApi = OpenPlanningPoker.GameEngine.Api.Models.Features.GameSettings.UpdateGameSettingsResponse;
global using GetInfoResponseApi = OpenPlanningPoker.GameEngine.Api.Models.Info.GetInfoResponse;
global using CreateTicketCommandApi = OpenPlanningPoker.GameEngine.Api.Models.Features.Tickets.CreateTicketCommand;
global using CreateTicketResponseApi = OpenPlanningPoker.GameEngine.Api.Models.Features.Tickets.CreateTicketResponse;
global using GetTicketResponseApi = OpenPlanningPoker.GameEngine.Api.Models.Features.Tickets.GetTicketResponse;
global using ImportTicketsCommandApi = OpenPlanningPoker.GameEngine.Api.Models.Features.Tickets.ImportTicketsCommand;
global using DeleteTicketResponseApi = OpenPlanningPoker.GameEngine.Api.Models.Features.Tickets.DeleteTicketResponse;
global using CreateVoteResponseApi = OpenPlanningPoker.GameEngine.Api.Models.Features.Votes.CreateVoteResponse;
global using CreateVoteCommandApi = OpenPlanningPoker.GameEngine.Api.Models.Features.Votes.CreateVoteCommand;
global using UpdateVoteResponseApi = OpenPlanningPoker.GameEngine.Api.Models.Features.Votes.UpdateVoteResponse;
global using UpdateVoteCommandApi = OpenPlanningPoker.GameEngine.Api.Models.Features.Votes.UpdateVoteCommand;
global using ListPlayersPlayerItemApi = OpenPlanningPoker.GameEngine.Api.Models.Features.GamePlayer.ListPlayersItem;
