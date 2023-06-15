using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, UserParameters>, IUserDal
    {
        public EfUserDal(CarAppContext context) : base(context)
        {

        }

        public IEnumerable<OperationClaim> GetOperationClaims(User user)
        {
            var operationClaims = _context.Set<OperationClaim>();
            var userOperationClaims = _context.Set<UserOperationClaim>();


            var result = from operationClaim in operationClaims

                         join userOperationClaim in userOperationClaims
                         on operationClaim.Id equals userOperationClaim.OperationClaimId

                         where userOperationClaim.UserId == user.Id
                         select new OperationClaim { Id = operationClaim.Id, OperationClaimName = operationClaim.OperationClaimName };

            return result.ToList();

        }
    }
}
