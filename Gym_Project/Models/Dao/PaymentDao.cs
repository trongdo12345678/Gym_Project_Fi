using Gym_Project.IService;
using Microsoft.EntityFrameworkCore;

namespace Gym_Project.Models.Dao;

public class PaymentDao : IPaymentService
{
    private readonly GymProjectContext _context;
    public PaymentDao(GymProjectContext context)
    {
        _context = context;
    }
    public bool AddPay(Payment pay)
    {
        try
        {
            _context.Payments.Add(pay);
            return _context.SaveChanges() > 0;

        }
        catch (Exception)
        {
            return false;
        }
    }
    public List<Package> GetPack()
    {
        try
        {
            var res = _context.Packages.ToList();
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
    public List<Payment> GetPay()
    {
        try
        {
            var res = _context.Payments.ToList();
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
    public List<Member> GetMem()
    {
        try
        {
            var res = _context.Members.ToList();
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
    public (int, int) GetPaginationInfo(int pageSize, int currentPage, string searchText = null)
    {
        try
        {
            IQueryable<Payment> query = _context.Payments;

            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(pt => pt.PaymentMethod.Contains(searchText));
            }

            int totalItems = query.Count();

            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            currentPage = Math.Max(1, Math.Min(currentPage, totalPages));

            return (totalPages, currentPage);
        }
        catch (Exception)
        {
            return (1, 1);
        }
    }
    //lấy projecttype theo từng trang seach
    public List<Payment> GetlistPbyPages(int page, int pageSize, string searchText = null)
    {
        IQueryable<Payment> query = _context.Payments;

        if (!string.IsNullOrEmpty(searchText))
        {
            query = query.Where(pt => pt.PaymentMethod.Contains(searchText));
        }

        var result = query.Skip((page - 1) * pageSize)
                          .Take(pageSize)
                          .Include(pt => pt.Package)
                          .Include(pt => pt.Member)
                          .ToList();

        return result;
    }
}
