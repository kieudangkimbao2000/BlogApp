using TypeGen.Core.TypeAnnotations;

namespace BlogApp.DTOs
{
    [ExportTsInterface]
    public class LoginDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}