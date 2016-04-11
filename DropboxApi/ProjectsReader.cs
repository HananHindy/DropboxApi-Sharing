using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DropboxApi
{
    public class Project
    {
        public string GroupId { get; set; }
        public string Email { get; set; }
    }


    public static class ProjectsReader
    {
        public static List<Project> ReadAllProjects(string filePath)
        {
            FileStream FS = new FileStream(filePath, FileMode.Open);
            StreamReader SR = new StreamReader(FS);
            SR.ReadLine();

            List<Project> p = new List<Project>();
            while (SR.EndOfStream == false)
            {
                string temp = SR.ReadLine();
                string[] temp2 = temp.Split(',');
                p.Add(new Project()
                {
                    GroupId = temp2[0].Trim(),
                    Email = temp2[1].Trim()
                }
                );
            }

            SR.Close();
            FS.Close();
            return p;
        }
    }
}
