using Microsoft.AspNetCore.Mvc;
using SocialAppApi.Data;
using SocialAppApi.Entities;
using System.Security.Cryptography;
using System.Text;

namespace SocialAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController: ControllerBase
    {
    }
}
