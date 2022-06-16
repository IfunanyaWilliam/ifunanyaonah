using System.ComponentModel.DataAnnotations;

namespace ifunanyaonah.Models
{
    public class EmailViewModel
    {
        public string Name { get; set; }
        public string Subject { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please enter your message")]
        public string Message { get; set; }
    }
}
