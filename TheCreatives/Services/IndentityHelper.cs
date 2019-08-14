using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TheCreatives.Models;

namespace Services
{
    public class IdentityHelper
    {
        //Create a method that will create the roles and users
        internal static void SeedIdentities(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            //Create the clerk Role
            if (!roleManager.RoleExists(RoleName.ROLE_CLERK))
            {
                var roleresult = roleManager.Create(new IdentityRole(RoleName.ROLE_CLERK));
            }



            //Create the doctor Role
            if (!roleManager.RoleExists(RoleName.ROLE_DOCTOR))
            {
                var roleresult = roleManager.Create(new IdentityRole(RoleName.ROLE_DOCTOR));
            }


            //Creating the clerk user
            string userName = "clerk@clerk.com";
            string password = "clerk@clerk";

            //Adding user to the clerk role
            ApplicationUser user = userManager.FindByName(userName);

            if (user == null)
            {
                user = new ApplicationUser()

                {
                    UserName = userName,

                    Email = userName,

                    EmailConfirmed = true
                };

                IdentityResult userResult = userManager.Create(user, password);

                if (userResult.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, RoleName.ROLE_CLERK);

                }
            }

            //Creating the doctor user
            string _username = "doc@doc.com";
            string _password = "doc000";

            //Adding user to the clerk role
            ApplicationUser _user = userManager.FindByName(_username);

            if (_user == null)
            {
                _user = new ApplicationUser()

                {
                    UserName = _username,

                    Email = _username,

                    EmailConfirmed = true
                };

                IdentityResult userResult = userManager.Create(_user, _password);

                if (userResult.Succeeded)
                {
                    var result = userManager.AddToRole(_user.Id, RoleName.ROLE_DOCTOR);

                }
            }
        }
    }
}
