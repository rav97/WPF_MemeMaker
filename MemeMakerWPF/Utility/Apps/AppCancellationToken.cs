using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MemeMakerWPF.Utility.Apps
{
    public sealed class AppCancellationToken
    {
        private static bool created = false;
        private static CancellationTokenSource tokenSource = null;

        /// <summary>
        /// Returns CancellationTokenSource for entire APP. Use this token when task should be cancelled together with entire app.
        /// </summary>
        public static CancellationTokenSource TokenSource
        {
            get
            {
                if(!created)
                {
                    tokenSource = new CancellationTokenSource();
                    created = true;
                }

                return tokenSource;
            }
        }

        /// <summary>
        /// Returns new CancellationTokenSource which is linked to App CancellationToken. Use this token when task should be cancelled together with entire app or you want to cancel it after task is done
        /// </summary>
        public static CancellationTokenSource LinkedTokenSource
        {
            get
            {
                var newToken = new CancellationTokenSource();
                return CancellationTokenSource.CreateLinkedTokenSource(newToken.Token, TokenSource.Token);
            }
        }
    }
}
