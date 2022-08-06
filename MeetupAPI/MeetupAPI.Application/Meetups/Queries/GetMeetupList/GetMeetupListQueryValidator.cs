using FluentValidation;
using MeetupAPI.Application.Meetups.Queries.GetMeetupDetails;

namespace MeetupAPI.Application.Meetups.Queries.GetMeetupList;

public class GetMeetupListQueryValidator : AbstractValidator<GetMeetupDetailsQuery>
{
    public GetMeetupListQueryValidator()
    {
        RuleFor(query => query.UserId)
            .NotEqual(Guid.Empty);
    }   
}