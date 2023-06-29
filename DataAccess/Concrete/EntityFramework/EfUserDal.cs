using Core.DataAccess.EntityFramework;
using Core.Entities.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Contexts;
using Entities.Concrete.Models;
using Entities.Concrete.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User>, IUserDal
    {
        public EfUserDal(CarAppContext context) : base(context)
        {

        }

        public new void Update(User user)
        {
            
            var trackedEntity = _context.Set<User>().Local.SingleOrDefault(u => u.Id == user.Id);
            
            if (trackedEntity is not null)
                _context.Entry(trackedEntity).State = EntityState.Detached;
           
            _context.Set<User>().Update(user);
            
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
