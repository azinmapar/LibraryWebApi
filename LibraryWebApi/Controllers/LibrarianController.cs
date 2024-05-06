using LibraryWebApi.DTOs.Librarian;
using LibraryWebApi.Extensions;
using LibraryWebApi.Interfaces;
using LibraryWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Security.Claims;

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
                        return Ok(
                            new NewLibrarianDto
                            {
                                Username = newLibrarian.UserName,
                                Email = newLibrarian.Email,
                                PhoneNumber = newLibrarian.PhoneNumber,
                                FullName = newLibrarian.FullName,
                                NationalId = newLibrarian.NationalId,
                                Address = newLibrarian.Address,
                                StartWork = newLibrarian.StartWork,
                                IsManager = librarianDto.IsManager,
                                
                            }
                            );

                    } else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }



                } else
                {
                    return StatusCode(500, createLibrarian.Errors);
                }



            }catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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


            return Ok(
                new TokenDto
                {
                    Token =  await _tokenService.CreateToken(user),
                }
                );
        }

        
    }
}
