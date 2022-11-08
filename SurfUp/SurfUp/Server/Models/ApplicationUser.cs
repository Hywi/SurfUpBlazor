using Microsoft.AspNetCore.Identity;
using SurfUp.Shared;

namespace SurfUp.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Board> Boards { get; set; } = new List<Board>();

        public List<Rent> Rents { get; set; } = new List<Rent>();

    }
}