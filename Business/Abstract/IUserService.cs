using Core.Business;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Entities.Concrete.DTOs.OperationClaim;
using Entities.Concrete.DTOs.User;
using Entities.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService : IBusinessRepository<UserDto,UserDtoForManipulation>
    {

        IDataResult<IEnumerable<OperationClaimDto>> GetOperationClaims (UserDto userDto);
        IDataResult<UserDto> GetByEmail(string email);
        


    }
}
