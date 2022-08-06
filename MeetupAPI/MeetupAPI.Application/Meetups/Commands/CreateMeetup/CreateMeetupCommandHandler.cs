using MediatR;
using MeetupAPI.Domain;
using MeetupAPI.Application.Interfaces;

namespace MeetupAPI.Application.Meetups.Commands.CreateMeetup;

public class CreateMeetupCommandHandler : IRequestHandler<CreateMeetupCommand, Guid>
{
    private readonly IMeetupsDbContext _context;

    public CreateMeetupCommandHandler(IMeetupsDbContext context) => _context = context;
    
    public async Task<Guid> Handle(CreateMeetupCommand request, CancellationToken cancellationToken)
    {
        Meetup meetup = new Meetup
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
            Organizer = request.Organizer,
            Place = request.Place,
            Date = request.Date
        };

        await _context.Meetups.AddAsync(meetup, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return meetup.Id;
    }
}