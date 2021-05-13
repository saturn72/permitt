using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Permitt.Services;

namespace Permitt.SampleApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalcController : ControllerBase
    {
        private const string SUBJECT_ID = "some-user-id";
        private readonly IPermissionService _permitt;
        private readonly Repository _repository;
        private readonly ILogger<CalcController> _logger;

        public CalcController(
            IPermissionService permitt,
            Repository repository,
            ILogger<CalcController> logger)
        {
            _permitt = permitt;
            _repository = repository;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Add(int a, int b)
        {
            UserPermittedForActionRequest request = new UserPermittedForActionRequest
            {
                EntityName = nameof(AddExpression).ToLower(),
                PermissionType = "create",
                UserId = SUBJECT_ID,
            };
            UserPermittedForActionResponse response = await _permitt.UserIsPermittedForAction(request);

            if (!response.Permitted) //This example always insert to this code block
            {
                return StatusCode((int)HttpStatusCode.Forbidden);
            }
            var ae = new AddExpression
            {
                A = a,
                B = b
            };
            _repository.Create(ae);
            return Ok(ae);
        }
        [HttpGet]
        public async Task<IActionResult> Get(string id)
        {
            //validate user is permitted for the sent id
            UserPermittedForEntityRequest request = new UserPermittedForEntityRequest
            {
                EntityId = id,
                EntityName = nameof(AddExpression).ToLower(),
                PermissionType = "read",
                UserId = SUBJECT_ID,
            };

            UserPermittedForEntityResponse response = await _permitt.UserIsPermittedForEntityAsync(request);

            if (!response.Permitted) //This example always insert to this code block
            {
                return StatusCode((int)HttpStatusCode.Forbidden);
            }
            var data = _repository.GetbyId(id);
            return Ok(data);
        }
    }
}
