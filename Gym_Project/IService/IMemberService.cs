using Gym_Project.Models;

namespace Gym_Project.IService;

public interface IMemberService
{
	public Member? getmemberbyusername(string? username);
}
