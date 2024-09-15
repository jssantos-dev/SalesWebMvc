using SalesWebMvc.Data;
using SalesWebMvc.Models;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            return _context.Seller.ToList();
        }

        public void Insert(Seller obj) 
        {
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id) 
        {
#pragma warning disable CS8603 // Possible null reference return.
            return _context.Seller.FirstOrDefault(obj => obj.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public void Remove(int id) 
        {
            var obj = _context.Seller.Find(id);
#pragma warning disable CS8604 // Possible null reference argument.
            _context.Seller.Remove(obj);
#pragma warning restore CS8604 // Possible null reference argument.
            _context.SaveChanges();
        }
    }
}
