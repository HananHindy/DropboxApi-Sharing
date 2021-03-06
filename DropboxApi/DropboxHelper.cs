﻿using Dropbox.Api.Files;
using Dropbox.Api.Sharing;
using DropboxRestAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropboxApi
{
    public static class DropboxHelper
    {
        public static void HandleFolderCreationAndSharing(string folderPath, string emailAddress)
        {
            // GET Dropbox oauth2AccessToken from the Dropbox app you create.
            var dropboxClient = new Dropbox.Api.DropboxClient("");

            FolderMetadata folderData;

            // Try to Get folder
            try
            {
                var folders = dropboxClient.Files.ListFolderAsync(folderPath, true).Result;
                folderData = folders.Entries[0].AsFolder;
            }
            catch
            {
                // Create the folder if not exist
                folderData = dropboxClient.Files.CreateFolderAsync(folderPath).Result;
            }

            string shareId = folderData.SharedFolderId;
            //Initial Share if not sharing enabled on the folder
            //This only shares the folder with the Dropbox owner
            if (folderData.SharedFolderId == null)
            {
                shareId = dropboxClient.Sharing.ShareFolderAsync(folderPath).Result.AsComplete.Value.SharedFolderId;

            }


            MemberSelector.Email mailMember = new MemberSelector.Email(emailAddress);
            AddMember addMember = new AddMember(mailMember, AccessLevel.Editor.Instance);

            AddFolderMemberArg args2 = new AddFolderMemberArg(shareId, new List<AddMember>() { addMember }, false, "This is a message sent from the app trial");

            dropboxClient.Sharing.AddFolderMemberAsync(args2).Wait();
        }

        public static void CreateFolderWithOldAPI(string filePath)
        {
            #region OLD API.

            var options = new Options
            {
                ClientId = "", //get from dropbox app console
                ClientSecret = "", //get from dropbox app console
                AccessToken = "",  //get from dropbox app console
                RedirectUri = ""
            };

            // Initialize a new Client (without an AccessToken)
            var client = new Client(options);

            // Get the OAuth Request Url Used once
            //var authRequestUrl = client.Core.OAuth2.Authorize("code");

            // TODO: Navigate to authRequestUrl using the browser, and retrieve the Authorization Code from the response
            var authCode = "";

            // Exchange the Authorization Code with Access/Refresh tokens
            var token = client.Core.OAuth2.TokenAsync(authCode);

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


            client.Core.FileOperations.CreateFolderAsync(filePath).Wait();
         
            // Find a file in the root folder
            //var file = rootFolder.Result.contents.FirstOrDefault(x => x.is_dir == false);

            #endregion

        }
    }
}
