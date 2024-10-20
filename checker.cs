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
    }
}
