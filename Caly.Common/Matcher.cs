using System;
using System.Collections.Generic;
using System.Linq;
namespace Caly.Common
{

    public class Matcher : Tree<Classification, string>
    {
        public List<Classification> Find(string find, params Classification[] classifications)
        {
            if (classifications == null)
            {
                throw new ArgumentNullException(nameof(classifications));
            }

            var result = new List<Classification>();


            return null;
        }

        public Matcher()
        {
            Root = new TreeNode<Classification, string>(new Classification() { Name = "root", Order = 0 });
        }

        public List<TreeNode<Classification, string>> Narrow(List<Classification> criteria)
        {
            var maxOrder = criteria.Max(x => x.Order);

            Func<TreeNode<Classification, string>, bool> passFurther = (x) =>
              {
                  if (x.Value.Order < maxOrder && criteria.Any(y => y == x))
                  {
                      return true;
                  }

                  return false;
              };

            Func<TreeNode<Classification, string>, bool> acquire = (x) =>
            {
                if (x.Value.Order == maxOrder && criteria.Any(y => y == x))
                {
                    return true;
                }

                return false;
            };

            return Root.Narrow(null, criteria, passFurther, acquire);
        }

    }

    public class Classification : IEquatable<Classification>
    {
        public string Name
        {
            get;
            set;
        }
        public int Order
        {
            get;
            set;
        }

        public bool Equals(Classification other)
        {
            return (Order == other.Order && Name == other.Name);
        }
    }
}
