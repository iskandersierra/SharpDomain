using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDomain.Reflection
{
    public interface ITypeBasedRegistry<TItem>
    {
        void Register(Type type, TItem item);
    }
}
