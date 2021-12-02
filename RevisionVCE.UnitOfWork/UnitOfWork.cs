using RevisionVCE.Infra.Context;
using System;
using System.Threading.Tasks;

namespace RevisionVCE.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VceQuizzContext _context;

        public UnitOfWork(VceQuizzContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
