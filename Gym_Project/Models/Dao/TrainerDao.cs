using Gym_Project.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NuGet.ProjectModel;
using System.Security.Cryptography;
using System.Text;

namespace Gym_Project.Models.Dao;

public class TrainerDao : ITrainerService
{
    private readonly GymProjectContext _context;
    public TrainerDao(GymProjectContext context)
    {
        _context = context;
    }
    public bool AddTrainer(Trainer trai)
    {
        try
        {
            trai.Password = HashPassword(trai.Password);

            _context.Trainers.Add(trai);
            return _context.SaveChanges() > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }

    // Hash password function
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

    //lấy tổng số trang của cái bản projectType 
    public (int, int) GetPaginationInfo(int pageSize, int currentPage , string searchText = null)
    {
        try
        {
            IQueryable<Trainer> query = _context.Trainers;

            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(pt => pt.TrainerName.Contains(searchText) ||
                                          pt.Username.Contains(searchText) ||
                                          pt.Email.Contains(searchText) ||
                                          pt.Phone.Contains(searchText) ||
                                          pt.Specialization.Contains(searchText));
            }

            int totalItems = query.Count();

            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            currentPage = Math.Max(1, Math.Min(currentPage, totalPages));

            return (totalPages, currentPage);
        }
        catch (Exception)
        {
            return (1, 1);
        }
    }
    //lấy projecttype theo từng trang seach
    public List<Trainer> GetlistPbyPages(int page, int pageSize, string searchText = null)
    {
        IQueryable<Trainer> query = _context.Trainers;

        if (!string.IsNullOrEmpty(searchText))
        {
            query = query.Where(pt => pt.TrainerName.Contains(searchText) || 
                                      pt.Username.Contains(searchText) || 
                                      pt.Email.Contains(searchText) ||
                                      pt.Phone.Contains(searchText) ||
                                      pt.Specialization.Contains(searchText)


                                 );
        }

        var result = query.Skip((page - 1) * pageSize)
                          .Take(pageSize)
                          .Include(pt => pt.Role)
                          .ToList();

        return result;
    }
    
    public Trainer GetTrai(int id)
    {
        try
        {
            var trai = _context.Trainers
                .Include(p => p.Role)
                .FirstOrDefault(p => p.TrainerId == id);
            if (trai != null) return trai;
            return new Trainer();
        }
        catch (Exception)
        {
            return new Trainer();
        }
    }

	public List<Trainer> GetTrainer()
	{
		try
		{
			var res = _context.Trainers.ToList();
			if (res != null)
			{
				return res;
			}
			return [];
		}
		catch (Exception)
		{

			return [];
		}
	}
	public bool DropTrainer(int id)
    {
        try
        {
            var res = _context.Trainers.FirstOrDefault(p => p.TrainerId.Equals(id));
            if(res != null)
            {
                _context.Trainers.Remove(res);
                return _context.SaveChanges() > 0;
            }
            return true;
        }
		catch (Exception)
		{
            return false;
        }
    }
    public bool UpdateTrainer(Trainer trai)
    {
        try
        {
            var e = _context.Trainers.FirstOrDefault(e => e.TrainerId == trai.TrainerId);
            if(e != null)
            {
                e.TrainerName = trai.TrainerName;
                e.Username = trai.Username;
                e.Password = trai.Password;
                e.Email = trai.Email;
                e.Img = trai.Img;
                e.RoleId = trai.RoleId;
                e.Phone = trai.Phone;
                e.Specialization = trai.Specialization;
                return _context.SaveChanges() > 0;

            }
            return true;
        }catch (Exception) 
        { 
            return false;
        }
    }
   
}

