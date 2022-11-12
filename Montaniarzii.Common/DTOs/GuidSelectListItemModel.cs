using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.Common.DTOs
{
    public class GuidSelectListItemModel<TEntity>
        where TEntity : class, IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
