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
            bool isTemperatureOk = _parameterChecker.ParameterInRange(max: 45f, value: temperature, thresholdPercent: tempThresholdPercent, errorMessage: "Temperature");
            bool isSocOk = _parameterChecker.ParameterInRange(max: 80f, value: soc, thresholdPercent: socThresholdPercent, errorMessage: "State of Charge");
            bool isChargeRateOk = _parameterChecker.ParameterInRange(max: 0.8f, value: chargeRate, thresholdPercent: chargeRateThresholdPercent, errorMessage: "Charge Rate");

            return isTemperatureOk && isSocOk && isChargeRateOk;
        }

        static void ExpectTrue(bool expression)
        {
            if (!expression)
            {
                Console.WriteLine("Expected true, but got false");
                Environment.Exit(1);
            }
        }
 
        static void ExpectFalse(bool expression)
        {
            if (expression)
            {
                Console.WriteLine("Expected false, but got true");
                Environment.Exit(1);
            }
        }

        static int Main()
        {
            // Create an instance of ParameterChecker
            ParameterChecker parameterChecker = new ParameterChecker(); // Assuming this has a parameterless constructor
            Checker checker = new Checker(parameterChecker); // Create an instance of Checker

            // Test case where everything is in the valid range
            ExpectTrue(checker.BatteryIsOk(25, 5, 70, 5, 0.7f, 5));
 
            // Test case where temperature and SOC are out of the valid range
            ExpectFalse(checker.BatteryIsOk(50, 5, 85, 5, 0.0f, 5));
 
            Console.WriteLine("All tests passed");
            return 0;
        }
    }
}
