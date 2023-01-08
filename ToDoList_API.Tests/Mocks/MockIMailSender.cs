using EmailService;
using Moq;

namespace ToDoList_API.Tests.Mocks
{
    internal class MockIMailSender
    {
        public static Mock<IEmailSender> GetMock()
        {
            var mock = new Mock<IEmailSender>();

            mock.Setup(x => x.SendEmailAsync(It.IsAny<Message>()))
                .Callback(() => { return; });

            return mock;
        }
    }
}
