using System;
using System.Linq;
using ThePresentServer.Data.Entities;

namespace ThePresentServer.Data.Core.Common
{
    public interface IPresentRepository : IRepository, IDisposable
    {
        public IQueryable<UserEntity> Users { get; }
        public IQueryable<PresentEntity> Presents { get; }

    }
}