using Application.Modules.ContactManagement.People.Commands.AddAttachment;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Base.Heleprs;

namespace WebAPI.Controllers.ContactManagement.People
{
    [Route($"{BaseRoutingHelper.ContactManagement}/people/Attachment")]
    [Tags("people")]
    public class AttachmentController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> PostAsync(IFormFile file, int personID)
        {
            var attachmentId = await Mediator.Send(new AddAttachmentToPersonCommand { File = file, PersonID = personID });
            return Ok(attachmentId);
        }
    }
}
