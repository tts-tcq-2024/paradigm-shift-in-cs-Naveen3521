using System;
using Moq;

namespace paradigm_shift_csharp
{
    public static class CheckerTest
    {
        public static void RunTests(Checker checker)
        {
            // Create a mock logger for the tests
            var mockLogger = new Mock<IMessageLogger>();
            var parameterChecker = new ParameterChecker(mockLogger.Object);
            var batteryChecker = new Checker(parameterChecker);

            // Temperature Tests
            TestBatteryIsOk_TemperatureOutOfRange_LogsError(batteryChecker, mockLogger);
            TestBatteryIsOk_TemperatureReachingThreshold_LogsWarning(batteryChecker, mockLogger);

            // SOC Tests
            TestBatteryIsOk_SocOutOfRange_LogsError(batteryChecker, mockLogger);
            TestBatteryIsOk_SocReachingThreshold_LogsWarning(batteryChecker, mockLogger);

            // Charge Rate Tests
            TestBatteryIsOk_ChargeRateOutOfRange_LogsError(batteryChecker, mockLogger);
            TestBatteryIsOk_ChargeRateReachingThreshold_LogsWarning(batteryChecker, mockLogger);

            // Valid and Invalid Cases
            TestBatteryIsOk_ValidCase_ExpectTrue(batteryChecker);
            TestBatteryIsOk_InvalidCase_ExpectFalse(batteryChecker);

            Console.WriteLine("All tests passed.");
        }

        // Test Cases
        // Temperature Tests
        private static void TestBatteryIsOk_TemperatureOutOfRange_LogsError(Checker batteryChecker, Mock<IMessageLogger> mockLogger)
        {
            bool result = batteryChecker.BatteryIsOk(50, 5, 70, 5, 0.7f, 5); // Temperature out of range

            mockLogger.Verify(logger => logger.LogError(It.Is<string>(s => s.Contains("Temperature is out of range!"))), Times.Once());
            if (result) throw new Exception("Expected false, but got true");
        }

        private static void TestBatteryIsOk_TemperatureReachingThreshold_LogsWarning(Checker batteryChecker, Mock<IMessageLogger> mockLogger)
        {
            bool result = batteryChecker.BatteryIsOk(44, 5, 70, 5, 0.7f, 5); // Temperature nearing threshold

            mockLogger.Verify(logger => logger.LogWarning(It.Is<string>(s => s.Contains("Temperature is reaching threshold"))), Times.Once());
            if (!result) throw new Exception("Expected true, but got false");
        }

        // SOC Tests
        private static void TestBatteryIsOk_SocOutOfRange_LogsError(Checker batteryChecker, Mock<IMessageLogger> mockLogger)
        {
            bool result = batteryChecker.BatteryIsOk(25, 5, 90, 5, 0.7f, 5); // SOC out of range

            mockLogger.Verify(logger => logger.LogError(It.Is<string>(s => s.Contains("State of Charge is out of range!"))), Times.Once());
            if (result) throw new Exception("Expected false, but got true");
        }

        private static void TestBatteryIsOk_SocReachingThreshold_LogsWarning(Checker batteryChecker, Mock<IMessageLogger> mockLogger)
        {
            bool result = batteryChecker.BatteryIsOk(25, 5, 78, 5, 0.7f, 5); // SOC nearing threshold

            mockLogger.Verify(logger => logger.LogWarning(It.Is<string>(s => s.Contains("State of Charge is reaching threshold"))), Times.Once());
            if (!result) throw new Exception("Expected true, but got false");
        }

        // Charge Rate Tests
        private static void TestBatteryIsOk_ChargeRateOutOfRange_LogsError(Checker batteryChecker, Mock<IMessageLogger> mockLogger)
        {
            bool result = batteryChecker.BatteryIsOk(25, 5, 70, 5, 0.9f, 5); // Charge rate out of range

            mockLogger.Verify(logger => logger.LogError(It.Is<string>(s => s.Contains("Charge Rate is out of range!"))), Times.Once());
            if (result) throw new Exception("Expected false, but got true");
        }

        private static void TestBatteryIsOk_ChargeRateReachingThreshold_LogsWarning(Checker batteryChecker, Mock<IMessageLogger> mockLogger)
        {
            bool result = batteryChecker.BatteryIsOk(25, 5, 70, 5, 0.78f, 5); // Charge rate nearing threshold

            mockLogger.Verify(logger => logger.LogWarning(It.Is<string>(s => s.Contains("Charge Rate is reaching threshold"))), Times.Once());
            if (!result) throw new Exception("Expected true, but got false");
        }

        // Valid and Invalid Cases
        private static void TestBatteryIsOk_ValidCase_ExpectTrue(Checker batteryChecker)
        {
            bool result = batteryChecker.BatteryIsOk(25, 5, 70, 5, 0.7f, 5); // All parameters valid

            if (!result) throw new Exception("Expected true, but got false");
        }

        private static void TestBatteryIsOk_InvalidCase_ExpectFalse(Checker batteryChecker)
        {
            bool result = batteryChecker.BatteryIsOk(50, 5, 85, 5, 0.0f, 5); // Invalid parameters

            if (result) throw new Exception("Expected false, but got true");
        }
    }
}
