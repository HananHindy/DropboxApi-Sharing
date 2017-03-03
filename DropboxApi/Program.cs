using System;
using System.Collections.Generic;
using System.Configuration;

namespace DropboxApi
{
    class Program
    {
        static void Main(string[] args)
        {
            string oauth2AccessToken = ConfigurationManager.AppSettings["DropboxOauthToken"];
            if (string.IsNullOrEmpty(oauth2AccessToken))
            {
                Console.WriteLine("Please enter Dropbox access token");
                return;
            }

            string dropboxFolder = ConfigurationManager.AppSettings["DropboxFolderPath"];
            if (string.IsNullOrEmpty(dropboxFolder))
            {
                Console.WriteLine("Please enter a valid Dropbox Folder");
                return;
            }

            string message = ConfigurationManager.AppSettings["SharingMessage"];

            string filePath = "Groups.csv";
            List<Project> projects = ProjectsReader.ReadAllProjects(filePath);

            int count = 0;
            if (projects != null && projects.Count > 0)
            {
                foreach (Project p in projects)
                {
                    string folderPath = dropboxFolder + p.GroupId;
                    DropboxHelper.HandleFolderCreationAndSharing(oauth2AccessToken, message, folderPath, p.Email);
                    Console.Write("\r{0} out of {1} shared", ++count, projects.Count);
                }
            }

            return;
        }       
    }
}
