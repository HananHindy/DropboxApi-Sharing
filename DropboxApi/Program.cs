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
        }       
    }
}
