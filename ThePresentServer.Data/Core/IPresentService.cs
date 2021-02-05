using System.Collections.Generic;
using System.Threading.Tasks;
using ThePresentServer.Data.Entities;
using ThePresentServer.Data.Models;

namespace ThePresentServer.Data.Core
{
    public interface IPresentService
    {
        public Task AddService(Present present);

        public Task RemovePresent(string id);

        public Task<IEnumerable<Present>> GetByIdsAsync(string[] ids);

        public Task<PresentEntity[]> GetByUserIds(string[] ids);
    }
}