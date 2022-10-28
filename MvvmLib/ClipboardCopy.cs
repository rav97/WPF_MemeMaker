using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace MvvmLib
{
    public class ClipboardCopy
    {
        public System.Windows.Forms.NotifyIcon nIcon;

        public void Copy(object data)
        {
            SetBalloonTip();
            Clipboard.SetText(data?.ToString());
            nIcon.ShowBalloonTip(1000);
            Task.Delay(5000).ContinueWith(t => nIcon.Dispose()); // bez tego każde nowe powiadomienie wisi w nieskończoność
        }
        
        private void SetBalloonTip()
        {
            nIcon = new System.Windows.Forms.NotifyIcon()
            {
                Visible = true,
                Icon = System.Drawing.SystemIcons.Information,
                BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info,
                BalloonTipText = "Skopiowano do schowka",
            };
            
        }

    }
}
