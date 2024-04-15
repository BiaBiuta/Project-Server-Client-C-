using System;

namespace Domain.domain;

public class Organizing :Entity<Int32>
{
    public Organizing(string name, string username, string password)
    {
        Name = name;
        Username = username;
        Password = password;
    }

    public Organizing(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public String Name { get; set; }
    public String Username { get; set; }
    public String Password { get; set; }
}