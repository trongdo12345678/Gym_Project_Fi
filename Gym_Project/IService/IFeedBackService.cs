using Gym_Project.Models;

namespace Gym_Project.IService;

public interface IFeedBackService
{
    public bool AddFeed(Feedback feedback);
	public (int, int) GetPaginationInfo(int pageSize, int currentPage);
	public List<Feedback> GetlistPbyPages(int page, int pageSize);
	public List<Trainer> GetTrai();
	public List<Feedback> GetFeed();
    public bool DropFeed(int id);
}
