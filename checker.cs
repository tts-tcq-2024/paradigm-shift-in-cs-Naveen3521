using System;
using System.Diagnostics;
namespace paradigm_shift_csharp
{
class Checker
{
    static bool batteryIsOk(float temperature, float soc, float chargeRate) 
    {
     bool isBatteryok = true;
     isbattery = ParameterInRange(0,45,temperature,"Temperature") && ParameterInRange(20,80,soc,"State of Charge") && CheckMaxValue(0.8,chargeRate,"Charge Rate")
    }

    
    static bool ParameterInRange(float min,float max,float value,string errorMessage)
    {
        bool isInRange = value>=min && CheckMaxvalue(max,value);
        if(!isInRange)
            Console.WriteLine("{0} is out of range!")
        return isInRange;        
    }

    static bool CheckMaxValue(float max, float value,string errorMessage)
    {
        bool isInRange = value<=max;
        if(!isInRange)
            Console.WriteLine("{0} is out of range!")
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
