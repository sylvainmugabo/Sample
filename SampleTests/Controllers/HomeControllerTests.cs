using Microsoft.AspNetCore.Mvc;
using Sample.Controllers;
using Sample.Models;
using Xunit;

namespace SampleTests.Controllers
{
    public class HomeControllerTests
    {
        
        [Fact]
        public  void Index()
        {
            var controller = new HomeController();

            var result = controller.Index();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void RsvpFormGet()
        {
            var controller = new HomeController();

            var result = controller.RsvpForm();

            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        /// <summary>
        /// Test post message
        /// </summary>
        /// <param name="isInvalidState">Modestate validation</param>
        /// <param name="expectedViewName">expected view name</param>
        [Theory]
        [InlineData(true, null)]
        [InlineData(false, "Thanks")]
        public void RsvpFormPostState(bool isInvalidState, string expectedViewName)
        {
            var controller = new HomeController();
            var guestResponse = new GuestResponse
            {
                Email = "test@email.com",
                Name = "test",
                Phone = "2401111111",
                WillAttend = true
            };
            
            if (isInvalidState) {
                controller.ModelState.AddModelError("Invalid", "Required");
            };

            var result = controller.RsvpForm(guestResponse);

            Assert.NotNull(result);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(expectedViewName, viewResult.ViewName);
        }
    }
}
