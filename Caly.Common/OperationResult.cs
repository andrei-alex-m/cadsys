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
        public List<object> Observations
        {
            get;
            set;
        }

        public OperationResult()
        {
            Observations = new List<object>();
        }

    }
}
