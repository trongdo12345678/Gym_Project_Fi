using Gym_Project.IService;
using Microsoft.EntityFrameworkCore;
using NuGet.ProjectModel;

namespace Gym_Project.Models.Dao;

public class PackageDao : IPackageService
{
    private readonly GymProjectContext _context;
    public PackageDao(GymProjectContext context)
    {
        _context = context;
    }
	public List<Trainer> GetTrai()
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
	public bool AddPack(Package package)
    {
        try
        {
            _context.Packages.Add(package);
            return _context.SaveChanges() > 0;
        }catch (Exception)
        {
            return false;
        }
    }
    public Package GetPack(int id)
    {
        try
        {
            var pack = _context.Packages
                .Include(p => p.Trainer)
                .FirstOrDefault(p => p.PackageId == id);
            if (pack != null) return pack;
            return new Package();
        }
        catch (Exception)
        {
            return new Package();
        }
    }

    //lấy tổng số trang của cái bản projectType 
    public (int, int) GetPaginationInfo(int pageSize, int currentPage, string searchText = null)
    {
        try
        {
            IQueryable<Package> query = _context.Packages;

            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(pt => pt.PackageName.Contains(searchText) ||
                                          pt.Trainer.TrainerName.ToString().Contains(searchText) ||
                                          pt.Discount.ToString().Contains(searchText) ||
                                          pt.Cost.ToString().Contains(searchText));
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
    public List<Package> GetlistPbyPages(int page, int pageSize, string searchText = null)
    {
        IQueryable<Package> query = _context.Packages;

        if (!string.IsNullOrEmpty(searchText))
        {
            query = query.Where(pt => pt.PackageName.Contains(searchText) ||
                                      pt.Trainer.TrainerName.ToString().Contains(searchText) ||
                                      pt.Discount.ToString().Contains(searchText) ||
                                      pt.Cost.ToString().Contains(searchText));
        }

        var result = query.Skip((page - 1) * pageSize)
                          .Take(pageSize)
                          .Include(pt => pt.Trainer)
                          .ToList();

        return result;
    }
    public bool DropPack(int id)
    {
        try
        {
            var p = _context.Packages.FirstOrDefault(e => e.PackageId.Equals(id));
            if(p != null)
            {
                _context.Packages.Remove(p);
                return _context.SaveChanges() > 0;
            }
            return true;
        }catch (Exception) { return false; }
    }
    public bool UpdatePack(Package package)
    {
        try
        {
            var p = _context.Packages.FirstOrDefault(p => p.PackageId == package.PackageId);
            if(p != null)
            {
                p.PackageName = package.PackageName;
                p.Img = package.Img;
                p.Cost = package.Cost;
                p.Discount = package.Discount;
                p.TrainerId = package.TrainerId;
                p.Description = package.Description;
                return _context.SaveChanges() > 0;
            }
            return true;
        }catch(Exception) { return false; }
    }
}
