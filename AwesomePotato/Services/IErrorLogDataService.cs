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
        IList<ErrorLogData> FilterByApplicationLevelDate(string applicationName, int? level, string startDate, string endDate);
        IList<ErrorLogData> GetAll();
        IList<ErrorLogData> FilterByApplication(IList<ErrorLogData> logDatas, string application);
        IList<ErrorLogData> FilterByLevel(IList<ErrorLogData> logDatas, int? level);
        IList<ErrorLogData> FilterByDate(IList<ErrorLogData> logDatas, string startDate, string endDate);
        ErrorLogData Save(ErrorLogData errorLogData);
    }
}
