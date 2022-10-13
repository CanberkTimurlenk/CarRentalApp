using Core.Entities;
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
    public interface IBusinessFileRepository<TFormFile, TFileEntity>
            where TFormFile : class, IFormFile
            where TFileEntity : class, IFileEntity, new()
    {

        IResult Add(TFormFile files, TFileEntity addedFileEntity);
        IResult Update(TFormFile files, TFileEntity updatedFileEntity);
        IResult Delete(TFileEntity deletedFileEntity);
        IDataResult<List<TFileEntity>> GetAll();
        IDataResult<TFileEntity> GetById(int id);



    }
}
