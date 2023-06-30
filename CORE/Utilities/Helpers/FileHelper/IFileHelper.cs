using Core.Utilities.Results.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Helpers.FileHelper
{
    public interface IFileHelper
    {
        Task<IDataResult<string>> UploadFile(IFormFile uploadedFile, string root);
        IResult DeleteFile(string deletedFileAbsoluteFilepath);
        Task<IDataResult<string>> UpdateFile(IFormFile newFile, string oldFileAbsoluteFilepath);

    }
}
