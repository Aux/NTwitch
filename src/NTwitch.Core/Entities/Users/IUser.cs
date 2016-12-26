using System;

namespace NTwitch
{
    public interface IUser : IEntity
    {
        string Bio { get; set; }
        DateTime CreatedAt { get; set; }
        string DisplayName { get; set; }
        string LogoUrl { get; set; }
        string Name { get; set; }
        string Type { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}
