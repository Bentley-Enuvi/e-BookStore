using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eBookStoreAPI.Domain.Entities;

public class User: EntityBase
{
    public int Id { get; set; }
    public string Username { get; private set; }
    public string PasswordHash { get; private set; }

    public User() { }

    public User(string username, string passwordHash)
    {
        Username = username;
        PasswordHash = passwordHash;
    }
}
