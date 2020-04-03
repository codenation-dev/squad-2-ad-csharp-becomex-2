using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomePotato.Models;

namespace AwesomePotato.Services
{
    public interface IErrorLogData
    {
        ErrorLogData FindById(int id);
        IList<ErrorLogData> FindByAplicationName(string name);
        IList<ErrorLogData> FindByToken(Guid token);
        ErrorLogData Save(ErrorLogData errorLogData);
    }
}
