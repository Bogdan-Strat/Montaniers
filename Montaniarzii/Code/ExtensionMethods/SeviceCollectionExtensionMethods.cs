using Montaniarzii.BusinessLogic.Base;
using Montaniarzii.BusinessLogic.Implementation;
using Montaniarzii.BusinessLogic.Implementation.Account;
using Montaniarzii.Code.Base;
using Montaniarzii.Common.DTOs;
using System.Security.Claims;

namespace Montaniarzii.Code.ExtensionMethods
{
    public static class SeviceCollectionExtensionMethods
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddScoped<ControllerDependencies>();

            return services;
        }

        public static IServiceCollection AddMontaniarziiBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<ServiceDependencies>();
            services.AddScoped<BaseService>();
            services.AddScoped<UserService>();
            services.AddScoped<TripService>();
            services.AddScoped<AttractionService>();
            services.AddScoped<TripXAttractionService>();
            services.AddScoped<InvitationService>();
            services.AddScoped<FollowService>();
            services.AddScoped<WarningService>();
            services.AddScoped<SuggestionAttractionService>();
            services.AddScoped<TrainStationService>();
            services.AddScoped<PhotoService>();
            services.AddScoped<LikeService>();

            return services;
        }

        public static IServiceCollection AddMontaniarziiCurrentUser(this IServiceCollection services)
        {
            services.AddScoped(s =>
            {
                var accessor = s.GetService<IHttpContextAccessor>();
                var httpContext = accessor.HttpContext;
                var claims = httpContext.User.Claims;

                var userIdClaim = claims?.FirstOrDefault(c => c.Type == "Id")?.Value;
                var isParsingSuccessful = Guid.TryParse(userIdClaim, out Guid id);

                return new CurrentUserDto
                {
                    Id = id,
                    IsAuthenticated = httpContext.User.Identity.IsAuthenticated,
                    Email = claims?.FirstOrDefault(c => c.Type == "Email")?.Value,
                    Username = claims?.FirstOrDefault(c => c.Type == "Username")?.Value,
                    Role = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value,
                    PhotoId = claims?.FirstOrDefault(c => c.Type == "PhotoId")?.Value,
                    PhotoPath = claims.FirstOrDefault(c => c.Type == "PhotoPath")?.Value

                };
            });

            return services;
        }
    }
}

