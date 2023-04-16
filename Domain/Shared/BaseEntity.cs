using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared
{
    public abstract class BaseEntity<TKey> : IBaseEntity<TKey>
    {
        public abstract TKey Id { get; set; }
    }
}
