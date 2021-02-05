using System.Linq;
using ThePresentServer.Data.Core.Common;
using ThePresentServer.Data.Entities;

namespace ThePresentServer.Data.Repository
{
    public class PresentRepository : DbContextRepositoryBase<ThePresentDbContext>, IPresentRepository
    {
        public PresentRepository(ThePresentDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<UserEntity> Users => DbContext.Set<UserEntity>();
        public IQueryable<PresentEntity> Presents => DbContext.Set<PresentEntity>();
    }
}