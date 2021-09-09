using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Base
{
    public class BasicEntity
    {
        public Guid Id { get; protected init; }

    protected BasicEntity()
        {

        }

        protected BasicEntity(Guid id)
        {
            Id = id;
        }
    }
}
