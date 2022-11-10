using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MemeMakerWPF.Utility.Apps
{
    public class WaitCursor : IDisposable
    {
        private Cursor cursor;
        private int? delay;

        public WaitCursor(int? delay = null, bool appStarting = false, bool applicationCursor = false)
        {
            if (Mouse.OverrideCursor == Cursors.Wait)
                return;

            this.delay = delay;
            Mouse.OverrideCursor = appStarting ? Cursors.AppStarting : Cursors.Wait;
            if (applicationCursor)
                System.Windows.Forms.Application.UseWaitCursor = true;

            cursor = Mouse.OverrideCursor;
        }

        public async void Dispose()
        {
            if (cursor == null) return;

            await Wait(delay);
            Mouse.OverrideCursor = null;
            System.Windows.Forms.Application.UseWaitCursor= false;
        }

        private async Task Wait(int? delay)
        {
            await Task.Delay(delay ?? 0);
        }
    }
}
