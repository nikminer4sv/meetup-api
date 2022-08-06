using FluentValidation;

namespace MeetupAPI.Application.Meetups.Commands.DeleteMeetup;

public class DeleteMeetupCommandValidator : AbstractValidator<DeleteMeetupCommand>
{
    public DeleteMeetupCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEqual(Guid.Empty);

        RuleFor(command => command.UserId)
            .NotEqual(Guid.Empty);
    }    
}