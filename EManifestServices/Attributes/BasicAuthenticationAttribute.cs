using EManifestServices.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace EManifestServices.Attributes
{
    public class BasicAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public string Realm { get; set; } = "RaiyaGroup";

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var request = context.Request;
            var authorization = request.Headers.Authorization;

            if (authorization == null)
            {
                return;
            }

            if (authorization.Scheme != "Basic")
            {
                return;
            }

            if (String.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Enter credential.", request);
                return;
            }

            var userNameAndPasword = HeaderHelper.ExtractUserNameAndPassword(authorization.Parameter);

            if (userNameAndPasword == null ||
                string.IsNullOrWhiteSpace(userNameAndPasword.Item1) ||
                string.IsNullOrWhiteSpace(userNameAndPasword.Item2))
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid credentials.", request);
                return;
            }

            var userName = userNameAndPasword.Item1;
            var password = userNameAndPasword.Item2;

            var principal = await AuthenticateAsync(userName, password, cancellationToken);
            if (principal == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid credentials.", request);
            }
            else
            {
                context.Principal = principal;
            }
        }

        private async Task<IPrincipal> AuthenticateAsync(string userName, string password,
            CancellationToken cancellationToken)
        {
            using (EmanifestContext db = new EmanifestContext())
            {
                cancellationToken.ThrowIfCancellationRequested();
                var dbClient = db.ApiClients.Include("Carriers").FirstOrDefault(c => c.ApiClientName == userName && c.ApiClientPassword == password);
                if (dbClient == null || dbClient.IsActive == false)
                {
                    return null;
                }
                //if (userName != "IQManClient" || password != "IQManItcom1989")
                //{
                //    return null;
                //}
                var nameClaim = new Claim(ClaimTypes.Name, userName);
                var claims = new List<Claim> { nameClaim };
                if (dbClient.CarrierId != null)
                {
                    var carrierClaim = new Claim("CarrierId", dbClient.CarrierId.ToString());
                    var emailClaim = new Claim(ClaimTypes.Email, dbClient.Carriers.Email);
                    claims.Add(carrierClaim);
                    claims.Add(emailClaim);
                }

                var clientRoles = dbClient.Role.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();
                foreach (var role in clientRoles)
                {
                    var roleClaim = new Claim(ClaimTypes.Role, role);
                    claims.Add(roleClaim);
                }
                var identity = new ClaimsIdentity(claims, "Custom");
                var principal = new ClaimsPrincipal(identity);
                return principal;
            }
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            Challenge(context);
            return Task.FromResult(0);
        }

        private void Challenge(HttpAuthenticationChallengeContext context)
        {
            string parameter;

            if (String.IsNullOrEmpty(Realm))
            {
                parameter = null;
            }
            else
            {
                parameter = "realm=\"" + Realm + "\"";
            }

            context.ChallengeWith("Basic", parameter);
        }

        public virtual bool AllowMultiple
        {
            get { return true; }
        }
    }
}