using System;

namespace Networking
{
    [Serializable]
    public class OrganizingDTO
    {
        private string username;
        private string password;
        private string name;

        public OrganizingDTO(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public OrganizingDTO(string username, string password, string name)
        {
            this.username = username;
            this.password = password;
            this.name = name;
        }
        public virtual string Username
        {
            get
            {
                return username;
            }
            set
            {
                this.username = value;
            }
        }
        public virtual string Password
        {
            get
            {
                return password;
            }
            set
            {
                this.password = value;
            }
        }
        public virtual string Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name = value;
            }
        }
    }
}