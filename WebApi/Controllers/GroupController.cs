using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Services;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GroupController : ControllerBase
{
    private readonly IGroupService _groupService;

    public GroupController(IGroupService groupService)
    {
        _groupService = groupService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateGroup(CreateGroupDto dto)
    {
        var result = await _groupService.CreateGroupAsync(dto, User);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetMyGroups()
    {
        var result = await _groupService.GetMyGroupsAsync(User);
        return Ok(result);
    }
}
