using System;
namespace Caly.Common
{
    public interface IAddressParser
    {
        void Parse(IAdresaFaraLocalitate address, string field, IMatcher matcher, IMatchProcessor matchProcessor);
    }
}
