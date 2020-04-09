using AutoMapper;
using System;
using System.Collections.Generic;
using AwesomePotato.DTOs;
using AwesomePotato.Models;
using AwesomePotato.Services;
using Moq;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace AwesomePotatoTests
{
    public class Fakes
    {
        private Dictionary<Type, string> DataFileNames { get; } =
            new Dictionary<Type, string>();
        private string FileName<T>() { return DataFileNames[typeof(T)]; }

        public IMapper Mapper { get; }

        public Fakes()
        {
            DataFileNames.Add(typeof(ErrorLogData), $"C:\\Users\\devso\\ProjetoFinal\\AwesomePotatoTests\\TestData{Path.DirectorySeparatorChar}errorLogData.json");

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ErrorLogData, ErrorLogDataDTO>().ReverseMap();
            });

            this.Mapper = configuration.CreateMapper();
        }

        public List<T> Get<T>()
        {
            string content = File.ReadAllText(FileName<T>());
            return JsonConvert.DeserializeObject<List<T>>(content);
        }

        public Mock<IErrorLogDataService> FakeErrorLogDataService()
        {
            var service = new Mock<IErrorLogDataService>();

            service.Setup(x => x.FindById(It.IsAny<int>())).
                Returns((int id) => Get<ErrorLogData>().FirstOrDefault(x => x.Id == id));

            service.Setup(x => x.Save(It.IsAny<ErrorLogData>())).
                Returns((ErrorLogData logData) => {
                    if (logData.Id == 0)
                        logData.Id = 999;
                    return logData;
                });

            return service;
        }

    }
}
