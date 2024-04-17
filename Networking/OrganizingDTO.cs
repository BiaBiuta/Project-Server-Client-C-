using System;

namespace Networking
{
    [Serializable]
    public class OrganizingDTO
    {
        private string username;
        private string password;
        private string name;
        private string id;

        public OrganizingDTO(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
        public OrganizingDTO(string id,string username, string password)
        {
            this.username = username;
            this.password = password;
            this.id = id;
        }

        public virtual string Id
        {
            get => id;
            set => id = value;
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