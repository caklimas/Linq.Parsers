using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq.Parsers
{
    public interface IParseCommand<TInput, TValue>
    {
        ParseResult<TInput, TValue> Execute(TInput input);
    }
}