namespace guesttalktask.Helper
{
    using guesttalktask.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable]
    public class XMLreader
    {
        private const string DSPath = @"C:\Temp\GTT";

        public List<User> Users { get; set; }

        public bool Load() {
            try {
                string UserFile = Path.Combine(DSPath, "Users.xml");
                if (!Directory.Exists(DSPath))
                {
                    Directory.CreateDirectory(DSPath);
                }
                if (File.Exists(UserFile))
                {
                    XmlSerializer reader = new XmlSerializer(typeof(List<User>));
                    StreamReader sr = new StreamReader(UserFile);
                    Users = (List<User>)reader.Deserialize(sr);
                    sr.Close();
                }
                if (Users == null)
                {
                    Users = new List<User>();
                }

                return true;
            } catch { Users = null; return false; }
        }

        public bool Save()
        {
            try
            {
                // Build the xml users file path
                string strUsersFile = Path.Combine(DSPath, "Users.xml");

                // Get from the database the users
                if (!Directory.Exists(DSPath))
                {
                    Directory.CreateDirectory(DSPath);
                }
                XmlSerializer writer = new XmlSerializer(typeof(List<User>));
                FileStream fs = File.Create(strUsersFile);
                writer.Serialize(fs, Users);
                fs.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }

        
    }
}
