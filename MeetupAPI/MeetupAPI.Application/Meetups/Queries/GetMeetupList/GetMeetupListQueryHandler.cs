using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using MeetupAPI.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeetupAPI.Application.Meetups.Queries.GetMeetupList;

public class GetMeetupListQueryHandler : IRequestHandler<GetMeetupListQuery, MeetupListViewModel>
{
    private readonly IMeetupsDbContext _context;
    private readonly IMapper _mapper;

    public GetMeetupListQueryHandler(IMeetupsDbContext context, IMapper mapper) =>
        (_context, _mapper) = (context, mapper);
    
    public async Task<MeetupListViewModel> Handle(GetMeetupListQuery request, CancellationToken cancellationToken)
    {
        var meetups = await _context.Meetups.Where(meetup => meetup.UserId == request.UserId)
            .ProjectTo<MeetupLookupDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new MeetupListViewModel {Meetups = meetups};
    }
}