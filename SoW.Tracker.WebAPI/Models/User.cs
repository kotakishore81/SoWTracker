using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace SoW.Tracker.WebAPI.Models
{
    [ExcludeFromCodeCoverage]
    public class User
    {
        string _lanId = string.Empty;
        public string LanId { get => !string.IsNullOrEmpty(_lanId) ? _lanId.ToUpper() : _lanId; set => _lanId = value; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SessionId { get; set; }
        public string AccessToken { get; set; }
        public DateTime? AccessTokenExpiresAt { get; set; }
        public string Role { get; set; }
        public bool status { get; set; }
        public string AuthorizedComp { get; set; }
    }
}
