using Core.Entities;
using Core.Entities.Abstract;
using Core.Entities.Concrete.RequestFeatures;
using Core.Utilities.Results.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business
{
    public interface IBusinessFileRepository<TFormFile, TFileDto, TFileDtoForManipulation, TRequestParameters>
            where TFormFile : class, IFormFile
            where TFileDto : class, IDto, new()
            where TFileDtoForManipulation : class, IDto, new()
            where TRequestParameters : RequestParameters, new()
    {
        Task<IResult> AddAsync(TFormFile file, TFileDtoForManipulation addedFileEntity);
        Task<IResult> UpdateAsync(TFormFile file, int id, TFileDtoForManipulation updatedFileEntity, bool trackChanges);
        Task<IResult> DeleteAsync(int id, bool trackChanges);
        Task<IDataResult<TFileDto>> GetByIdAsync(int id, bool trackChanges);
        Task<(IDataResult<IEnumerable<TFileDto>> result, MetaData metaData)> GetAllAsync(TRequestParameters requestParameters, bool trackChanges);

    }
}
