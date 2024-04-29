using Gym_Project.IService;
using Microsoft.EntityFrameworkCore;

namespace Gym_Project.Models.Dao;

public class MemberDao : IMemberService
{
	private readonly GymProjectContext _context;
	public MemberDao(GymProjectContext context)
	{
		_context = context;
	}
	public Member? getmemberbyusername(string? username)
	{
		try
		{
			var memberlogin = _context.Members.FirstOrDefault(m => m.Username == username);
			if (memberlogin != null) return memberlogin;

			return new Member();
		}
		catch (Exception)
		{

			return new Member();

		}
	}
}
