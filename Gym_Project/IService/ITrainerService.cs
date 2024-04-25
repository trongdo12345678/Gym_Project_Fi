
using Gym_Project.Models;
namespace Gym_Project.IService;

public interface ITrainerService
{
	public bool AddTrainer(Trainer trai);
    public (int, int) GetPaginationInfo(int pageSize, int currentPage, string searchText = null);
	public bool DropTrainer(int id);
    public bool UpdateTrainer(Trainer trai);
    public Trainer GetTrai(int id);
    public List<Trainer> GetlistPbyPages(int page, int pageSize, string searchText = null);
}
