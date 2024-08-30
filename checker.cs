using System;
using System.Diagnostics;
namespace paradigm_shift_csharp
{
class Checker
{
    static bool batteryIsOk(float temperature, float soc, float chargeRate) {
        return TemmperatureChecker(temperature) && SocChecker(soc) && ChargeRateChecker(chargeRate);
    }

    public static bool TemmperatureChecker(float temperature)
    {
       bool isInRange = temperature>=0 && temperature<=45;
        if(!isInRange)
        {
            Console.WriteLine("Temperature is out of range!");
        }
        return isInRange;
    }

    public static bool SocChecker(float soc)
    {
       bool isInRange = soc>=20 && soc<=80;
        if(!isInRange)
        {
            Console.WriteLine("State of Charge is out of range!");
        }
        return isInRange;
    }

    public static bool ChargeRateChecker(float chargeRate)
    {
        bool isInRange = chargeRate>=0.8;
        if(!isInRange)
        {
            Console.WriteLine("Charge Rate is out of range!");
        }
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
