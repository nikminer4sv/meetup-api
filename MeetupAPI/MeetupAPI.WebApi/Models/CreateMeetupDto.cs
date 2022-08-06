using AutoMapper;
using MeetupAPI.Application.Common.Mappings;
using MeetupAPI.Application.Meetups.Commands.CreateMeetup;
using MeetupAPI.Domain;

namespace MeetupAPI.WebApi.Models;

public class CreateMeetupDto : IMapWith<CreateMeetupCommand>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Organizer { get; set; }
    public string Place { get; set; }
    public DateTime Date { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateMeetupDto, CreateMeetupCommand>()
            .ForMember(command => command.Title,
                opt => opt.MapFrom(dest => dest.Title))
            .ForMember(command => command.Description,
                opt => opt.MapFrom(dest => dest.Description))
            .ForMember(command => command.Organizer,
                opt => opt.MapFrom(dest => dest.Organizer))
            .ForMember(command => command.Place,
                opt => opt.MapFrom(dest => dest.Place))
            .ForMember(command => command.Date,
                opt => opt.MapFrom(dest => dest.Date));
    }
}