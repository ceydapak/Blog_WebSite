using Blog_WebSite.Models;
using Microsoft.AspNetCore.Identity;

namespace Blog_WebSite.Initializerss
{
    public class DbInitializer : IDbInitializer
    {
        private readonly DBContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DbInitializer(DBContext context,
                               UserManager<AppUser> userManager,
                               RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            if (!_roleManager.RoleExistsAsync(BlogRoles.BlogAdmin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(BlogRoles.BlogAdmin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(BlogRoles.BlogAuthor)).GetAwaiter().GetResult();
                _userManager.CreateAsync(new AppUser()
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    Name = "Super",
                    LastName = "Admin"
                }, "Admin@0011").Wait();

                var appUser = _context.AppUsers!.FirstOrDefault(x => x.Email == "admin@gmail.com");
                if (appUser != null)
                {
                    _userManager.AddToRoleAsync(appUser, BlogRoles.BlogAdmin).GetAwaiter().GetResult();
                }


                var listOfPages = new List<Page>()
                {
                    new Page()
                    {
                         Title = "About Us",
                        Slug = "about"
                    },
                    new Page()
                    {
                        Title = "Contact Us",
                        Slug = "contact"
                    },
                    new Page()
                    {
                        Title = "Privacy Policy",
                        Slug = "privacy"
                    }
                 };

                _context.Pages!.AddRange(listOfPages);

                //var setting = new Setting
                //{
                //    SiteName = "Site Name",
                //    Title = "Site Title",
                //    ShortDescription = "Short Description of site"
                //};

                //_context.Settings!.Add(setting);
                _context.SaveChanges();

            }
        }
    }
}
