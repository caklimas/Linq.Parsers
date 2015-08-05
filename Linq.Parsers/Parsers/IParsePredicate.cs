using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers
{
    public interface IParsePredicate<TInput>
    {
        bool Execute(TInput input);
    }
}