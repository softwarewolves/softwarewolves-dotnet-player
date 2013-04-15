using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetPlayer
{
    class DecisionStrategy
    {
        public string vote(string[] candidates)
        {
            return candidates.First();
        }
    }
}
