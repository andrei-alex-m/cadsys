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

        /// <summary>
        /// Returns the classification of a string, departing from a list of classifications where its match is met
        /// Narrow the tree with the supplied classification (Adresa) and then return the supplied classifications completed with the one found down the tree found for its match: Adresa, TipStrada, Bulevardul
        /// A func should be supplied for the matching (input: string to match, leafe, output bool)
        /// </summary>
        /// <returns>The match.</returns>
        public List<Classification> Match(List<Classification> narrowTo, string find, IMatchProcessor  with)
        {
            var result = new List<Classification>();

            var narrowed = Narrow(narrowTo);

            narrowed.ForEach(x => {
                var leaves = new List<TreeNode<Classification, string>>();
                leaves = x.GetLeaves(leaves);
                leaves.ForEach(y =>{
                    if (with.Process(find, y.Leaf))
                    {
                        result.AddRange(y.BuildChainUp(null, x.Id).OrderBy(z=>z.Order));
                    }
                });
            });

            return result;
        }

        public List<TreeNode<Classification, string>> Narrow(List<Classification> criteria)
        {
            var maxOrder = criteria.Max(x => x.Order);

            Func<TreeNode<Classification, string>, bool> passFurther = (x) =>
              {
                if (x.Value.Order < maxOrder && criteria.Any(y => y.Equals(x.Value)))
                  {
                      return true;
                  }

                  return false;
              };

            Func<TreeNode<Classification, string>, bool> acquire = (x) =>
            {
                if (x.Value.Order == maxOrder && criteria.Any(y => y.Equals(x.Value)))
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
        public Classification()
        {

        }

        public Classification(int order, string name)
        {
            this.Order = order;
            this.Name = name;
        }

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

    public interface IMatchProcessor
    {
        bool Process(params object[] prm);

    }
}
