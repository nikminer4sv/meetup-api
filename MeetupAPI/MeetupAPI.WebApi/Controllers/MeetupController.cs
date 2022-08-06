using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper;
using IdentityModel.Client;
using MeetupAPI.Application.Interfaces;
using MeetupAPI.Application.Meetups.Commands.CreateMeetup;
using MeetupAPI.Application.Meetups.Commands.DeleteMeetup;
using MeetupAPI.Application.Meetups.Commands.UpdateMeetup;
using MeetupAPI.Application.Meetups.Queries.GetMeetupDetails;
using MeetupAPI.Application.Meetups.Queries.GetMeetupList;
using MeetupAPI.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeetupAPI.WebApi.Controllers;

[Produces("application/json")]
[Route("api/[controller]")]
public class MeetupController : BaseController
{
    private readonly IMapper _mapper;

    public MeetupController(IMapper mapper) => _mapper = mapper;

    
    /// <summary>
    /// Get all meetups
    /// </summary>
    /// <returns>Returns MeetupListViewModel</returns>
    /// <response code="200">Success</response>
    /// <response code="401">User is unauthorized</response>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<MeetupListViewModel>> GetAll()
    {
        GetMeetupListQuery query = new ()
        {
            UserId = UserId
        };
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    /// <summary>
    /// Get meetup by id
    /// </summary>
    /// <param name="id">Meetup id (guid)</param>
    /// <returns>Returns MeetupDetailsViewModel</returns>
    /// <response code="200">Success</response>
    /// <response code="401">User is unauthorized</response>
    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<MeetupDetailsViewModel>> GetMeetupDetails(Guid id)
    {
        GetMeetupDetailsQuery query = new()
        {
            Id = id,
            UserId = UserId
        };
        var vm = await Mediator.Send(query);
        return Ok(vm);
    }

    /// <summary>
    /// Create meetup
    /// </summary>
    /// <param name="createMeetupDto">CreateMeetupDto object</param>
    /// <returns>Returns id (guid)</returns>
    /// <response code="201">Success</response>
    /// <response code="401">User is unauthorized</response>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Guid>> CreateMeetup([FromBody]CreateMeetupDto createMeetupDto)
    {
        CreateMeetupCommand command = _mapper.Map<CreateMeetupCommand>(createMeetupDto);
        command.UserId = UserId;
        var id = await Mediator.Send(command);
        return Created("", id);
    }

    /// <summary>
    /// Delete meetup by id
    /// </summary>
    /// <param name="id">Id of the meetup (guid)</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="401">User is unauthorized</response>
    [HttpDelete("{id}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> DeleteMeetup(Guid id)
    {
        DeleteMeetupCommand command = new()
        {
            Id = id,
            UserId = UserId
        };
        await Mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Update meetup by id
    /// </summary>
    /// <param name="id">Id of the meetup (guid)</param>
    /// <returns>Returns NoContent</returns>
    /// <response code="204">Success</response>
    /// <response code="401">User is unauthorized</response>
    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateMeetup([FromBody] UpdateMeetupDto updateMeetupDto)
    {
        var command = _mapper.Map<UpdateMeetupCommand>(updateMeetupDto);
        command.UserId = UserId;
        await Mediator.Send(command);
        return NoContent();
    }

}