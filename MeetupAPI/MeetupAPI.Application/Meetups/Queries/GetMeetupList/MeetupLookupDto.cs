using AutoMapper;
using MeetupAPI.Application.Common.Mappings;
using MeetupAPI.Domain;

namespace MeetupAPI.Application.Meetups.Queries.GetMeetupList;

public class MeetupLookupDto : IMapWith<Meetup>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Meetup, MeetupLookupDto>()
            .ForMember(meetupDto => meetupDto.Id,
                opt => opt.MapFrom(dest => dest.Id))
            .ForMember(meetupDto => meetupDto.Title,
                opt => opt.MapFrom(dest => dest.Title));
    }
}