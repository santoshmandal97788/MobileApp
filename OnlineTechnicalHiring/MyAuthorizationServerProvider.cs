using Microsoft.Owin.Security.OAuth;
using OnlineTechnicalHiring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace OnlineTechnicalHiring
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private OnlineTechnicalProfessionalHiringSystemEntities db = new OnlineTechnicalProfessionalHiringSystemEntities();
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            var adminUser = db.tblAdmins.Where(a => a.Email == context.UserName && a.Password == context.Password).FirstOrDefault();
            var users = db.tblUsers.Where(a => a.Email == context.UserName && a.Password == context.Password).FirstOrDefault();
            var technicians = db.tblTechnicians.Where(a => a.Email == context.UserName && a.Password == context.Password).FirstOrDefault();

            if (adminUser != null)
            {

                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                //identity.AddClaim(new Claim("Email", adminUser.Email));
                identity.AddClaim(new Claim(ClaimTypes.Name, adminUser.Email));
                context.Validated(identity);
            }
            else if (users != null)
            {

                // var loggedInStudentId = studentUser.Student_Id;

                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                identity.AddClaim(new Claim("Id", users.UId.ToString()));
                identity.AddClaim(new Claim("Email", users.Email));
                //  identity.AddClaim(new Claim(ClaimTypes.Name, studentUser.Name));
                context.Validated(identity);
            }
            else if (technicians != null)
            {
                // var loggedInTeacherId = studentUser.Student_Id;
                identity.AddClaim(new Claim(ClaimTypes.Role, "technicians"));
                identity.AddClaim(new Claim("Id", technicians.TID.ToString()));
                identity.AddClaim(new Claim("Email", technicians.Email));
                identity.AddClaim(new Claim(ClaimTypes.Name, technicians.Email));
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_Grant", "Incorrect Username and Password");
                return;
            }
        
        }


    }
}

