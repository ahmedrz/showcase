using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Telerik.Windows.Controls;

namespace EmanifestDownloader.Helper
{
    public static class Extensions
    {
        public static void ShowInTaskbar(this RadWindow control, string title)
        {
            control.Show();
            var window = control.ParentOfType<Window>();
            window.ShowInTaskbar = true;
            window.Title = title;
            var uri = new Uri("pack://application:,,,/EmanifestDownloader;component/edi_logo_nPK_icon.ico");
            window.Icon = BitmapFrame.Create(uri);
        }
    }
}
