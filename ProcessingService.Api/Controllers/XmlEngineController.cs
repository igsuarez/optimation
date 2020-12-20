using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProcessingService.Api.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessingService.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class XmlEngineController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<XmlEngineController> _logger;

        public XmlEngineController(
                IMediator mediator,
                ILogger<XmlEngineController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Route("extract")]
        [HttpPost]
        public async Task<ActionResult<ExtractDataResult>> ExtractsDataAsync([FromBody] ExtractDataCommand extractDataCommand)
        {
            _logger.LogInformation("----- Extract data command: {RawData}", extractDataCommand.RawData);

            return await _mediator.Send(extractDataCommand);
        }
    }
}
