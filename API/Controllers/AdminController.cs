using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API;

public class AdminController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager;

    public AdminController(UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper)
    {
        _userManager = userManager;
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpGet("users-with-roles")]
    public async Task<ActionResult> GetUsersWithRoles()
    {
        var users = await _userManager.Users.OrderBy(u => u.UserName).Select(u => new
        {
            u.Id,
            Username = u.UserName,
            Roles = u.UserRoles.Select(r => r.Role.Name)
        }).ToListAsync();

        return Ok(users);
    }
    
    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("edit-roles/{username}")]
    public async Task<ActionResult> EditRoles(string username, [FromQuery]string roles)
    {
        if (string.IsNullOrEmpty(roles))
        {
            return BadRequest("You must select at least one role");
        }

        var selectedRoles = roles.Split(',').ToArray();
        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            return NotFound();
        }

        var userRoles = await _userManager.GetRolesAsync(user);
        var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));
        if (!result.Succeeded) { return BadRequest("Failed to add to roles"); }

        result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));
        if (!result.Succeeded) { return BadRequest("Failed to remove from roles"); }

        return Ok(await _userManager.GetRolesAsync(user));
    }

    [Authorize(Policy = "ModeratorPhotoRole")]
    [HttpGet("photos-to-moderator")]
    public ActionResult GetPhotosForModeration()
    {
        return Ok("Admins or moderators can see it");
    }

}
