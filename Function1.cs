using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;

namespace FunctionsAssemblyLoadingRepro
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                RequireExpirationTime = true,
                RequireSignedTokens = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("eQ8aTuThdJZ")),

                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = false,
            };

            new JsonWebTokenHandler().ValidateToken("not a real JWT", tokenValidationParameters);

            return new OkObjectResult("Function executed.");
        }
    }
}
