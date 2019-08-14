using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TheCreatives.Models;

namespace Services
{
    public class IdentityHelper
    {
        internal static void SeedIdentities(DbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            //Create the Clerk Role
            if (!roleManager.RoleExists(RoleName.ROLE_CLERK))
            {
                var roleresult = roleManager.Create(new IdentityRole(RoleName.ROLE_CLERK));
            }

            //Create the Doctor Role
            if (!roleManager.RoleExists(RoleName.ROLE_DOCTOR))
            {
                var roleresult = roleManager.Create(new IdentityRole(RoleName.ROLE_DOCTOR));
            }

            //Creating the Clerk user
            string UserName = "clerk@clerk.com";
            string Password = "clerk000";

            //Adding user to the Clerk role
            ApplicationUser user = userManager.FindByName(UserName);

            if (user == null)
            {
                user = new ApplicationUser()

                {
                    UserName = UserName,

                    Email = UserName,

                    EmailConfirmed = true
                };

                IdentityResult userResult = userManager.Create(user, Password);

                if (userResult.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, RoleName.ROLE_CLERK);
                }


                //Creating the Doctor users
                string _UserName = "doc@doc.com";
                string _Password = "doc000";

                //Adding user to the Clerk role
                ApplicationUser _user = userManager.FindByName(_UserName);

                if (user == null)
                {
                    user = new ApplicationUser()
                    {
                        UserName = _UserName,

                        Email = _UserName,

                        EmailConfirmed = true
                    };

                    IdentityResult _userResult = userManager.Create(user, _Password);

                    if (userResult.Succeeded)
                    {
                        var result = userManager.AddToRole(user.Id, RoleName.ROLE_DOCTOR);
                    }

                }
            }
        }
    }
}
