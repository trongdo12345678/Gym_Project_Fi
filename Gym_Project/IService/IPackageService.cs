using Gym_Project.Models;

namespace Gym_Project.IService;

public interface IPackageService
{
    public bool AddPack(Package package);
	public List<Trainer> GetTrai();
    public Package GetPack(int id);
    public (int, int) GetPaginationInfo(int pageSize, int currentPage, string searchText = null);
    public List<Package> GetlistPbyPages(int page, int pageSize, string searchText = null);
    public bool DropPack(int id);
    public bool UpdatePack(Package package);
}
