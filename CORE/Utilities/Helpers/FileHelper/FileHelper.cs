using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Core.Utilities.Helpers.GuidHelperr;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Helpers.Constants.HelperMessages;

namespace Core.Utilities.Helpers.FileHelper
{
    public class FileHelper : IFileHelper
    {
        public IDataResult<string> UploadFile(IFormFile uploadedFile, string root)
        {
            //
            // Summary:
            //     Gets a file, save it to demanded location. Return relative filepath with success information
            //     Otherwise returns null.

            if (uploadedFile != null) // careful about here !!
            {

                if (!Directory.Exists(root))
                {

                    Directory.CreateDirectory(root);  // create directory if it is not exists


                }

                string extension;
                string guid;
                string relativeFilepath;


                extension = Path.GetExtension(uploadedFile.FileName);
                guid = GuidHelper.CreateGuid();
                relativeFilepath = guid + extension;  // add the file extension to given guid parameter

                using (FileStream fileStream = File.Create(Path.Combine(root, relativeFilepath)))
                {
                    uploadedFile.CopyTo(fileStream);
                    fileStream.Flush();


                }
                return new SuccessDataResult<string>(relativeFilepath,FileHelperMessages.FileUploaded);    // has two string parameter, indicate call with data
            }

            return null;

        }
        public IResult DeleteFile(string deletedFileAbsoluteFilepath)
        {
            //
            // Summary:
            //     Deletes the file if such a file exists 
            //     Otherwise returns, file not exist message

            if (File.Exists(deletedFileAbsoluteFilepath))
            {
                File.Delete(deletedFileAbsoluteFilepath);

                return new SuccessResult(FileHelperMessages.FileDeleted);



            }

            return new ErrorResult(FileHelperMessages.FileNotExist);



        }
        public IDataResult<string> UpdateFile(IFormFile newFile, string oldFileAbsoluteFilepath)
        {
            //
            // Summary:
            //     If such a file exists in given location, update it with came from the method call.
            //     otherwise returns file not exist message.

            if (File.Exists(oldFileAbsoluteFilepath))
            {
                var newFilePath = Path.GetDirectoryName(oldFileAbsoluteFilepath) ;

                File.Delete(oldFileAbsoluteFilepath);
                

                var result = UploadFile(newFile, newFilePath).Data;

                return new SuccessDataResult<string>(result,FileHelperMessages.FileUpdated);


            }

            return new ErrorDataResult<string>(message:FileHelperMessages.FileNotExist);
        }
    }
}
