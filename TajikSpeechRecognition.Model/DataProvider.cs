using System;
using System.Linq;

namespace TajikSpeechRecognition.Model
{
    public class DataProvider : IDisposable
    {
        public DataProvider()
        {
            Context = new EntityContext();
        }

        EntityContext Context { get; set; }

        public IQueryable<TEntity> GetEntities<TEntity>() where TEntity : EntityBase
        {
            return Context.GetEntities<TEntity>();
        }

        public TEntity Add<TEntity>() where TEntity : EntityBase, new()
        {
            var newEntity = new TEntity();
            newEntity.AddToContext(Context);
            return newEntity;
        }

        public void Add(EntityBase entity)
        {
            entity.AddToContext(Context);
        }

        public void Delete(EntityBase entity)
        {
            Context.Delete(entity);
        }

        public void SaveChanges()
        {
            Context.SaveChangesAsync();
        }

        public void DiscardChanges(EntityBase entity) => Context.DiscardChanges(entity);

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}