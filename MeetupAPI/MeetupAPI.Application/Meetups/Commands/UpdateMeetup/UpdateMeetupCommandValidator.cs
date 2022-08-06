using FluentValidation;

namespace MeetupAPI.Application.Meetups.Commands.UpdateMeetup;

public class UpdateMeetupCommandValidator : AbstractValidator<UpdateMeetupCommand>
{
    public UpdateMeetupCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEqual(Guid.Empty);
        
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