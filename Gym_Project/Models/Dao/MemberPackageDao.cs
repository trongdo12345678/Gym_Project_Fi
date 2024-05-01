using Gym_Project.IService;
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;

namespace Gym_Project.Models.Dao;

public class MemberPackageDao : IMemberPackageService
{
	private readonly GymProjectContext _context;
	public MemberPackageDao(GymProjectContext context)
	{
		_context = context;
	}
    public MemberPackage GetMemPack(int id)
    {
        try
        {
            var mempack = _context.MemberPackages
                .Include(p => p.Member)
                .Include(p => p.Pay)
				.Include(p => p.Class)
				.Include(p => p.Trainer)
				.Include(p => p.Package)
                .FirstOrDefault(p => p.MemPackId == id);
            if (mempack != null) return mempack;
            return new MemberPackage();
        }
        catch (Exception)
        {
            return new MemberPackage();
        }
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
	public List<ClassPack> GetClass()
	{
		try
		{
			var res = _context.ClassPacks.ToList();
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
    public List<Package> GetPack()
    {
        try
        {
            var res = _context.Packages.ToList();
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
    //lấy tổng số trang của cái bản projectType 
    public (int, int) GetPaginationInfo(int pageSize, int currentPage, string searchText = null)
    {
        try
        {
            IQueryable<MemberPackage> query = _context.MemberPackages;

            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(pt => pt.Status.Contains(searchText) ||
                                    pt.Trainer.TrainerName.ToString().Contains(searchText) ||
                                    pt.Package.PackageName.ToString().Contains(searchText) ||
                                    pt.Class.ClassName.ToString().Contains(searchText) ||
                                    pt.Member.Username.ToString().Contains(searchText) ||
                                    pt.Pay.PaymentMethod.ToString().Contains(searchText)
                                    
                                    );
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
    public List<MemberPackage> GetlistPbyPages(int page, int pageSize, string searchText = null)
    {
        IQueryable<MemberPackage> query = _context.MemberPackages;

        if (!string.IsNullOrEmpty(searchText))
        {
            query = query.Where(pt => pt.Status.Contains(searchText) || 
                                      pt.Trainer.TrainerName.ToString().Contains(searchText) ||
                                      pt.Package.PackageName.ToString().Contains(searchText) ||
                                      pt.Class.ClassName.ToString().Contains(searchText) ||
                                      pt.Member.Username.ToString().Contains(searchText) ||
                                      pt.Pay.PaymentMethod.ToString().Contains(searchText)

                                      );
        }

        var result = query.Skip((page - 1) * pageSize)
                          .Take(pageSize)
                          .Include(p => p.Member)
                          .Include(p => p.Pay)
                          .Include(p => p.Package)
						  .Include(p => p.Trainer)
                          .Include(p => p.Class)
                          .ToList();

        return result;
    }
    //gửi tt xuống admin
    public bool UserSendMemberPackage(MemberPackage mempack)
    {
        try
        {
            if (mempack == null)
            {

                return false;
            }
            else
            {
                _context.MemberPackages.Add(new MemberPackage()
                {
                    TrainerId = null,
                    MemberId = mempack.MemberId,
                    Status = "Waiting for confirmation",
                    PayId = mempack.PayId,
                    ClassId = null,
                    PackageId = mempack.PackageId,
                    StartDate = null,
                    EndDate = null,
                });

                return _context.SaveChanges() > 0;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in UserSendOrder: {ex.Message}");
            return false;
        }
    }
    public bool UpdateMemPack(MemberPackage package)
    {
        try
        {
            var p = _context.MemberPackages.FirstOrDefault(p => p.MemPackId == package.MemPackId);
            if (p != null)
            {
                p.TrainerId = package.TrainerId;
				p.MemberId = package.MemberId;
                p.Status = "There's still time limit";
                p.ClassId = package.ClassId;
				p.PayId = package.PayId;
				p.PackageId = package.PackageId;
                p.StartDate = package.StartDate;
                p.EndDate = package.EndDate;
                return _context.SaveChanges() > 0;
            }
            return true;
        }
        catch (Exception) { return false; }
    }
    public bool UpdateStatus(MemberPackage package)
    {
        try
        {
            var p = _context.MemberPackages.FirstOrDefault(p => p.MemPackId == package.MemPackId);
            if (p != null)
            {
                p.TrainerId = package.TrainerId;
                p.MemberId = package.MemberId;
                p.Status = package.Status;
                p.ClassId = package.ClassId;
                p.PayId = package.PayId;
                p.PackageId = package.PackageId;
                p.StartDate = package.StartDate;
                p.EndDate = package.EndDate;
                return _context.SaveChanges() > 0;
            }
            return true;
        }
        catch (Exception) { return false; }
    }
}
