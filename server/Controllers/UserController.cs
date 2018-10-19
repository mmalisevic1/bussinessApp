using server.CustomAttributes;
using server.data;
using server.services;
using server.services.DTOs;
using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace server.Controllers
{
    [RoutePrefix("api")]
    public class UserController : ApiController
    {
        UserService _userService;
        GlobomanticsContext _globomanticsContext;

        public UserController()
        {
            _globomanticsContext = new GlobomanticsContext();
            _userService = new UserService(_globomanticsContext);
        }

        [HttpGet]
        [Route("user/{id}")]
        public async Task<IHttpActionResult> GetUser(long id)
        {
            return Ok(await _userService.GetUser(id));
        }

        [HttpGet]
        [Route("user/email/{email}")]
        public async Task<IHttpActionResult> GetUser(string email)
        {
            return Ok(await _userService.GetUser(email));
        }

        [HttpPost]
        [Route("user")]
        [ValidateModel]
        public async Task<IHttpActionResult> PostUser([FromBody]UserDTO user)
        {
            return Created(Request.RequestUri, await _userService.CreateUser(user));
        }

        [HttpPatch]
        [Route("user")]
        [ValidateModel]
        public async Task<IHttpActionResult> PatchUser([FromBody]UserDTO user)
        {
            return Ok(await _userService.UpdateUser(user));
        }
    }
}
