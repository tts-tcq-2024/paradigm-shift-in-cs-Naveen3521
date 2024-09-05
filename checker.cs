using System;
using System.Diagnostics;
namespace paradigm_shift_csharp
{
class Checker
{
    static bool batteryIsOk(float temperature, float soc, float chargeRate) {
       bool checkTemperature = ParameterInRange(0,45,temperature);
       bool checksoc = ParameterInRange(20,80,soc);
       bool checkChargeRate = CheckMaxValue(0.8,chargeRate);
       if(!checkTemperature)
           Console.WriteLine("Temperature is out of range!");
       if(!checksoc)
           Console.WriteLine("State of Charge is out of range!");
        if(!checkChargeRate)
           Console.WriteLine("Charge Rate is out of range!");
        return checkTemperature && checksoc && checkChargeRate;
    }

    
    static bool ParameterInRange(float min,float max,float value)
    {
        bool isInRange = value>=min && CheckMaxvalue(max,value);
        return isInRange;        
    }

    static bool CheckMaxvalue(float max, float value)
    {
        bool isInRange = value<=max;
        return isInRange;
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
        ExpectTrue(batteryIsOk(25, 70, 0.7f));
        ExpectFalse(batteryIsOk(50, 85, 0.0f));
        Console.WriteLine("All ok");
        return 0;
    }
    
}
}
