using System;
using System.Collections.Generic;

namespace DropboxApi
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\Owner\Desktop\Projects.csv";
            List<Project> projects = ProjectsReader.ReadAllProjects(filePath);

            int count = 0;
            if (projects != null && projects.Count > 0)
            {
                foreach (Project p in projects)
                {
                    string folderPath = "/[OS'16]Projects/" + p.GroupId;
                    DropboxHelper.HandleFolderCreationAndSharing(folderPath, p.Email);
                    Console.Write("\r{0} out of {1} shared", ++count, projects.Count);
                }
            }

            return;

            #region OLD API.

            //var options = new Options
            //{
            //    ClientId = "8sey5gnuv7cwogm", //get from dropbox app console
            //    ClientSecret = "0jpd6qboqzw3x96", //get from dropbox app console
            //    AccessToken = "zSsboNlTEIAAAAAAAAAACfBmDQS38CRdF9atS6IZLl5mUODstvWSmbpHT7ubMjx3",  //get from dropbox app console
            //    RedirectUri = "https://api.dropbox.com/1/oauth/request_token"
            //};

            //// Initialize a new Client (without an AccessToken)
            //var client = new Client(options);

            //// Get the OAuth Request Url
            ////var authRequestUrl = client.Core.OAuth2.Authorize("code");

            //// TODO: Navigate to authRequestUrl using the browser, and retrieve the Authorization Code from the response
            //var authCode = "zSsboNlTEIAAAAAAAAAACgS47Ld_tp1tU2gYaTHL0Nk";

            //// Exchange the Authorization Code with Access/Refresh tokens
            //var token = client.Core.OAuth2.TokenAsync(authCode);

            //// Get root folder without content
            //var rootFolder = client.Core.Metadata.MetadataAsync("/", list: false);

            //Console.WriteLine("Root Folder: {0} (Id: {1})", rootFolder.Result.Name, rootFolder.Result.path);

            //// Get root folder with content
            //rootFolder = client.Core.Metadata.MetadataAsync("/", list: true);
            //foreach (var folder in rootFolder.Result.contents)
            //{
            //    Console.WriteLine(" -> {0}: {1} (Id: {2})",
            //        folder.is_dir ? "Folder" : "File", folder.Name, folder.path);
            //}

            //for (int i = 4; i <= 150; i++)
            //{
            //    client.Core.FileOperations.CreateFolderAsync("//[OS'16]Projects/"+i.ToString()).Wait();
            //}

            //// Find a file in the root folder
            ////var file = rootFolder.Result.contents.FirstOrDefault(x => x.is_dir == false);

            #endregion

        }

       
    }
}
