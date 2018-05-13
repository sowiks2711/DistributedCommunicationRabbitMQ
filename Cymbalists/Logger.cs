using System;

namespace Cymbalists
{
    public class Logger
    {
        private readonly object _consoleLock;

        public Logger()
        {
            _consoleLock = new object();
        }

        public void LoggTransition(int id, string transitionName)
        {
            lock (_consoleLock)
            {
                System.Console.WriteLine($" [{id}] took transition {transitionName} ");
            }
        }
        public void LoggMessageReceived(int id, string message)
        {
            lock (_consoleLock)
            {
                System.Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine($" [{id}] received {message}");
                System.Console.ResetColor();
            }
        }
        public void LoggMessageSent(int id, string message)
        {
            lock (_consoleLock)
            {
                System.Console.ForegroundColor = ConsoleColor.Cyan;
                System.Console.WriteLine($" [{id}] sent {message}");
                System.Console.ResetColor();
            }
        }
    }
}