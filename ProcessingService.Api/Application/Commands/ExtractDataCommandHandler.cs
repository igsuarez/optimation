using MediatR;
using ProcessingService.Api.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessingService.Api.Application.Commands
{
    public class ExtractDataCommandHandler
        : IRequestHandler<ExtractDataCommand, ExtractDataResult>
    {
        private readonly IMediator _mediator;
        private readonly IXmlService _xmlService;
        private readonly ITaxesService _taxesService;
        private static string[] _mandatoryTags = new string[] { "total", "cost_centre" };
        private const string TOTAL_TAG = "total";

        public ExtractDataCommandHandler(IMediator mediator, IXmlService xmlService, ITaxesService taxesService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _xmlService = xmlService ?? throw new ArgumentNullException(nameof(xmlService));
            _taxesService = taxesService ?? throw new ArgumentNullException(nameof(taxesService));
        }

        public Task<ExtractDataResult> Handle(ExtractDataCommand message, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var tag in _mandatoryTags)
                {
                    if (!_xmlService.XmlTagExist(message.RawData, tag))
                    {
                        throw new InvalidInputException($"Invalid input, missing tag: {tag}", null);
                    }
                }

                string xmlData = _xmlService.ExtractXmlNodes(message.RawData);
                if (!_xmlService.IsXmlWellFormed($"<root>{xmlData}</root>"))
                {
                    throw new InvalidInputException($"Invalid input, xml wrong format: {xmlData}", null);
                }

                string totaltext = _xmlService.GetXmlNodeValue($"<root>{xmlData}</root>", TOTAL_TAG);
                double total;
                if (!double.TryParse(totaltext, out total))
                {
                    throw new InvalidInputException($"Invalid input, invalid value on total node: {xmlData}", null);
                }

                double totalWithoutGST = _taxesService.SubtractGST(total);
                return Task.FromResult(ExtractDataResult.Create(xmlData, totalWithoutGST, total - totalWithoutGST));

            }
            catch (InvalidInputException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidInputException($"Error during processing data. Input: {message.RawData}", ex); ;
            }
        }
    }

    public class ExtractDataResult
    {
        public string ExtractedData { get; set; }
        public double Total { get; set; }
        public double GST { get; set; }

        public static ExtractDataResult Create(string extractedData, double total, double gst)
        {
            return new ExtractDataResult()
            {
                ExtractedData = extractedData,
                Total = total,
                GST = gst
            };
        }

    }

    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message, Exception exception)
            : base(message, exception)
        { }
    }


}
