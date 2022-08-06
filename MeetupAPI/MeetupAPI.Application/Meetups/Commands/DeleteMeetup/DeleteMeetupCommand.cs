using MediatR;

namespace MeetupAPI.Application.Meetups.Commands.DeleteMeetup;

public class DeleteMeetupCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}