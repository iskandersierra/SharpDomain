using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDomain.DDD
{
    public interface IDomainConcept
    {
        string Name { get; }
        string Description { get; }
        string GetDescription(CultureInfo culture);
    }

    public interface IBoundedContext : IDomainConcept
    {
        IReadOnlyDictionary<string, IDomainObject> DomainObjects { get; }

    }

    public interface IDomainObject : IDomainConcept
    {
        
    }
}
