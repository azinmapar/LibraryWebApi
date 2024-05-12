using LibraryWebApi.Helpers;
using LibraryWebApi.Models;

namespace LibraryWebApi.Interfaces
{
    public interface IMemberRepository
    {

        Task<Member> RegisterMemberAsync(Member member);
        
        Task<List<Member>?> GetAllMembersAsync(PaginaionQuery query);

        Task<Member?> GetMemberByIdAsync(int id);

    }
}
