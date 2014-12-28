using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SharpDomain.Commanding;

namespace ContactsDomain.Commands
{
    public interface CreateContact : ICreationCommand, IRequestCommand
    {
        string Title { get; set; }
    }

    public interface UpdateContactTitle : IInstanceCommand, IRequestCommand
    {
        string Title { get; set; }
    }
}
