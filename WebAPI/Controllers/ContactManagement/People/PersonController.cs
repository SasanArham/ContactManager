using Application.Modules.ContactManagement.People.Commands.CreatePerson;
using Application.Modules.ContactManagement.People.Commands.DeletePerson;
using Application.Modules.ContactManagement.People.Commands.EditPerson;
using Application.Modules.ContactManagement.People.Queries.GetPeople;
using Application.Modules.ContactManagement.People.Queries.GetPersonByID;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Base.Heleprs;

namespace WebAPI.Controllers.ContactManagement.People
{
    [Route($"{BaseRoutingHelper.ContactManagement}/people")]
    [Tags("people")]
    public class PersonController : BaseController
    {
        /// <summary>
        /// Create new person
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostAsync(CreatePersonCommand command)
        {
            int createdID = await Mediator.Send(command);
            return Ok(createdID);
        }


        /// <summary>
        /// Edit person
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> PutAsync(EditPersonCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }


        /// <summary>
        /// Delete person
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int ID)
        {
            await Mediator.Send(new DeletePersonCommand { ID = ID });
            return Ok();
        }


        /// <summary>
        /// Return person
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet("{ID}}")]
        [ProducesResponseType(typeof(GetPersonByIDResponse), 200)]
        public async Task<IActionResult> GetByIDAsync(int ID)
        {
            var person = await Mediator.Send(new GetPersonByIDQuery { ID = ID });
            return Ok(person);
        }


        /// <summary>
        /// Return all people
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<GetPeopleResponse>), 200)]
        public async Task<IActionResult> GetAsync()
        {
            var people = await Mediator.Send(new GetPeopleQuery());
            return Ok(people);
        }
    }
}
