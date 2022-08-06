using FluentValidation;

namespace MeetupAPI.Application.Meetups.Queries.GetMeetupDetails;

public class GetMeetupDetailsQueryValidator : AbstractValidator<GetMeetupDetailsQuery>
{
    public GetMeetupDetailsQueryValidator()
    {
        RuleFor(query => query.Id)
            .NotEqual(Guid.Empty);

        RuleFor(query => query.UserId)
            .NotEqual(Guid.Empty);
    }
}