using System;

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
