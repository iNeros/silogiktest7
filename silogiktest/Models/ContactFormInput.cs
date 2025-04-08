namespace silogiktest.Models
{
    public class ContactFormInput
    {
        public string ContactEmail { get; set; }

        public string ContactName { get; set; }

        public string ContactLastNames { get; set; }

        public string ContactComments { get; set; }

        public IFormFile? ContactAttachFile { get; set; }

        public string Lang { get; set; }

    }
}
