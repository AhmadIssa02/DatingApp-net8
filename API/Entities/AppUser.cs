using System;

namespace API.Entities;

public class AppUser
{

    public int Id { get; set; }
    public required string Username { get; set; }
    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }

}

public class AppUser2
{

    public int Id { get; set; }
    public required string Username { get; set; }
   

}
