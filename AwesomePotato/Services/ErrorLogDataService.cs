using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomePotato.Models;
using AwesomePotato.Services;
using Microsoft.EntityFrameworkCore;

namespace AwesomePotato.Services
{
    public class ErrorLogDataService : IErrorLogData
    {
        private readonly AwesomePotatoContext context;

        public ErrorLogDataService(AwesomePotatoContext context)
        {
            this.context = context;
        }
        public IList<ErrorLogData> FindByAplicationName(string name)
        {
            return context.ErrorLogData.Where(e => e.Aplication == name).ToList();
        }

        public IList<ErrorLogData> FindByAplicationNameAndLevel(string name, int level)
        {
            return context.ErrorLogData.Where(e => e.Aplication == name && e.Level == level).ToList();
        }

        public IList<ErrorLogData> FindByAplicationNameAndToken(string name, Guid token)
        {
            return context.ErrorLogData.Where(e => e.Aplication == name && e.Token == token).ToList();
        }

        public IList<ErrorLogData> FindByAplicationNameAndTokenAndLevel(string name, Guid token, int level)
        {
            return context.ErrorLogData.Where(e => e.Aplication == name && e.Token == token && e.Level == level).ToList();
        }

        public ErrorLogData FindById(int id)
        {
            return context.ErrorLogData.Find(id);
        }

        public IList<ErrorLogData> FindByLevel(int level)
        {
            return context.ErrorLogData.Where(e => e.Level == level).ToList();
        }

        public IList<ErrorLogData> FindByToken(Guid token)
        {
            return context.ErrorLogData.Where(e => e.Token == token).ToList();
        }

        public IList<ErrorLogData> FindByTokenAndLevel(Guid token, int level)
        {
            return context.ErrorLogData.Where(e => e.Token == token && e.Level == level).ToList();
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
