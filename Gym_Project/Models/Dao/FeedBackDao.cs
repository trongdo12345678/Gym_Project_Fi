using Gym_Project.IService;

namespace Gym_Project.Models.Dao;

public class FeedBackDao : IFeedBackService
{
    private readonly GymProjectContext _context;
    public FeedBackDao(GymProjectContext context)
    {
        _context = context;
    }

}
