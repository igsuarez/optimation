using MediatR;
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

        public ExtractDataCommandHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public Task<ExtractDataResult> Handle(ExtractDataCommand message, CancellationToken cancellationToken)
        {



            return Task.FromResult(ExtractDataResult.Create("", 1, 1));
        }
    }


    public class ExtractDataResult
    {
        public string ExtractedData { get; set; }
        public decimal Total { get; set; }
        public decimal GST { get; set; }

        public static ExtractDataResult Create(string extractedData, decimal total, decimal gst)
        {
            return new ExtractDataResult()
            {
                ExtractedData = extractedData,
                Total = total,
                GST = gst
            };
        }

    }


}
