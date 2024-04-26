using Gym_Project.IService;
using Microsoft.EntityFrameworkCore;

namespace Gym_Project.Models.Dao;

public class AttendanceDao : IAttendanceService
{
	private readonly GymProjectContext _context;
	public AttendanceDao(GymProjectContext context)
	{
		_context = context;
	}
	public bool AddAttendance(Attendance attendance)
	{
		try
		{
			_context.Attendances.Add(attendance);
			return _context.SaveChanges() > 0;
		}catch (Exception)
		{
			return false;
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
    //lấy tổng số trang của cái bản projectType 
    public (int, int) GetPaginationInfo(int pageSize, int currentPage)
    {
        try
        {
            int totalItems = _context.Attendances.Count();

            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            currentPage = Math.Max(1, Math.Min(currentPage, totalPages));

            return (totalPages, currentPage);
        }
        catch (Exception)
        {
            return (1, 1);
        }
    }
    //lấy projecttype theo từng trang 
    public List<Attendance> GetlistPbyPages(int page, int pageSize)
    {
        try
        {

            int skip = (page - 1) * pageSize;

            var Att = _context.Attendances
             .OrderByDescending(pt => pt.AttendanceId)
             .Skip(skip)
             .Take(pageSize)
             .Include(pt => pt.Trainer)
             .ToList();

            return Att;
        }
        catch (Exception)
        {
            return [];
        }
    }
    public bool IsAttendanceExistsForToday(DateOnly currentDate, int? trainerId)
    {
        return _context.Attendances.Any(a => a.TrainerId == trainerId && a.Date == currentDate);
    }
    public bool DropAll(int id)
    {
        try
        {
            var res = _context.Attendances.FirstOrDefault(p => p.AttendanceId == id);
            if (res != null)
            {
                _context.Attendances.Remove(res);
                return _context.SaveChanges() > 0;
            }
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool DropAtt(int id)
    {
        try
        {
            var res = _context.Attendances.FirstOrDefault(p => p.AttendanceId.Equals(id));
            if (res != null)
            {
                _context.Attendances.Remove(res);
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
