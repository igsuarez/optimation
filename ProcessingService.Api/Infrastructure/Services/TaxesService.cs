using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessingService.Api.Infrastructure.Services
{
    public class TaxesService : ITaxesService
    {
        const double GST_DIVISOR = 1.15;

        public double SubtractGST(double @value)
        {
            return value / GST_DIVISOR;
        }
    }
}
