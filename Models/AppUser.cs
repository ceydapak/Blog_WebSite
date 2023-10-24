using Microsoft.AspNetCore.Identity;

namespace Blog_WebSite.Models
{
    public class AppUser:IdentityUser
    {
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public List<Post>? Posts { get; set; }
    }
}
