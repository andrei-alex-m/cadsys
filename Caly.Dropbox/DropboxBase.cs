using System;

using Dropbox.Api;
using Dropbox.Api.Files;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Caly.Dropbox
{
    public class DropBoxBase
    {
        #region Variables  
        private DropboxClient DBClient;
        #endregion

        #region Constructor  
        public DropBoxBase()
        {
            try
            {
                GenerateAccessToken();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Properties  
        public string AppName => "CadGen";


        public string AccessToken => "p5L439MIDIkAAAAAAAAHD-_eE6ucNeU6dICc7EHCVnhfciaNH5VAvraS0PcUEtCa";

        #endregion

        #region UserDefined Methods  

        /// <summary>  
        /// This method is to generate Access Token required to access dropbox outside of the environment (in ANy application).  
        /// </summary>  
        /// <returns></returns>  
        public void GenerateAccessToken() //=> "p5L439MIDIkAAAAAAAAHDP6DYNaCltCpGwTpXIjW_RXOpKoSpi4fIOXxjpjKbrDJ";
        {
            try
            {
                DropboxClientConfig CC = new DropboxClientConfig(AppName, 1);
                HttpClient HTC = new HttpClient();
                HTC.Timeout = TimeSpan.FromMinutes(10); // set timeout for each ghttp request to Dropbox API.  
                CC.HttpClient = HTC;
                DBClient = new DropboxClient(AccessToken, CC);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public Task<FolderMetadata> CreateFolder(string path)
        //{
        //    var folderArg = new CreateFolderArg(path);
        //    var folder = DBClient.Files.CreateFolderV2Async(folderArg).Result;
        //    return folder.Metadata;
        //}

        /// <summary>  
        /// Method is to check that whether folder exists on Dropbox or not.  
        /// </summary>  
        /// <param name="path"> Path of the folder we want to check for existance.</param>  
        /// <returns></returns>  
        public bool FolderExists(string path)
        {
            try
            {
                var folders = DBClient.Files.ListFolderAsync(path).Result;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<string> ListFolder(string path, bool folders = true, bool files = true, string filters = "")
        {
            var list = DBClient.Files.ListFolderAsync(path).Result;

            foreach (var i in list.Entries)
            {
                if (((i.IsFolder && folders) ||
                        (i.IsFile && files)) &&
                    (string.IsNullOrEmpty(filters) || i.Name.Contains(filters, StringComparison.InvariantCultureIgnoreCase)))
                {
                    yield return i.Name;
                }

            }
        }

        /// <summary>  
        /// Method to delete file/folder from Dropbox  
        /// </summary>  
        /// <param name="path">path of file.folder to delete</param>  
        /// <returns></returns>  
        public bool Delete(string path)
        {
            try
            {
                var folders = DBClient.Files.DeleteV2Async(path).Result;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>  
        /// Method to upload files on Dropbox  
        /// </summary>  
        /// <param name="UploadfolderPath"> Dropbox path where we want to upload files</param>  
        /// <param name="UploadfileName"> File name to be created in Dropbox</param>  
        /// <param name="SourceFilePath"> Local file path which we want to upload</param>  
        /// <returns></returns>  
        public bool Upload(string UploadfolderPath, string UploadfileName, string SourceFilePath)
        {
            try
            {
                using (var stream = new MemoryStream(File.ReadAllBytes(SourceFilePath)))
                {
                    var response = DBClient.Files.UploadAsync(UploadfolderPath + "/" + UploadfileName, WriteMode.Overwrite.Instance, true, body: stream).Result;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        /// <summary>  
        /// Method to Download files from Dropbox  
        /// </summary>  
        /// <param name="DropboxFolderPath">Dropbox folder path which we want to download</param>  
        /// <param name="DropboxFileName"> Dropbox File name availalbe in DropboxFolderPath to download</param>  
        /// <param name="DownloadFolderPath"> Local folder path where we want to download file</param>  
        /// <param name="DownloadFileName">File name to download Dropbox files in local drive</param>  
        /// <returns></returns>  
        public bool Download(string DropboxFolderPath, string DropboxFileName, string DownloadFolderPath, string DownloadFileName)
        {
            try
            {
                var response = DBClient.Files.DownloadAsync(DropboxFolderPath + "/" + DropboxFileName);
                var result = response.Result.GetContentAsStreamAsync(); //Added to wait for the result from Async method  

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion
    }
}

