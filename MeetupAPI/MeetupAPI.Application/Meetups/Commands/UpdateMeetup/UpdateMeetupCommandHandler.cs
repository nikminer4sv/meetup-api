using MediatR;
using MeetupAPI.Application.Common.Exceptions;
using MeetupAPI.Application.Interfaces;
using MeetupAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace MeetupAPI.Application.Meetups.Commands.UpdateMeetup;

public class UpdateMeetupCommandHandler : IRequestHandler<UpdateMeetupCommand>
{
    private readonly IMeetupsDbContext _context;

    public UpdateMeetupCommandHandler(IMeetupsDbContext context) => _context = context;
    
    public async Task<Unit> Handle(UpdateMeetupCommand request, CancellationToken cancellationToken)
    {
        Meetup? meetup = await _context.Meetups.FirstOrDefaultAsync(meetup => meetup.Id == request.Id, cancellationToken);

        if (meetup == null || meetup.UserId != request.UserId)
        {
            throw new NotFoundException(nameof(Meetup), request.Id);
        }

        meetup.Title = request.Title;
        meetup.Description = request.Description;
        meetup.Organizer = request.Organizer;
        meetup.Place = request.Place;
        meetup.Date = request.Date;
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}