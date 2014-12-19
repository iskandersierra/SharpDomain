using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace SharpDomain.DDD
{
    public abstract class DomainConcept : IDomainConcept
    {
        private readonly Type _type;
        private readonly string _name;
        private readonly Func<CultureInfo, string> _description;

        protected DomainConcept()
        {
            _type = this.GetType();
            _name = GetBoundedContextName(_type);
            _description = GetBoundedContextDescription(_type);
        }

        public virtual string Name { get { return _name; } }
        public string Description { get { return GetDescription(null); } }
        public virtual string GetDescription(CultureInfo culture)
        {
            return _description(culture);
        }

        private string GetBoundedContextName(Type type)
        {
            // cache result and use attribute convention [BoundedContextName("...")]
            return type.Name;
        }

        private Func<CultureInfo, string> GetBoundedContextDescription(Type type)
        {
            // cache result and use attribute convention [Description("...")]
            return (_) => "";
        }
    }

    public abstract class BoundedContext : DomainConcept, IBoundedContext
    {
        private readonly IReadOnlyDictionary<string, IDomainObject> _domainObjects;
        private readonly IDictionary<string, IDomainObject> _domainObjectsInternal;
        private static readonly Type TypeOfIDomainObject = typeof(IDomainObject);

        protected BoundedContext()
        {
            _domainObjects = new ReadOnlyDictionary<string, IDomainObject>(_domainObjectsInternal);
        }

        public IReadOnlyDictionary<string, IDomainObject> DomainObjects { get { return _domainObjects; } }

        protected void HasDomainObjectsInThisAssembly()
        {
            var thisAssembly = Assembly.GetExecutingAssembly();
            var domainObjectTypes = thisAssembly.GetExportedTypes()
                .Where(TypeOfIDomainObject.IsAssignableFrom)
                .ToList();
            foreach (var obj in domainObjectTypes)
            {
                HasDomainObjectInternal(obj);
            }
        }

        protected void HasDomainObject(Type domainObject)
        {
            if (domainObject == null) 
                throw new ArgumentNullException("domainObject");
            if (!TypeOfIDomainObject.IsAssignableFrom(domainObject)) 
                throw new ArgumentException(string.Format("Cannot setup type {0} on domain {1}.", domainObject.FullName, Name), "domainObject");

            var instance = CreateDomainObjectInstance(domainObject);
        }

        protected void HasDomainObject<TDomainObject>() where TDomainObject : class, IDomainObject
        {
            
        }

        private void HasDomainObjectInternal(Type domainObject)
        {
            var instance = CreateDomainObjectInstance(domainObject);
        }

        private IDomainObject CreateDomainObjectInstance(Type domainObject)
        {
            return (IDomainObject) Activator.CreateInstance(domainObject);
        }
    }

    public abstract class DomainObject : DomainConcept, IDomainObject
    {
        protected DomainObject()
        {
        }
    }
}