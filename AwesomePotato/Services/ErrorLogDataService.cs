using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomePotato.Models;
using AwesomePotato.Services;
using Microsoft.EntityFrameworkCore;

namespace AwesomePotato.Services
{
    public class ErrorLogDataService : IErrorLogDataService
    {
        private readonly AwesomePotatoContext context;

        public ErrorLogDataService(AwesomePotatoContext context)
        {
            this.context = context;
        }

        public IList<ErrorLogData> FilterByApplication(IList<ErrorLogData> logDatas, string application)
        {
            if (!string.IsNullOrEmpty(application))
                logDatas = logDatas.Where(ld => ld.Application == application).ToList();

            return logDatas;
        }

        public IList<ErrorLogData> FilterByDate(IList<ErrorLogData> logDatas, string startDate, string endDate)
        {
            if (!string.IsNullOrEmpty(startDate) && !string.IsNullOrEmpty(endDate)
                && DateTime.TryParse(startDate, out DateTime startDatetime) && DateTime.TryParse(endDate, out DateTime endDatetime))
                logDatas = logDatas.Where(ld => ld.TimeStamp > startDatetime && ld.TimeStamp < endDatetime).ToList();

            return logDatas;
        }

        public IList<ErrorLogData> FilterByLevel(IList<ErrorLogData> logDatas, int? level)
        {
            if (level.HasValue)
                logDatas = logDatas.Where(ld => ld.Level == level).ToList();

            return logDatas;
        }

        public IList<ErrorLogData> FilterByApplicationLevelDate(string applicationName, int? level, string startDate, string endDate)
        {
            IList<ErrorLogData> data = GetAll();

            data = FilterByApplication(data, applicationName);
            data = FilterByLevel(data, level);
            data = FilterByDate(data, startDate, endDate);

            return data;
        }

        public ErrorLogData FindById(int id)
        {
            return context.ErrorLogData.Find(id);
        }

        public IList<ErrorLogData> GetAll()
        {
            return context.ErrorLogData.ToList();
        }

        public ErrorLogData Save(ErrorLogData errorLogData)
        {
            var state = errorLogData.Id == 0 ? EntityState.Added : EntityState.Modified;
            context.Entry(errorLogData).State = state;
            context.SaveChanges();
            return errorLogData;
        }
    }
}
