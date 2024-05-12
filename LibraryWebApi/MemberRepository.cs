using LibraryWebApi.Data;
using LibraryWebApi.Helpers;
using LibraryWebApi.Interfaces;
using LibraryWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi
{
    public class MemberRepository(ApplicationDbContext context) : IMemberRepository
    {

        private readonly ApplicationDbContext _context = context;

        public async Task<List<Member>?> GetAllMembersAsync(PaginaionQuery query)
        {
            var members = await _context.Members.Include(s => s.RegisterLibrarian).ToListAsync();


            if (members == null)
            {
                return null;
            }


            // pagination

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            var paginatedMembers = members.Skip(skipNumber).Take(query.PageSize).ToList();

            return paginatedMembers;
        }

        public async Task<Member?> GetMemberByIdAsync(int id)
        {
            var member = await _context.Members.Include(s => s.RegisterLibrarian).FirstOrDefaultAsync(i => i.MemberId == id);

            return member;
        }

        public async Task<Member> RegisterMemberAsync(Member member)
        {

            await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();
            return member;

        }


    }
}
