using LibraryWebApi.DTOs.Librarian;
using LibraryWebApi.Extensions;
using LibraryWebApi.Interfaces;
using LibraryWebApi.Mappers;
using LibraryWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LibraryWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrarianController(UserManager<Librarian> userManager, ITokenService tokenService, SignInManager<Librarian> signInManager) : ControllerBase
    {

        private readonly UserManager<Librarian> _userManager = userManager;
        private readonly ITokenService _tokenService = tokenService;
        private readonly SignInManager<Librarian> _signInManager = signInManager;



        


        [HttpPost("Hire")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Hire([FromBody] HireDto librarianDto)
        {

            try
            {

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);


                // id for register manager id
                var loggedInUser = await _userManager.FindByNameAsync(User.GetUsername());
                var loggedInUserId = loggedInUser!.Id;



                var newLibrarian = new Librarian
                {
                    UserName = librarianDto.Username,
                    Email = librarianDto.Email,
                    PhoneNumber = librarianDto.PhoneNumber,
                    FullName = librarianDto.FullName!,
                    NationalId = librarianDto.NationalId!,
                    Address = librarianDto.Address!,
                    StartWork = librarianDto.StartWork,
                    RegisterManagerId = loggedInUserId,
                };

                var createLibrarian = await _userManager.CreateAsync(newLibrarian, librarianDto.Password!);

                

                if (createLibrarian.Succeeded)
                {

                    var roleName = librarianDto.IsManager ? "Manager" : "Receptionist";

                    var roleResult = await _userManager.AddToRoleAsync(newLibrarian, roleName);

                    if (roleResult.Succeeded)
                    {
                        // create on okay or something instead of ok
                        return CreatedAtAction(nameof(GetLibrarianById), new { id = newLibrarian.Id}, librarianDto);

                    } else
                    {
                        return StatusCode(500, roleResult.Errors.ToString());
                    }



                } else
                {
                    return StatusCode(500, createLibrarian.Errors.ToString());
                }



            }catch (Exception ex)
            {
                return StatusCode(500, ex.GetBaseException().Message);
            }

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user  = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == loginDto.Username);

            if (user == null)
            {
                return Unauthorized("Invalid Username");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password!, false);

            if (!result.Succeeded)
            {
                return Unauthorized("Invalid Username and/or wrong password");
            }

            if (!user.IsActive)
            {
                return Unauthorized("This user no longer works in this Library");
            }


            return Ok(
                new TokenDto
                {
                    Token =  await _tokenService.CreateToken(user),
                }
                );
        }

        [HttpGet("GetAllLibrarians")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAllLibrarians()
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var librarians = await _userManager.Users.Where( s => s.IsActive == true).ToListAsync();

            var librariansDto =  librarians.Select(s => s.toGetLibrarianDto()).ToList();

            return Ok(librariansDto);
            
        }

        [HttpGet("GetLibrarianById{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetLibrarianById([FromRoute] string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var librarian = await _userManager.Users.Where(s => s.IsActive == true).FirstOrDefaultAsync(i => i.Id == id);
            if (librarian == null)
            {
                return NotFound("no librarian found with this id");
            }
            var librarianDto = librarian.toGetLibrarianDto();

            return Ok(librarianDto);
        }


        [HttpDelete("FireLibrarianById{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> FireLibrarian([FromRoute] string id, [FromBody] FireLibrarianDto fireLibrarian)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var librarian = await _userManager.Users.Where(s => s.IsActive == true).FirstOrDefaultAsync(i => i.Id == id);
            if (librarian == null)
            {
                return NotFound("no librarian found with this id");
            }

            librarian.EndWork = fireLibrarian.EndWork;
            librarian.IsActive = false;

            await _userManager.UpdateAsync(librarian);

            return Ok("This Librarian no longer works in this library");

        }
        
    }
}
