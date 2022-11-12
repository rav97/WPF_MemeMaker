using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MemeMakerWPF.Utility.Apps
{
    public class TimedExecutionResult<R>
    {
        public bool ExecutionSucceded { get; }
        public R Result { get; }

        public TimedExecutionResult(bool succeded, R result)
        {
            this.ExecutionSucceded = succeded;
            this.Result = result;
        }
    }

    public static class TaskTimedExecution
    {
        public static bool ExecuteForTime(Action action, int timeout)
        {
            var cts = AppCancellationToken.LinkedTokenSource;
            var task = Task.Factory.StartNew(action, cts.Token);

            if(Task.WaitAny(new[] { task }, TimeSpan.FromMilliseconds(timeout)) < 0)
            {
                cts.Cancel();
                return false;
            }
            else if (task.Exception != null)
            {
                cts.Cancel();
                throw task.Exception;
            }
            return true;
        }

        public static bool ExecuteForTime(Action action, int timeout, CancellationTokenSource tokenSource)
        {
            var task = Task.Factory.StartNew(action, tokenSource.Token);

            if (Task.WaitAny(new[] { task }, TimeSpan.FromMilliseconds(timeout)) < 0)
            {
                tokenSource.Cancel();
                return false;
            }
            else if (task.Exception != null)
            {
                tokenSource.Cancel();
                throw task.Exception;
            }
            return true;
        }

        public static TimedExecutionResult<R> ExecuteForTime<R>(Func<R> func, int timeout)
        {
            var cts = AppCancellationToken.LinkedTokenSource;
            var task = Task.Factory.StartNew(func, cts.Token);
            R result = default(R);

            if (Task.WaitAny(new[] { task }, TimeSpan.FromMilliseconds(timeout)) < 0)
            {
                cts.Cancel();
                return new TimedExecutionResult<R>(false, result);
            }
            else if (task.Exception != null)
            {
                cts.Cancel();
                throw task.Exception;
            }

            result = task.Result;
            return new TimedExecutionResult<R>(true, result);
        }

        public static TimedExecutionResult<R> ExecuteForTime<R>(Func<R> func, int timeout, CancellationTokenSource tokenSource)
        {
            var task = Task.Factory.StartNew(func, tokenSource.Token);
            R result = default(R);

            if (Task.WaitAny(new[] { task }, TimeSpan.FromMilliseconds(timeout)) < 0)
            {
                tokenSource.Cancel();
                return new TimedExecutionResult<R>(false, result);
            }
            else if (task.Exception != null)
            {
                tokenSource.Cancel();
                throw task.Exception;
            }

            result = task.Result;
            return new TimedExecutionResult<R>(true, result);
        }

    }
}
