using FluentValidation;

namespace MeetupAPI.Application.Meetups.Commands.CreateMeetup;

public class CreateMeetupCommandValidator : AbstractValidator<CreateMeetupCommand>
{
    public CreateMeetupCommandValidator()
    {
        RuleFor(command => command.UserId)
            .NotEqual(Guid.Empty);
        
        RuleFor(command => command.Title)
            .NotEmpty()
            .MaximumLength(128);
        
        RuleFor(command => command.Description)
            .NotEmpty()
            .MaximumLength(256);
        
        RuleFor(command => command.Organizer)
            .NotEmpty()
            .MaximumLength(64);
        
        RuleFor(command => command.Place)
            .NotEmpty()
            .MaximumLength(64);
        
        RuleFor(command => command.Date)
            .NotEmpty();
    }
}