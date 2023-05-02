using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStat.DAL.Core
{
    public class DALObject
    {
        protected readonly DatabaseContext _context;

        public DALObject(DatabaseContext context)
        {
            _context = context;
        }
    }
}
