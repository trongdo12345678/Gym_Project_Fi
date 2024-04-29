using Gym_Project.Models;

namespace Gym_Project.IService;

public interface IMemberPackageService
{
    public bool UserSendMemberPackage(MemberPackage mempack);
    public MemberPackage GetMemPack(int id);
    public bool UpdateMemPack(MemberPackage package);
	public List<ClassPack> GetClass();
	public List<Trainer> GetTrai();
    public List<MemberPackage> GetlistPbyPages(int page, int pageSize, string searchText = null);
    public (int, int) GetPaginationInfo(int pageSize, int currentPage, string searchText = null);
    public bool UpdateStatus(MemberPackage package);
}
