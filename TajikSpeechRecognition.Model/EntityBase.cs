using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TajikSpeechRecognition.Model
{
    public abstract class EntityBase : INotifyPropertyChanged
    {
        public EntityBase() { }

        public EntityBase(EntityContext context)
        {
            Context = context;
            Context.Add(this, this.GetType());
        }

        private Guid guid;
        public Guid Guid
        {
            get
            {
                if (guid == Guid.Empty) guid = Guid.NewGuid();
                return guid;
            }
            protected set
            {
                if (guid == Guid.Empty)
                    guid = value;
            }
        }

        EntityContext _context;
        public virtual EntityContext Context
        {
            get => _context;
            set => OnPropertySetting(nameof(Context), value, ref _context);
        }

        protected void OnPropertySetting<TProperty>(string propertyName, TProperty newValue, ref TProperty oldValue)
        {
            //var _newValue = oldValue;
            //if ((_newValue as object) != (oldValue as object))
            //    RaisePropertyChanged(propertyName);
            //BLManager.PropertySetter(this, propertyName, newValue, oldValue, (TProperty value) => { _newValue = value; RaisePropertyChanged(propertyName); });
            oldValue = newValue;
            RaisePropertyChanged(propertyName);
        }

        public void SetContext(EntityContext context) => Context = context;

        public void Delete() => Context.Delete(this);

        internal void AddToContext(EntityContext context)
        {
            if (context != null)
            {
                context.Add(this, this.GetType());
                Context = context;
            }
        }

        #region Indexer

        /// <summary>
        /// Gets or sets the value for a specified property
        /// </summary>
        /// <param name="propertyName">Property name</param>
        /// <returns></returns>
        public object this[string propertyName]
        {
            get
            {
                object obj = null;
                if (propertyName != null && propertyName.Contains("."))
                {
                    int index = propertyName.IndexOf(".");
                    string transferablePropertyName = propertyName.Substring(0, index);

                    EntityBase transferable = this[transferablePropertyName] as EntityBase;
                    if (transferable == null)
                        return null;

                    obj = transferable[propertyName.Substring(index + 1)];
                    return obj;

                }
                Type type = GetType();
                if (type.GetTypeInfo().IsAssignableFrom(typeof(IList<>).GetTypeInfo()))
                {
                    if (propertyName == "Count")
                    {
                        obj = type.GetProperty(propertyName).GetValue(this);
                    }
                }
                else
                {
                    PropertyInfo propertyInfo = type.GetProperty(propertyName);
                    if (propertyInfo == null)
                    {
                        throw new ArgumentException("The '" + propertyName + "' property does not exist in " + type.Name);
                    }
                    obj = propertyInfo.GetValue(this);
                }
                return obj;
            }
            set
            {
                Type type = GetType();
                PropertyInfo propertyInfo = type.GetProperty(propertyName);
                if (propertyInfo == null)
                {
                    throw new ArgumentException("The '" + propertyName + "' property does not exist in " + type.Name);
                }
                if (propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(this, value);
                }
                else
                {
                    throw new InvalidOperationException("Attempting to set a readonly property.  Type:" + type.Name + " Property:" + propertyName);
                }
            }
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        internal void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    public class EntityBaseConfiguration
    {
        public static void Configure(DbModelBuilder modelBuilder, string prefix)
        {
            modelBuilder.Ignore<EntityBase>();
            var config = modelBuilder.Entity<EntityBase>();
            config.Ignore(eb => eb.Context);
        }
    }
}
