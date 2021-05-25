using System;
using System.Collections.Generic;
using System.Text;

namespace Boarsenger.API.BLL.Models
{
    public class EntityPage<TEntity>
    {
        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public bool CanMoveNext { get; set; }

        public bool CanMoveBack { get; set; }

        public IEnumerable<TEntity> PageData { get; set; }
    }
}
