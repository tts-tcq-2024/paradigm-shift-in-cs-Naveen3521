using System;

namespace paradigm_shift_csharp
{
    public class ParameterChecker
    {
        private readonly IMessageLogger _logger;

        public ParameterChecker(IMessageLogger logger)
        {
            _logger = logger;
        }

        public bool ParameterInRange(float max, float value, float thresholdPercent, string errorMessage, float? min = null)
        {
            var thresholdNumber = CalculateThresholdNumber(thresholdPercent, max);
            bool isInRange = CheckInRange(max, value, errorMessage, min);
            if (!isInRange)
                _logger.LogError($"{errorMessage} is out of range!");
            else if(IsOutOfThresholdRange(value,max,thresholdNumber,min))
                _logger.LogWarning($"{errorMessage} is reaching threshold");
            return isInRange;
        }

        private bool CheckInRange(float max, float value, string errorMessage, float? min = null)
        {
            if (min.HasValue)
                return value >= min && value <= max;
            else
                return value <= max;
        }

        private float CalculateThresholdNumber(float thresholdPercent, float max)
        {
            return (max * (thresholdPercent / 100));
        }

        public bool IsOutOfThresholdRange(float value, float max, float thresholdNumber, float? min = null)
        {
            return (min.HasValue && value < min + thresholdNumber) || value > max - thresholdNumber;
        }
    }
}
