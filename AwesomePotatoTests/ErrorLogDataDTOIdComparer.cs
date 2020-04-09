using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AwesomePotato.DTOs;
using System.Text;

namespace AwesomePotatoTests
{
    class ErrorLogDataDTOIdComparer : IEqualityComparer<ErrorLogDataDTO>
    {
        public bool Equals([AllowNull] ErrorLogDataDTO x, [AllowNull] ErrorLogDataDTO y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode([DisallowNull] ErrorLogDataDTO obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
