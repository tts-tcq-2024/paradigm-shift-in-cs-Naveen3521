using System;
using System.Diagnostics;

namespace paradigm_shift_csharp
{
    class Checker
    {
        static bool batteryIsOk(float temperature, float tempThresholdPercent, float soc, float socThresholdPercent, float chargeRate, float chargeRateThresholdPercent)
        {
            bool isTemperatureOk = ParameterInRange(min: 0f, max: 45f, value: temperature, threshholdPercent: tempThresholdPercent, errorMessage: "Temperature");
            bool isSocOk = ParameterInRange(min: 20f, max: 80f, value: soc, threshholdPercent: socThresholdPercent, errorMessage: "State of Charge");
            bool isChargeRateOk = ParameterInRange(max: 0.8f, value: chargeRate, threshholdPercent: chargeRateThresholdPercent, errorMessage: "Charge Rate");

            return isTemperatureOk && isSocOk && isChargeRateOk;
        }

        static bool ParameterInRange(float max, float value, float threshholdPercent, string errorMessage, float? min = null)
        {
            var thresholdNumber = calculateThresholdNumber(threshholdPercent, max);
            bool isInRange = CheckInRange(max, value, thresholdNumber, errorMessage, min);
            DisplayWarningMessage(max, value, thresholdNumber, errorMessage, min);
            if (!isInRange)
                Console.WriteLine("{0} is out of range!", errorMessage);
            return isInRange;
        }

        // Function which dynamically checks if in range based on number of values
        static bool CheckInRange(float max, float value, float thresholdNumber, string errorMessage, float? min = null)
        {
            if (min.HasValue)
                return value >= min && value <= max;
            else
                return value <= max;
        }

        // Function which calculates threshold number given max value and percentage
        static float calculateThresholdNumber(float thresholdPercent, float max)
        {
            return (max * (thresholdPercent / 100));
        }

        
        // Function to display warnings based on range checks
         static void DisplayWarningMessage(float max, float value, float thresholdNumber, string errorMessage, float? min = null)
        {
            if (min.HasValue && value < min + thresholdNumber)
            {
                Console.WriteLine("{0} is below threshold!", errorMessage);
            }
            else if (value > max - thresholdNumber)
            {
                Console.WriteLine("{0} is above threshold!", errorMessage);
            }
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
            // Test case where everything is in the valid range
            ExpectTrue(batteryIsOk(25, 5, 70, 5, 0.7f, 5));

            // Test case where temperature and SOC are out of the valid range
            ExpectFalse(batteryIsOk(50, 5, 85, 5, 0.0f, 5));

            Console.WriteLine("All tests passed");
            return 0;
        }
    }
}
