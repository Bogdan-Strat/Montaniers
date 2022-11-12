using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Montaniarzii.Common.DTOs
{
    public class ByteSelectListItemModel<TEntity>
        where TEntity : class, IEntity
    {
        public byte Id { get; set; }
        public string Name { get; set; }
    }
}
