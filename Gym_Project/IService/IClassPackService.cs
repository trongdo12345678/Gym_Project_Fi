using Gym_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym_Project.IService;

public interface IClassPackService
{
    public bool UpdateClass(ClassPack clas);
    public bool AddClass(ClassPack clas);
    public (int, int) GetPaginationInfo(int pageSize, int currentPage, string searchText = null);
    public List<ClassPack> GetlistPbyPages(int page, int pageSize, string searchText = null);
    public ClassPack GetClass(int id);
    public bool DropClass(int id);
    public List<Trainer> GetTrai();
	public List<ClassPack> GetClasss();
}
