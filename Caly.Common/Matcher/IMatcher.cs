using System.Collections.Generic;

namespace Caly.Common
{
    public interface IMatcher
    {
        List<Classification> Match(List<Classification> narrowTo, string find, IMatchProcessor with);
    }
}