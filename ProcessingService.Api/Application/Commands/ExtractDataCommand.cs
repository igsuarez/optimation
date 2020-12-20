using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessingService.Api.Application.Commands
{
    public class ExtractDataCommand : IRequest<ExtractDataResult>
    {
        public string RawData { get; private set; }
    }
}
