using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DataAcces
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFDbContext _context;

        public UnitOfWork(EFDbContext context)
        {
            _context  = context;
        }

        public async Task SaveAsync()
        {
            await using var transaction = await this._context.Database.BeginTransactionAsync();

            await this._context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
    }
}
