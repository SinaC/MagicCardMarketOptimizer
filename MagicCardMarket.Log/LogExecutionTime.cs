using System;
using System.Diagnostics;

namespace MagicCardMarket.Log
{
    public class LogExecutionTime : IDisposable
    {
        private readonly Stopwatch _stopwatch;
        private readonly string _operationName;

        public LogExecutionTime(string operationName)
        {
            _operationName = operationName;
            _stopwatch = new Stopwatch();
            _stopwatch.Start();

            Log.Default.WriteLine(LogLevels.Info, $"START {_operationName}");
        }

        public void Dispose()
        {
            _stopwatch.Stop();

            Log.Default.WriteLine(LogLevels.Info, $"STOP {_operationName} execution time: {_stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
