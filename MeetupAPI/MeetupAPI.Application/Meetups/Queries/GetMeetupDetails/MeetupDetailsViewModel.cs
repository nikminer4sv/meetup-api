using AutoMapper;
using MeetupAPI.Application.Common.Mappings;
using MeetupAPI.Domain;

namespace MeetupAPI.Application.Meetups.Queries.GetMeetupDetails;

public class MeetupDetailsViewModel : IMapWith<Meetup>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Organizer { get; set; }
    public string Place { get; set; }
    public DateTime Date { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Meetup, MeetupDetailsViewModel>()
            .ForMember(meetupVm => meetupVm.Id,
                opt => opt.MapFrom(dest => dest.Id))
            .ForMember(meetupVm => meetupVm.Title,
                opt => opt.MapFrom(dest => dest.Title))
            .ForMember(meetupVm => meetupVm.Description,
                opt => opt.MapFrom(dest => dest.Description))
            .ForMember(meetupVm => meetupVm.Organizer,
                opt => opt.MapFrom(dest => dest.Organizer))
            .ForMember(meetupVm => meetupVm.Place,
                opt => opt.MapFrom(dest => dest.Place))
            .ForMember(meetupVm => meetupVm.Date,
                opt => opt.MapFrom(dest => dest.Date));


    }
}