using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace ProcessingService.Api.Application.Commands
{
    public class ExtractDataCommand : IRequest<ExtractDataResult>
    {
        [DataMember]
        public string RawData { get; set; }
    }
}
