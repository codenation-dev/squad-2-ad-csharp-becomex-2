using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomePotato.Models;

namespace AwesomePotato.Services
{
    public interface IErrorLogDataService
    {
        ErrorLogData FindById(int id);
        IList<ErrorLogData> FindByAplicationName(string name);
        IList<ErrorLogData> FindByToken(Guid token);
        IList<ErrorLogData> FindByLevel(int level);
        IList<ErrorLogData> FindByAplicationNameAndLevel(string name, int level);
        IList<ErrorLogData> FindByAplicationNameAndToken(string name, Guid token);
        IList<ErrorLogData> FindByTokenAndLevel(Guid token, int level);
        IList<ErrorLogData> FindByAplicationNameAndTokenAndLevel(string name, Guid token, int level);
        ErrorLogData Save(ErrorLogData errorLogData);
    }
}
