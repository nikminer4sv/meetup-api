using AutoMapper;
using MediatR;
using MeetupAPI.Application.Common.Exceptions;
using MeetupAPI.Application.Interfaces;
using MeetupAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace MeetupAPI.Application.Meetups.Queries.GetMeetupDetails;

public class GetMeetupDetailsQueryHandler : IRequestHandler<GetMeetupDetailsQuery, MeetupDetailsViewModel>
{
    private readonly IMeetupsDbContext _context;
    private readonly IMapper _mapper;

    public GetMeetupDetailsQueryHandler(IMeetupsDbContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);
    
    public async Task<MeetupDetailsViewModel> Handle(GetMeetupDetailsQuery request, CancellationToken cancellationToken)
    {
        Meetup? meetup = await _context.Meetups.FirstOrDefaultAsync(meetup => meetup.Id == request.Id);

        if (meetup == null || meetup.UserId != request.UserId)
        {
            throw new NotFoundException(nameof(Meetup), request.Id);
        }

        return _mapper.Map<MeetupDetailsViewModel>(meetup);
    }
}