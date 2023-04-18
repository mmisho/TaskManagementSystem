using System.ComponentModel.DataAnnotations;

namespace Application.Shared.Options
{
    public sealed class JwtOptions
    {
        /// <summary>
        /// ConfigSection for appsettings
        /// </summary>
        public const string ConfigSection = "JwtConfig";

        /// <summary>
        /// Gets or sets ValidIssuer
        /// </summary>
        [Required]
        public string ValidIssuer { get; set; } = default!;

        /// <summary>
        /// Gets or sets ValidAudience
        /// </summary>
        [Required]
        public string ValidAudience { get; set; } = default!;

        /// <summary>
        /// Gets or sets ValidAudiences
        /// </summary>
        public string[] ValidAudiences { get; set; } = Array.Empty<string>();

        /// <summary>
        /// Gets or sets SecretKey
        /// </summary>
        [Required]
        public string SecretKey { get; set; } = default!;

        /// <summary>
        /// Gets or sets ExpiresInMinutes
        /// </summary>
        public int ExpiresInMinutes { get; set; } = 60;
    }
}
