using LibraryWebApi.DTOs.Member;
using LibraryWebApi.Extensions;
using LibraryWebApi.Helpers;
using LibraryWebApi.Interfaces;
using LibraryWebApi.Mappers;
using LibraryWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController(IMemberRepository memberRepo, UserManager<Librarian> userManager) : ControllerBase
    {

        private readonly IMemberRepository _memberRepo = memberRepo;
        private readonly UserManager<Librarian> _userManager = userManager;

        [HttpPost("RegisterMember")]
        [Authorize]
        public async Task<IActionResult> RegisterMember([FromBody] RegisterMemberDto MemberDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // id for register manager id
            var loggedInUser = await _userManager.FindByNameAsync(User.GetUsername());
            var loggedInUserId = loggedInUser!.Id;

            var member = await _memberRepo.RegisterMemberAsync(MemberDto.FromRegisterMemberDto(loggedInUserId));

            return CreatedAtAction(nameof(GetMemberById), new { id = member.MemberId }, member.ToGetMemberDto());
        }

        [HttpGet("GetAllMembers")]
        [Authorize]
        public async Task<IActionResult> GetAllMembers([FromQuery] PaginaionQuery query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var members =await _memberRepo.GetAllMembersAsync(query);

            if (members == null)
            {
                return NotFound("No Members Available");
            }

            return Ok(members.Select(s => s.ToGetMemberDto()));
        }

        [HttpGet("GetMemberById{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetMemberById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var member = await _memberRepo.GetMemberByIdAsync(id);

            if (member == null)
            {
                return NotFound("No member found with this id");
            }
            
            return Ok(member.ToGetMemberDto());
        }

        //fix the showing registeredlibrarianFullName

    }
}
