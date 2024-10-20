using Xunit;
using Moq;

namespace paradigm_shift_csharp.Tests
{
    public class CheckerTests
    {
        private readonly Mock<IMessageLogger> _loggerMock;
        private readonly ParameterChecker _parameterChecker;
        private readonly Checker _batteryChecker;

        public CheckerTests()
        {
            _loggerMock = new Mock<IMessageLogger>();
            _parameterChecker = new ParameterChecker(_loggerMock.Object);
            _batteryChecker = new Checker(_parameterChecker);
        }

        [Fact]
        public void BatteryIsOk_InRange_ReturnsTrue()
        {
            // Arrange
            float temperature = 25f;
            float soc = 70f;
            float chargeRate = 0.7f;

            // Act
            var result = _batteryChecker.BatteryIsOk(temperature, 5, soc, 5, chargeRate, 5);

            // Assert
            Assert.True(result);
            _loggerMock.Verify(m => m.LogError(It.IsAny<string>()), Times.Never);
            _loggerMock.Verify(m => m.LogWarning(It.IsAny<string>()), Times.Never);
        }

        [Theory]
        [InlineData(50f, 85f, 0.0f)] // All parameters out of range
        [InlineData(50f, 70f, 0.0f)] // Temperature out of range, others ok
        [InlineData(25f, 85f, 0.0f)] // SOC out of range, others ok
        [InlineData(25f, 70f, 1.0f)] // Charge Rate out of range, others ok
        public void BatteryIsOk_OutOfRange_ReturnsFalse(float temperature, float soc, float chargeRate)
        {
            // Act
            var result = _batteryChecker.BatteryIsOk(temperature, 5, soc, 5, chargeRate, 5);

            // Assert
            Assert.False(result);
            _loggerMock.Verify(m => m.LogError(It.IsAny<string>()), Times.Never); // No errors should be logged
            _loggerMock.Verify(m => m.LogWarning(It.IsAny<string>()), Times.Never); // No warnings should be logged
        }

        [Theory]
        [InlineData(44f, 79f, 0.75f)] // Nearing max values
        [InlineData(1f, 21f, 0.01f)]  // Nearing min values
        public void BatteryIsOk_NearingThreshold_ReturnsTrueAndLogsWarning(float temperature, float soc, float chargeRate)
        {
            // Act
            var result = _batteryChecker.BatteryIsOk(temperature, 5, soc, 5, chargeRate, 5);

            // Assert
            Assert.True(result);
            _loggerMock.Verify(m => m.LogWarning(It.IsAny<string>()), Times.Exactly(3)); // Check for warnings
            _loggerMock.Verify(m => m.LogError(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void BatteryIsOk_ApproachingThreshold_ReturnsTrueAndLogsWarning()
        {
            // Arrange
            float temperature = 0f; // Approaching min
            float soc = 19f; // Approaching min
            float chargeRate = 0.0f; // At minimum

            // Act
            var result = _batteryChecker.BatteryIsOk(temperature, 5, soc, 5, chargeRate, 5);

            // Assert
            Assert.True(result);
            _loggerMock.Verify(m => m.LogWarning(It.Is<string>(s => s.Contains("is below threshold!"))), Times.Exactly(2)); // Check for warnings
            _loggerMock.Verify(m => m.LogError(It.IsAny<string>()), Times.Never);
        }
    }
}
