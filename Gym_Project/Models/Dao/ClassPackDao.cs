using Gym_Project.IService;
using Microsoft.EntityFrameworkCore;

namespace Gym_Project.Models.Dao;

public class ClassPackDao : IClassPackService
{
    private readonly GymProjectContext _context;
    public ClassPackDao(GymProjectContext context)
    {
        _context = context;
    }
    public bool AddClass(ClassPack clas)
    {
        try
        {
            _context.ClassPacks.Add(clas);
            return _context.SaveChanges() > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }
    //lấy tổng số trang của cái bản projectType 
    public (int, int) GetPaginationInfo(int pageSize, int currentPage, string searchText = null)
    {
        try
        {
            IQueryable<ClassPack> query = _context.ClassPacks;

            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(pt => pt.ClassName.Contains(searchText));
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
    public List<ClassPack> GetlistPbyPages(int page, int pageSize, string searchText = null)
    {
        IQueryable<ClassPack> query = _context.ClassPacks;

        if (!string.IsNullOrEmpty(searchText))
        {
            query = query.Where(pt => pt.ClassName.Contains(searchText));
        }

        var result = query.Skip((page - 1) * pageSize)
                          .Take(pageSize)
                          .ToList();

        return result;
    }

    public ClassPack GetClass(int id)
    {
        try
        {
            var clas = _context.ClassPacks
                .FirstOrDefault(p => p.ClassId == id);
            if (clas != null) return clas;
            return new ClassPack();
        }
        catch (Exception)
        {
            return new ClassPack();
        }
    }
    public bool DropClass(int id)
    {
        try
        {
            var res = _context.ClassPacks.FirstOrDefault(p => p.ClassId.Equals(id));
            if (res != null)
            {
                _context.ClassPacks.Remove(res);
                return _context.SaveChanges() > 0;
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public bool UpdateClass(ClassPack clas)
    {
        try
        {
            var e = _context.ClassPacks.FirstOrDefault(e => e.ClassId == clas.ClassId);
            if (e != null)
            {
                e.ClassName = clas.ClassName;
                e.DateClass = clas.DateClass;
                e.AssignmentTime = clas.AssignmentTime;
                return _context.SaveChanges() > 0;

            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
