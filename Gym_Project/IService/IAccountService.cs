using Gym_Project.Models;

namespace Gym_Project.IService;

public interface IAccountService
{
    public bool AdminLogin(string username, string password);
    public bool LoginTrainer(string username, string password);
    public bool LoginUser(string username, string password);
    public bool Register(Member member);
    public bool SaveUserInfo(int userId, string name, string address);
    public int? GetMemberIdByUsername(string username);

}
