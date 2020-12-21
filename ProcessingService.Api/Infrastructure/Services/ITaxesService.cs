using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessingService.Api.Infrastructure.Services
{
    public interface ITaxesService
    {
        /// <summary>
        /// Subtract GST from value which includes GST
        /// </summary>
        /// <param name="value">Value includes GST</param>
        /// <returns>Value without GST</returns>
        double SubtractGST(double @value);
    }
}
