using Core.DTOs.Participants;
using Core.Interfaces.Participants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParticipantsController : ControllerBase
{
    private readonly IParticipantService _service;

    public ParticipantsController(IParticipantService service)
    {
        _service = service;
    }

    private string GetCurrentUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
    }

    [HttpPost("JoinClub")]
    [Authorize]
    public async Task<IActionResult> JoinClub(RegisterParticipantDto dto)
    {
        dto.UserId = GetCurrentUserId();
        var result = await _service.JoinClubAsync(dto);
        if (!result) return BadRequest(new { message = "Already a member or registration failed" });
        return Ok(new { message = "Successfully joined the club" });
    }

    [HttpPost("RegisterActivity")]
    [Authorize]
    public async Task<IActionResult> RegisterActivity(RegisterParticipantDto dto)
    {
        dto.UserId = GetCurrentUserId();
        var result = await _service.RegisterForActivityAsync(dto);
        if (!result) return BadRequest(new { message = "Already registered or registration failed" });
        return Ok(new { message = "Successfully registered for activity" });
    }

    [HttpPost("RegisterCompetition")]
    [Authorize]
    public async Task<IActionResult> RegisterCompetition(RegisterParticipantDto dto)
    {
        dto.UserId = GetCurrentUserId();
        var validationMessage = await _service.ValidateCompetitionRegistrationAsync(dto);
        if (validationMessage != null) return BadRequest(new { message = validationMessage });

        var result = await _service.RegisterForCompetitionAsync(dto);
        if (!result) return BadRequest(new { message = "Competition registration could not be completed." });
        return Ok(new { message = "Successfully registered for competition" });
    }

    [HttpPost("RegisterEvent")]
    [Authorize]
    public async Task<IActionResult> RegisterEvent(RegisterParticipantDto dto)
    {
        dto.UserId = GetCurrentUserId();
        var result = await _service.RegisterForEventAsync(dto);
        if (!result) return BadRequest(new { message = "Already registered or registration failed" });
        return Ok(new { message = "Successfully registered for event" });
    }

    [HttpGet("ActivityRegistrations")]
    public async Task<IActionResult> GetActivityRegistrations()
    {
        var result = await _service.GetActivityRegistrationsAsync();
        return Ok(result);
    }

    [HttpGet("ClubRegistrations")]
    public async Task<IActionResult> GetClubRegistrations()
    {
        var result = await _service.GetClubRegistrationsAsync();
        return Ok(result);
    }

    [HttpGet("CompetitionRegistrations")]
    public async Task<IActionResult> GetCompetitionRegistrations()
    {
        var result = await _service.GetCompetitionRegistrationsAsync();
        return Ok(result);
    }
}
