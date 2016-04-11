using Dropbox.Api.Files;
using Dropbox.Api.Sharing;
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

    }
}
