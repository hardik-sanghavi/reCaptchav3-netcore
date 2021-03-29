using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace reCaptchav3.Repository.Interfaces
{
    public interface ICaptchaValidator
    {
        Task<bool> IsCaptchaPassedAsync(string token);
    }
}
