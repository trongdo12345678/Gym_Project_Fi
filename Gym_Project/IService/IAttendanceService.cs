using Gym_Project.Models;

namespace Gym_Project.IService;

public interface IAttendanceService
{
	public bool AddAttendance(Attendance attendance);
	public List<Trainer> GetTrai();
    public List<Attendance> GetlistPbyPages(int page, int pageSize);
    public (int, int) GetPaginationInfo(int pageSize, int currentPage);
    public bool IsAttendanceExistsForToday(DateOnly currentDate, int? trainerId);
    public bool DropAtt(int id);
    public bool DropAll(int id);
}
