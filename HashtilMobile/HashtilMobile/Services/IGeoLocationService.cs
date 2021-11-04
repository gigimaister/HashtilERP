using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HashtilMobile.Services
{
    public interface IGeoLocationService
    {
        Task<Dictionary<string, double>> GetCurrentLocation();
    }
}
