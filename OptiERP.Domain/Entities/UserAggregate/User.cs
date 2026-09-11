using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using OptiERP.Domain.Entities.UserAggregate.Model;

namespace OptiERP.Domain.Entities;

public class User

{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public bool IsActive { get; set; }
    public UserType UserType { get; set; }

    public DateTime CreatedAt { get; set; }

    private User()
    {

    }

    private User(
    string username,
    string email,
    UserType userType,
    string passwordHash)
    {
        Id = Guid.NewGuid();
        Username = username;
        Email = email;
        PasswordHash = passwordHash;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        UserType = userType;
    }

    public static User Create(
        string username,
        string email,
        UserType userType,
        string passwordHash)
    {
        return new User(
            username,
            email,
            userType,
            passwordHash);
    }

    public void Update(
        string username,
        string email)
    {
        Username = username;
        Email = email;
    }

}