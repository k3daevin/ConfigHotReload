using ConfigHotReload.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ConfigHotReload.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private readonly IOptionsSnapshot<FeatureConfig> _options;

        public FeatureController(IOptionsSnapshot<FeatureConfig> options)
        {
            _options = options;
        }

        // GET: api/<FeatureController>
        [HttpGet]
        public FeatureConfig Get() => _options.Value;

    }
}
