using Core.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;

namespace DataAccess.Abstract
{
    public interface IUserDal : IRepositoryBase<User,UserParameters>
    {
        IEnumerable<OperationClaim> GetOperationClaims(User user);
    }
}
