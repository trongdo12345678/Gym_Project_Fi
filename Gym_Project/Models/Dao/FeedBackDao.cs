using Gym_Project.IService;
using Microsoft.EntityFrameworkCore;

namespace Gym_Project.Models.Dao;

public class FeedBackDao : IFeedBackService
{
    private readonly GymProjectContext _context;
    public FeedBackDao(GymProjectContext context)
    {
        _context = context;
    }
    public bool AddFeed(Feedback feedback)
    {
        try
        {
            _context.Feedbacks.Add(feedback);
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
	public List<Feedback> GetFeed()
	{
		try
		{
			var res = _context.Feedbacks.ToList();
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
			int totalItems = _context.Feedbacks.Count();

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
	public List<Feedback> GetlistPbyPages(int page, int pageSize)
	{
		try
		{
			int skip = (page - 1) * pageSize;

			var feedbacks = _context.Feedbacks
				.OrderByDescending(f => f.FeedbackId)
				.Skip(skip)
				.Take(pageSize)
				.Include(f => f.Trainer)
				.ToList();

			return feedbacks;
		}
		catch (Exception)
		{
			return []; // Trả về danh sách rỗng nếu có lỗi
		}
	}

	public bool DropFeed(int id)
    {
        try
        {
            var res = _context.Feedbacks.FirstOrDefault(p => p.FeedbackId.Equals(id));
            if (res != null)
            {
                _context.Feedbacks.Remove(res);
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
