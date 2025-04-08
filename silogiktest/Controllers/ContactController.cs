using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using silogiktest.Models;
using silogiktest.Services;

namespace silogiktest.Controllers
{
    [Route("api/Contact")]
    public class ContactController : Controller
    {
        [HttpPost("GetViewLabels")]
        public JsonResult GetViewLabels(string lang)
        {
            var result = new ContactServices().GetViewLabels(lang);
            return new JsonResult(result);
        }

        [HttpPost("GetViewRulesMessages")]
        public JsonResult GetViewRulesMessages(string lang)
        {
            var result = new ContactServices().GetViewRulesMessages(lang);
            return new JsonResult(result);
        }

        [HttpPost("SendContactForm")]
        public Task<FormResponse> SendContactForm([FromForm] ContactFormInput form)
        {
            var result = new ContactServices().SendContactForm(form);
            return result;
        }

        
    }
}
