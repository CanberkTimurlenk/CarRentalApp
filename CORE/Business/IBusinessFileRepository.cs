using Core.Entities;
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
        IResult Add(TFormFile file, TFileDtoForManipulation addedFileEntity);
        IResult Update(TFormFile file, int id, TFileDtoForManipulation updatedFileEntity);
        IResult Delete(int id);        
        IDataResult<TFileDto> GetById(int id);        
        (IDataResult<IEnumerable<TFileDto>> result, MetaData metaData) GetAll(TRequestParameters requestParameters);

    }
}
