using LibraryWebApi.DTOs.Member;
using LibraryWebApi.Models;

namespace LibraryWebApi.Mappers
{
    public static class MemberMapper
    {

        public static Member FromRegisterMemberDto(this RegisterMemberDto MemberDto, string RegisterLibrarianId)
        {
            return new Member
            {
                FullName = MemberDto.FullName,
                NationalId = MemberDto.NationalId,
                Address = MemberDto.Address,
                PhoneNumber = MemberDto.PhoneNumber,
                Email = MemberDto.Email,
                RegisterDate = MemberDto.RegisterDate,
                LastRenewedDate = MemberDto.RegisterDate,
                IsActive = true,
                RegisterLibrarianId = RegisterLibrarianId,
            };
        }

        public static GetMemberDto ToGetMemberDto(this Member member)
        {
            return new GetMemberDto
            {
                MemberId = member.MemberId,
                FullName = member.FullName,
                NationalId = member.NationalId,
                Address = member.Address,
                PhoneNumber = member.PhoneNumber,
                Email = member.Email,
                RegisterDate = member.RegisterDate,
                LastRenewedDate = member.LastRenewedDate,
                RegisterLibrarianName = member.RegisterLibrarian.FullName,
            };
        }

    }
}
