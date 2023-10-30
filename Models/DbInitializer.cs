using Microsoft.AspNetCore.Identity;

namespace Blog_WebSite.Models
{
    public class DbInitializer : IDbInitializer
    {
        private readonly DBContext _context;
        private readonly UserManager<AppUser> _userManager; 
        private readonly RoleManager<IdentityRole> _roleManager;    
        public DbInitializer(DBContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
                
        }
        public void Initialize()
        {
            if(!_roleManager.RoleExistsAsync(Roles.Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(Roles.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(Roles.Author)).GetAwaiter().GetResult();
                _userManager.CreateAsync(new AppUser()
                {
                    UserName = "adminov@gmail.com",
                    Email="admin@gmail.com",
                    Name= "Admin",
                    LastName="Adminov"
                }, "Ad2346").Wait();

                var user1 = _context.AppUsers!.FirstOrDefault(u => u.Email=="admin@gmail.com");
                if (user1 != null)
                {
                    _userManager.AddToRoleAsync(user1, Roles.Admin).GetAwaiter().GetResult();
                }

                var aboutpage = new Page()
                {
                    Title = "About Us",
                    Slug = "about"
                };

                var contactpage = new Page()
                {
                    Title = "Contact Us",
                    Slug = "contact"
                };

                var privppage = new Page()
                {
                    Title = "Privacy Policies",
                    Slug = "privacy"
                };

                _context.Pages.Add(aboutpage);
                _context.Pages.Add(contactpage);
                _context.Pages.Add(privppage);
                _context.SaveChanges();


            }
        }
    }
}
