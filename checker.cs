using System;

namespace paradigm_shift_csharp
{
    public class Checker
    {
        private readonly ParameterChecker _parameterChecker;

        public Checker(ParameterChecker parameterChecker)
        {
            _parameterChecker = parameterChecker;
        }

        public bool BatteryIsOk(float temperature, float tempThresholdPercent, float soc, float socThresholdPercent, float chargeRate, float chargeRateThresholdPercent)
        {
            bool isTemperatureOk = _parameterChecker.ParameterInRange(min:0f,max: 45f, value: temperature, thresholdPercent: tempThresholdPercent, errorMessage: "Temperature");
            bool isSocOk = _parameterChecker.ParameterInRange(min:20f,max: 80f, value: soc, thresholdPercent: socThresholdPercent, errorMessage: "State of Charge");
            bool isChargeRateOk = _parameterChecker.ParameterInRange(max: 0.8f, value: chargeRate, thresholdPercent: chargeRateThresholdPercent, errorMessage: "Charge Rate");

            return isTemperatureOk && isSocOk && isChargeRateOk;
        }
        
        static int Main()
        {
            IMessageLogger logger = new ConsoleLogger();
            ParameterChecker parameterChecker = new ParameterChecker(logger);
            Checker checker = new Checker(parameterChecker);

            // Run tests
            CheckerTest.RunTests(checker);

            Console.WriteLine("All tests completed.");
            return 0;
        }
    }
}
