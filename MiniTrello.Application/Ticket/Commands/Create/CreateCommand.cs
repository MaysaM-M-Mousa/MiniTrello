using MediatR;
using MiniTrello.Domain.Primitives.Result;

namespace MiniTrello.Application.Ticket.Commands.Create;

public sealed record CreateCommand : IRequest<Result>;
