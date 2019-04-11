using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace padrao.Models
{
    public class FileUpload
    {
       
        public async Task Upload(HttpRequestMessage Request, String Address, List<string> newNames)
        {
            var ctx = HttpContext.Current;
            var root = ctx.Server.MapPath(Address);
            var provider = new MultipartFormDataStreamProvider(root);
            var index = 0;

            await Request.Content
                    .ReadAsMultipartAsync(provider);
            foreach (var file in provider.FileData)
            {
                if (file.Headers.ContentDisposition.Name.IndexOf("file") > -1)
                {
                    string name = file.Headers.ContentDisposition.FileName.Trim('"');
                    string newFileName = newNames[index++];//Guid.NewGuid().ToString();
                    delete(newFileName);
                    File.Move(file.LocalFileName, Path.Combine(root, newFileName));
                }
            }

        }

        public void delete(string file)
        {
            if (File.Exists(HttpContext.Current.Server.MapPath("~/documents") + "\\" + file))
            {
                File.Delete(HttpContext.Current.Server.MapPath("~/documents") + "\\" + file);
            }

        }
        public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
        {
            public CustomMultipartFormDataStreamProvider(string path) : base(path) { }

            public override string GetLocalFileName(HttpContentHeaders headers)
            {
                return headers.ContentDisposition.FileName.Replace("\"", string.Empty);
            }
        }

        //SEM USO
        public Task<HttpResponseMessage> SaveFile(HttpRequestMessage Request, String Address, String NewName)
        {
            List<string> savedFilePath = new List<string>();
            delete(NewName + ".jpg");
            // Check if the request contains multipart/form-data
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            //Get the path of folder where we want to upload all files.
            string rootPath = HttpContext.Current.Server.MapPath(Address);
            CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(rootPath);
            //var provider = new MultipartFileStreamProvider(rootPath);
            // Read the form data.
            //If any error(Cancelled or any fault) occurred during file read , return internal server error
            var task = Request.Content.ReadAsMultipartAsync(provider).
                ContinueWith<HttpResponseMessage>(t =>
                {
                    if (t.IsCanceled || t.IsFaulted)
                    {
                        Request.CreateErrorResponse(HttpStatusCode.InternalServerError, t.Exception);
                    }
                    foreach (MultipartFileData dataitem in provider.FileData)
                    {
                        try
                        {
                            if (dataitem.Headers.ContentDisposition.Name.IndexOf("file") > -1)
                            {
                                //Replace / from file name
                                string name = dataitem.Headers.ContentDisposition.FileName.Replace("\"", "");
                                //Create New file name using GUID to prevent duplicate file name
                                //string newFileName = Guid.NewGuid() + Path.GetExtension(name);
                                string newFileName = NewName + Path.GetExtension(name);
                                //Move file from current location to target folder.
                                File.Move(dataitem.LocalFileName, Path.Combine(rootPath, newFileName));
                            }

                        }
                        catch (Exception ex)
                        {
                            string message = ex.Message;
                        }
                    }

                    return Request.CreateResponse(HttpStatusCode.Created, savedFilePath);
                });
            delete("delete.jpg");
            return task;
        }
    }
}