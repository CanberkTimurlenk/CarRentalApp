using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore.Design;
using Entities.Concrete.Models;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, CarAppContext>, IUserDal
    {
        private readonly IDesignTimeDbContextFactory<CarAppContext> _contextFactory;

        public EfUserDal(IDesignTimeDbContextFactory<CarAppContext> contextFactory)
            : base(contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public IEnumerable<OperationClaim> GetOperationClaims(User user)
        {
            using (var context = _contextFactory.CreateDbContext(new string[0]))
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.Id
                             select new OperationClaim { Id = operationClaim.Id, OperationClaimName = operationClaim.OperationClaimName };
                
                var r = result.ToList();
                return result.ToList();

            }
            
        }
    }
}
