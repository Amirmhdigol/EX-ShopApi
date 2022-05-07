using Dapper;
using Microsoft.EntityFrameworkCore;
using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Repository;
using Shop.Infrastructure._Utilities;
using Shop.Infrastructure.Persistent.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Infrastructure.Persistent.Ef.SellerAgg
{
    internal class SellerRepository : BaseRepository<Seller>, ISellerRepository
    {
        private readonly DapperContext _dapperContext;
        public SellerRepository(ShopContext context, DapperContext dapperContext) : base(context)
        {
            _dapperContext = dapperContext;
        }

        //public async Task<InventoryResult> GetInventoryById(long id)
        //{
        //    return await _context.Inventories.Where(r => r.Id == id)
        //        .Select(i => new InventoryResult()
        //        {
        //            Count = i.Count,
        //            Id = i.Id,
        //            Price = i.Price,
        //            ProductId = i.ProductId,
        //            SellerId = i.SellerId
        //        }).FirstOrDefaultAsync();
        //}
        public async Task<InventoryResult?> GetInventoryById(long id)
        {
            using var Connection = _dapperContext.CreateConnection();
            var sql = $"SELECT * FROM {_dapperContext.Inventories} WHERE Id=@InventoryId";
            return await Connection.QueryFirstOrDefaultAsync<InventoryResult>(sql, new { InventoryId = id });
        }
    }
}
