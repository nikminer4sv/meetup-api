using MediatR;

namespace MeetupAPI.Application.Meetups.Queries.GetMeetupDetails;

public class GetMeetupDetailsQuery : IRequest<MeetupDetailsViewModel>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}