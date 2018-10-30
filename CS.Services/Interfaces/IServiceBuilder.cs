using System;
namespace CS.Services.Interfaces
{
    public interface IServiceBuilder
    {
        object GetService(string serviceName, params string[] parameters);
    }
}
