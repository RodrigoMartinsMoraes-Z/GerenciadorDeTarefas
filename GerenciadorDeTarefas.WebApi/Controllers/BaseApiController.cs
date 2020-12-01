using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeTarefas.WebApi.Controllers
{
    [ApiController, Authorize]
    public class BaseApiController : ControllerBase
    {
        
    }
}
