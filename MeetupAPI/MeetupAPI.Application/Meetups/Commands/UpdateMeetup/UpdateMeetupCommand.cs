using MediatR;

namespace MeetupAPI.Application.Meetups.Commands.UpdateMeetup;

public class UpdateMeetupCommand : IRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Organizer { get; set; }
    public string Place { get; set; }
    public DateTime Date { get; set; }
}