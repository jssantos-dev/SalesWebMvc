﻿using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            return await _context.Seller.ToListAsync();
        }

        public async Task InsertAsync(Seller obj) 
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id) 
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task RemoveAsync(int id) 
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
#pragma warning disable CS8604 // Possible null reference argument.
                _context.Seller.Remove(obj);
#pragma warning restore CS8604 // Possible null reference argument.
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) 
            {
                throw new IntegrityException("Can't delete seller bacause he/she has sales");
            }
            
        }

        public async Task UpdateAsync(Seller obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
