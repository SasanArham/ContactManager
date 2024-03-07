using Application.Modules;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class TestController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> PostAsync(TestCommand command)
        {
            var res = await Mediator.Send(command);
            return Ok(res);
        }
    }
}
