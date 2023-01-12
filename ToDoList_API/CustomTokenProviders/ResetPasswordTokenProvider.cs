using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ToDoList_API.CustomTokenProviders
{
    public class ResetPasswordTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
    {
        public ResetPasswordTokenProvider(IDataProtectionProvider dataProtectionProvider, IOptions<ResetPasswordTokenProviderOptions> options, ILogger<DataProtectorTokenProvider<TUser>> logger) : base(dataProtectionProvider, options, logger)
        {
        }
    }

    public class ResetPasswordTokenProviderOptions : DataProtectionTokenProviderOptions
    {
    }
}
