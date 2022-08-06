using MediatR;

namespace MeetupAPI.Application.Meetups.Queries.GetMeetupList;

public class GetMeetupListQuery : IRequest<MeetupListViewModel>
{
    public Guid UserId { get; set; }
}