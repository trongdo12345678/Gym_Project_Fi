using Gym_Project.IService;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Gym_Project.Models.Dao;

public class AccountDao : IAccountService
{
    private readonly GymProjectContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public AccountDao(GymProjectContext context, IHttpContextAccessor httpContextAccessor )
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }
    // login cho admin
    public bool AdminLogin(string username, string password)
    {
        var admin = _context.Members.Include(m => m.Role).FirstOrDefault(m => m.Username == username && m.Password == password);
        if (admin != null && admin.RoleId == 1)
        {
            _httpContextAccessor.HttpContext.Session.SetString("LoggedInUser", username);
            return true;
        }
        return false;
    }


    //login cho trainer
    public bool LoginTrainer(string username, string password)
    {
        var trainer = _context.Trainers.Include(m => m.Role).FirstOrDefault(m => m.Username == username);
        if (trainer != null && VerifyPassword(password, trainer.Password) && trainer.RoleId == 2)
        {
            _httpContextAccessor.HttpContext.Session.SetString("LoggedInUser", username);
            _httpContextAccessor.HttpContext.Session.SetString("UserRole", "Trainer");
            return true;
        }
        return false;
    }
    //login user
    public bool LoginUser(string username, string password)
    {
        var member = _context.Members.Include(m => m.Role).FirstOrDefault(m => m.Username == username);
        if (member != null && VerifyPassword(password, member.Password) && member.RoleId == 3)
        {
            _httpContextAccessor.HttpContext.Session.SetString("LoggedInUser", username);
            return true;
        }
        return false;
    }

    // hàm xác nhận mật khẩu
    private bool VerifyPassword(string password, string hashedPassword)
    {
        string hashedInputPassword = HashPassword(password);
        return string.Equals(hashedInputPassword, hashedPassword, StringComparison.OrdinalIgnoreCase);
    }
    //bảo mật mật khẩu
    private string HashPassword(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
    // đăng kí cho user
    public bool Register(Member member)
    {
        var existingMember = _context.Members.FirstOrDefault(m => m.Username == member.Username);
        if (existingMember != null)
        {

            return false;
        }
        member.Password = HashPassword(member.Password);


        var userRole = _context.Roles.FirstOrDefault(r => r.RoleId == 3);
        if (userRole != null)
        {
            member.RoleId = userRole.RoleId;
        }
        else
        {

            var newUserRole = new Role { RoleId = 3, RoleName = "User" };
            _context.Roles.Add(newUserRole);
            _context.SaveChanges();
            member.RoleId = newUserRole.RoleId;
        }

        _context.Members.Add(member);
        _context.SaveChanges();

        return true;
    }
    // LưuThông tin người dùng
    public bool SaveUserInfo(int userId, string name, string address)
    {
        try
        {
            var existingUser = _context.Members.FirstOrDefault(m => m.MemberId == userId);
            if (existingUser != null)
            {

                existingUser.MemName = name;
                existingUser.Address = address;

                

                return _context.SaveChanges() > 0;
			}
            else
            {
                return false;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }

    public int? GetMemberIdByUsername(string username)
    {
        var memberId = _context.Members
                               .Where(m => m.Username == username)
                               .Select(m => m.MemberId)
                               .FirstOrDefault();

        return memberId;
    }
}
