using eStat.DAL.Core.Context.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStat.BLL.Core
{
    public class BusinessObject
    {
        protected readonly IDALContext _dalContext;

        public BusinessObject(IDALContext dalContext)
        {
            _dalContext = dalContext;
        }
    }
}