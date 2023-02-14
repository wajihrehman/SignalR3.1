using Microsoft.AspNetCore.Identity;

namespace Final.BusinessObjects
{
    public class ApplicationUser:IdentityUser
    {
        public string? Fname { get; set; }
        public string? Lname { get; set; }
        public string? role { get; set; }
        public string? Organization { get; set; }
        public int? No_Of_HS_Teams { get; set; }
        public int? No_Of_YT_Teams { get; set; }
        public int? No_of_Softball_Teams { get; set; }
        public string? city { get; set; }
        public string? state { get; set; }

        public string? securityCode { get; set; }

    }
}
