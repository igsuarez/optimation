using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProcessingService.Api.Application.Commands;
using ProcessingService.Api.Controllers;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ProcessingService.UnitTests
{
    public class XmlEngineApiTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<ILogger<XmlEngineController>> _loggerMock;

        public XmlEngineApiTest()
        {
            _mediatorMock = new Mock<IMediator>();
            _loggerMock = new Mock<ILogger<XmlEngineController>>();
        }


        [Fact]
        public async Task Extracts_data_success()
        {
            //Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<ExtractDataCommand>(), default(CancellationToken)))
                .Returns(Task.FromResult(new ExtractDataResult()));

            //Act
            var xmlEngineController = new XmlEngineController(_mediatorMock.Object, _loggerMock.Object);
            var actionResult = await xmlEngineController.ExtractsDataAsync(new ExtractDataCommand());

            //Assert            
            Assert.Equal((actionResult.Result as OkObjectResult).StatusCode, (int)System.Net.HttpStatusCode.OK);

        }

        [Fact]
        public async Task Extracts_data_fail()
        {
            //Arrange
            _mediatorMock.Setup(x => x.Send(It.IsAny<ExtractDataCommand>(), default(CancellationToken)))
                .Throws(new InvalidInputException("", null))
                ;

            //Act
            var xmlEngineController = new XmlEngineController(_mediatorMock.Object, _loggerMock.Object);
            var actionResult = await xmlEngineController.ExtractsDataAsync(new ExtractDataCommand());

            //Assert    
            Assert.Equal((actionResult.Result as BadRequestObjectResult).StatusCode, (int)System.Net.HttpStatusCode.BadRequest);

        }
    }
}
