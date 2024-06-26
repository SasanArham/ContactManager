﻿using Application.Modules.ContactManagement.People.Commands.AddAttachment;
using Application.Modules.ContactManagement.People.Queries.GetPersonAttachmentDownloadUrl;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Base.Heleprs;

namespace WebAPI.Controllers.ContactManagement.People
{
    [Route($"{BaseRoutingHelper.ContactManagement}/people/{{personID}}/Attachment")]
    [Tags("people")]
    public class AttachmentController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> PostAsync(IFormFile file, int personID)
        {
            var attachmentId = await Mediator.Send(new AddAttachmentToPersonCommand { File = file, PersonID = personID });
            return Ok(attachmentId);
        }


        [HttpGet("{attachmentID}")]
        public async Task<IActionResult> GetAsync(int personID, string attachmentID)
        {
            var downloadUrl = await Mediator.Send(new GetPersonAttachmentDownloadUrlQuery
            {
                PersonID = personID,
                AttachmentID = attachmentID
            });
            return Ok(downloadUrl);
        }

    }
}
