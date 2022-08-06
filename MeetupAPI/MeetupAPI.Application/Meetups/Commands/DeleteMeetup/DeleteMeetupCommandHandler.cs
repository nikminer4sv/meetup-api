using System.Diagnostics.CodeAnalysis;
using MediatR;
using MeetupAPI.Application.Common.Exceptions;
using MeetupAPI.Application.Interfaces;
using MeetupAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace MeetupAPI.Application.Meetups.Commands.DeleteMeetup;

public class DeleteMeetupCommandHandler : IRequestHandler<DeleteMeetupCommand>
{
    private readonly IMeetupsDbContext _context;

    public DeleteMeetupCommandHandler(IMeetupsDbContext context) => _context = context;
    
    public async Task<Unit> Handle(DeleteMeetupCommand request, CancellationToken cancellationToken)
    {
        Meetup? meetup = await _context.Meetups.FirstOrDefaultAsync(meetup => meetup.Id == request.Id);

        if (meetup == null || meetup.UserId != request.UserId)
        {
            throw new NotFoundException(nameof(Meetup), request.Id);
        }

        _context.Meetups.Remove(meetup);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}