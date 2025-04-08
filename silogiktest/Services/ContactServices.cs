using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using silogiktest.Models;
using silogiktest.Queries;
using System.Collections;
using System.Data;

namespace silogiktest.Services
{
    public class ContactServices : Controller
    {
        public ArrayList GetViewLabels(string lang)
        {
            var resp = new ContactQueries().GetViewLabels(lang);
            return resp;
        }

        public ArrayList GetViewRulesMessages(string lang)
        {
            var resp = new ContactQueries().GetViewRulesMessages(lang);
            return resp;
        }

        public async Task<FormResponse> SendContactForm(ContactFormInput form)
        {
            try
            {

                byte[]? bytes = null;
                if (form.ContactAttachFile != null)
                {
                    using var memoryStream = new MemoryStream();
                    await form.ContactAttachFile.CopyToAsync(memoryStream);
                    bytes = memoryStream.ToArray();
                }

                new ContactQueries().SendContactForm(form, bytes);

                return new FormResponse
                {
                    Success = true,
                    Message = new ContactQueries().ObtenerResponseMessage(1, form.Lang)
                };

            }
            catch (Exception ex)
            {
                return new FormResponse
                {
                    Success = false,
                    Message = new ContactQueries().ObtenerResponseMessage(2, form.Lang)
                };
            }
        }
    }
}
