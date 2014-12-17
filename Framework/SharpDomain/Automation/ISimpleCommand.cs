using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDomain.Automation
{
    public interface ISimpleCommand
    {

    }

    public interface ISimpleCommandDefinition
    {
        string Name { get; }
        string Caption { get; }
        string ShortDescription { get; }
        string Description { get; }

        IEnumerable<SimpleCommandParameterDescription> Parameters { get; }

        ISimpleCommand Create();
    }

    public class SimpleCommandParameterDescription
    {


        public string Name { get; private set; }
        public string Caption { get; private set; }
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }
        public int Order { get; private set; }
        public bool IsPositionParameter { get; private set; }
        public bool IsOptional { get; set; }
    }
}
