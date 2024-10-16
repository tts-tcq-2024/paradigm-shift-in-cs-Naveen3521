using System;
using System.Diagnostics;
namespace paradigm_shift_csharp
{
class Checker
{
    static bool batteryIsOk(float temperature,float tempThresholdPercent, float soc,float socThresholdPercent, float chargeRate,float chargeRateThresholdPercent) 
    {
     bool isBatteryok = true;
     isBatteryok = ParameterInRange(min:0f,max:45f,value:temperature,threshholdPercent:tempThresholdPercent,errorMessage:"Temperature") && ParameterInRange(min:20f,max:80f,value:soc,threshholdPercent:socThresholdPercent,errorMessage:"State of Charge") && ParameterInRange(max:0.8f,value:chargeRate,threshholdPercent:chargeRateThresholdPercent,errorMessage:"Charge Rate");
     return isBatteryok;
    }

    
    static bool ParameterInRange(float max,float value,float threshholdPercent,string errorMessage,float ?min=null)
    {
        var thresholdNumber = calculateThresholdNumber(threshholdPercent,max);
        bool isInRange = CheckinRange(min,max,value,thresholdNumber,errorMessage);
        DisplayWarningMessage(min,max,value,thresholdNumber,errorMessage);
        if(!isInRange)
            Console.WriteLine("{0} is out of range!",errorMessage);
        return isInRange;        
    }

    //function which dynamically checks if in range based on number of values
    static bool CheckInRange(float max,float value,float thresholdNumber,string errorMessage,float ?min=null)
    {
        if(min.HasValue)
            return max<=value || min>=value;
        else
            return max<=value;
    }

    //function which calculates threshold number given max value and percentage
    static float calculateThresholdNumber(float thresholdPercent,float max)
    {
        return (max * (thresholdPercent/100));
    }

    static void DisplayWarningMessage(float min,float max,float value,float thresholdNumber)
    {
        if(min.HasValue)
        {
            if(value<min+thresholdNumber)
                Console.WriteLine("{0} is below threshold!",errorMessage);      
        }
        if(value>max-thresholdNumber)
            Console.WriteLine("{0} is above threshold!",errorMessage);
    }

    static void ExpectTrue(bool expression) {
        if(!expression) {
            Console.WriteLine("Expected true, but got false");
            Environment.Exit(1);
        }
    }
    static void ExpectFalse(bool expression) {
        if(expression) {
            Console.WriteLine("Expected false, but got true");
            Environment.Exit(1);
        }
    }
    static int Main() {
        ExpectTrue(batteryIsOk(25,5, 70,5, 0.7f,5));
        ExpectFalse(batteryIsOk(50,5, 85,5, 0.0f,5));
        Console.WriteLine("All ok");
        return 0;
    }
    
}
}
