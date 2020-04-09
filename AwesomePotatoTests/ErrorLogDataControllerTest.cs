using AwesomePotato.DTOs;
using AwesomePotato.Controllers;
using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;

namespace AwesomePotatoTests
{
    public class ErrorLogDataControllerTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetData_ExistingIdPassed_ReturnsOkResult(int id)
        {
            var fakes = new Fakes();
            var fakeService = fakes.FakeErrorLogDataService().Object;
            var expected = fakes.Mapper.Map<ErrorLogDataDTO>(fakeService.FindById(id));

            var controller = new ErrorLogDataController(fakeService, fakes.Mapper);
            var result = controller.GetData(id);

            Assert.IsType<OkObjectResult>(result.Result);
            var actual = (result.Result as OkObjectResult).Value as ErrorLogDataDTO;
            Assert.NotNull(actual);
            Assert.Equal(expected, actual, new ErrorLogDataDTOIdComparer());
        }

        [Theory]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        public void GetData_UnknownIdPassed_ReturnsNoContent(int id)
        {
            var fakes = new Fakes();
            var fakeService = fakes.FakeErrorLogDataService().Object;

            var controller = new ErrorLogDataController(fakeService, fakes.Mapper);
            var result = controller.GetData(id);

            Assert.IsType<NoContentResult>(result.Result);
        }

    }
}
