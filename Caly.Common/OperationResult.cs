using System;
using System.Collections.Generic;

namespace Caly.Common
{
    public class OperationResult
    {
        public bool Result
        {
            get;
            set;
        }
        public IEnumerable<string> Observations
        {
            get;
            set;
        }
    }
}
