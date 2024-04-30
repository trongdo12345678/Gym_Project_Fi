using Gym_Project.Models;

namespace Gym_Project.IService;

public interface IPaymentService
{
    public bool AddPay(Payment pay);
    public List<Package> GetPack();
    public List<Payment> GetPay();
    public List<Member> GetMem();
    public (int, int) GetPaginationInfo(int pageSize, int currentPage, string searchText = null);
    public List<Payment> GetlistPbyPages(int page, int pageSize, string searchText = null);

}
