using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore.Design;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, CarAppContext,UserParameters>, IUserDal
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
