using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.CompilerServices;

namespace TajikSpeechRecognition.Model
{

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class EntityContext : DbContext, INotifyPropertyChanged
    {
        public EntityContext() : base(GetConnectionString("dataset_app_db"))
        {
            if (!Database.Exists())
            {
                Database.Create();

            }
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
            ObjectContext context = ((IObjectContextAdapter)this).ObjectContext;
            context.ObjectStateManager.ObjectStateManagerChanged += ObjectStateManager_ObjectStateManagerChanged;
            context.ObjectMaterialized += Context_ObjectMaterialized;
            context.Connection.StateChange += Connection_StateChange;
        }

        static string GetConnectionString(string dbName, string userName = "root", string pass = "") =>
            string.Format(ConfigurationManager.ConnectionStrings["mysqlCon"].ConnectionString,
                "localhost", "3306", dbName, userName, pass);

        void Connection_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {

        }

        void Context_ObjectMaterialized(object sender, ObjectMaterializedEventArgs e)
        {
            if (!(e.Entity is EntityBase entity))
                return;
            SetEntityContext(entity);
        }

        void ObjectStateManager_ObjectStateManagerChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e)
        {
            if (!(e.Element is EntityBase entity))
                return;
            SetEntityContext(entity);
        }

        void SetEntityContext(EntityBase entity)
        {
            if (entity.Context == null)
                entity.SetContext(this);
        }

        #region GRUD

        public IQueryable<T> GetEntities<T>() where T : EntityBase => Set<T>().AsQueryable();

        public IQueryable<T> GetEntities<T>(IEnumerable<string> properties) where T : EntityBase
        {
            IQueryable<T> result = Set<T>();
            foreach (var property in properties)
            {
                result = result.Include(property);
            }
            return result;
        }

        public IQueryable GetEntities(Type typeOfEntity) => Set(typeOfEntity).AsQueryable();

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        protected ObjectContext GetObjectContext => ((IObjectContextAdapter)this).ObjectContext;

        public void Add(EntityBase entity, Type entityType) => Set(entityType).Add(entity);

        public void Add<T>(T entity) where T : EntityBase => Set<T>().Add(entity);

        public void Delete(EntityBase entity) => Set(entity.GetType()).Remove(entity);

        public void DiscardChanges(EntityBase entity)
        {
            DbEntityEntry entry = this.Entry(entity);
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.State = EntityState.Unchanged;
                    break;
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
                default: break;
            }
        }

        protected override bool ShouldValidateEntity(DbEntityEntry entityEntry) => true;

        #endregion

        #region DbSets

        public DbSet<Text> Texts { get; set; }

        public DbSet<Speaker> Speakers { get; set; }

        public DbSet<Word> Words { get; set; }

        public DbSet<Audio> Audios { get; set; }

        #endregion

        public bool IsMarkedAsDeleted(EntityBase entity) =>
            Entry(entity).State == EntityState.Deleted || Entry(entity).State == EntityState.Detached;

        protected override void OnModelCreating(DbModelBuilder modelBuilder) =>
            DbModelBuilderManager.BuildModels(modelBuilder);

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
