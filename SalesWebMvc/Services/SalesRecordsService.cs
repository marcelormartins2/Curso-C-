using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using SalesWebMvc.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SalesRecordsService
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordsService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<SalesRecord> FindAsync(int id) {
            return await _context.SalesRecord.FirstOrDefaultAsync(obj => obj.Id == id);
        }
        public async Task<List<SalesRecord>> FindAllAsync() {
            return await _context.SalesRecord.ToListAsync();
        }

        public async Task InsertAsync(SalesRecord obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }
     
        public async Task UpdatetAsync(SalesRecord obj)
        {
            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                //throw new NotFoundException("Id not found");
            }
            else
            {
                //try
                //{
                    _context.Update(obj);
                    await _context.SaveChangesAsync();
                //}
                //catch (DbUpdateConcurrencyException e)
                //{
                //    throw new DbConcurrencyException(e.Message);
                //}
            }
        }

        internal async Task RemoveAsync(int id)
        {
            var obj = await _context.SalesRecord.FindAsync(id);
            _context.SalesRecord.Remove(obj);
            await _context.SaveChangesAsync();
        }
    }
}
