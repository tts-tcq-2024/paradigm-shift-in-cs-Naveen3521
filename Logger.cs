using System;

namespace paradigm_shift_csharp
{
    public interface IMessageLogger
    {
        void LogError(string message);
        void LogWarning(string message);
    }

    public class ConsoleLogger : IMessageLogger
    {
        public void LogError(string message)
        {
            Console.WriteLine($"Error: {message}");
        }

        public void LogWarning(string message)
        {
            Console.WriteLine($"Warning: {message}");
        }
    }
}
