using System;
namespace Caly.Common
{
    public interface IMatchProcessor
    {
        bool Process(params object[] prm);

    }
}
