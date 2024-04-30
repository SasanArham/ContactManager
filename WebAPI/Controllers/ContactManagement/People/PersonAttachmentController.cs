using Application.Modules.ContactManagement.People.Commands.AddAttachment;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.ContactManagement.People
{
    public class PersonAttachmentController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> PostAsync(IFormFile file, int personID)
        {
            var attachmentId = await Mediator.Send(new AddAttachmentToPersonCommand { File = file, PersonID = personID });
            return Ok(attachmentId);
        }
    }
}
