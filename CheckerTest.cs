using Xunit;
using Moq;

namespace paradigm_shift_csharp.Tests
{
    public class CheckerTests
    {
        [Fact]
        public void BatteryIsOk_TemperatureOutOfRange_LogsError()
        {
            // Arrange
            var mockLogger = new Mock<IMessageLogger>();
            var parameterChecker = new ParameterChecker(mockLogger.Object);
            var checker = new Checker(parameterChecker);

            // Act
            bool result = checker.BatteryIsOk(50, 5, 70, 5, 0.7f, 5); // Temperature is out of range

            // Assert
            mockLogger.Verify(logger => logger.LogError(It.Is<string>(s => s.Contains("Temperature is out of range!"))), Times.Once());
            Assert.False(result); // Since temperature is out of range, BatteryIsOk should return false
        }

        [Fact]
        public void BatteryIsOk_TemperatureReachingThreshold_LogsWarning()
        {
            // Arrange
            var mockLogger = new Mock<IMessageLogger>();
            var parameterChecker = new ParameterChecker(mockLogger.Object);
            var checker = new Checker(parameterChecker);

            // Act
            bool result = checker.BatteryIsOk(44, 5, 70, 5, 0.7f, 5); // Temperature close to the max (45)

            // Assert
            mockLogger.Verify(logger => logger.LogWarning(It.Is<string>(s => s.Contains("Temperature is reaching threshold"))), Times.Once());
            Assert.True(result); // Since temperature is within range, BatteryIsOk should return true
        }

        [Fact]
        public void BatteryIsOk_AllParametersOk_ReturnsTrue()
        {
            // Arrange
            var mockLogger = new Mock<IMessageLogger>();
            var parameterChecker = new ParameterChecker(mockLogger.Object);
            var checker = new Checker(parameterChecker);

            // Act
            bool result = checker.BatteryIsOk(40, 5, 75, 5, 0.7f, 5); // All parameters are within range

            // Assert
            Assert.True(result); // Since all parameters are in range, BatteryIsOk should return true
        }
    }
}
