using System;
using System.Windows.Forms;

namespace ClipSchuss
{
    public partial class MainWindow : Form
    {
        KeyboardHook hook = new KeyboardHook();
        ClipHandler ClipHandler = new ClipHandler();

        public MainWindow()
        {
            InitializeComponent();
            this.CenterToScreen();

            this.Icon = Properties.Resources.Clipboard;
            this.SystemTrayIcon.Icon = Properties.Resources.Clipboard;

            this.SystemTrayIcon.Text = "ClipSchuss";
            this.SystemTrayIcon.Visible = true;

            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add("Open", ContextMenuOpen);
            menu.MenuItems.Add("Exit", ContextMenuExit);
            this.SystemTrayIcon.ContextMenu = menu;

            this.Resize += WindowResize;
            this.FormClosing += WindowClosing;

            this.WindowState = FormWindowState.Minimized;

            hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(Hook_KeyPressed);
            hook.RegisterHotKey(ClipSchuss.ModifierKeys.Control | ClipSchuss.ModifierKeys.Alt, Keys.C);
            hook.RegisterHotKey(ClipSchuss.ModifierKeys.Control | ClipSchuss.ModifierKeys.Alt, Keys.V);
        }

        private void SystemTrayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void ContextMenuOpen(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void ContextMenuExit(object sender, EventArgs e)
        {
            this.SystemTrayIcon.Visible = false;
            Application.Exit();
            Environment.Exit(0);
        }

        private void WindowResize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void WindowClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        async void Hook_KeyPressed(object sender, KeyPressedEventArgs e)
        {
            if (e.Key.ToString() == "C")
            {
                string message = await ClipHandler.SendClip();
                SystemTrayIcon.ShowBalloonTip(2000, "ClipSchuss", message, ToolTipIcon.Info);
            }
            else if (e.Key.ToString() == "V")
            {
                string message = await ClipHandler.GetClip();
                SystemTrayIcon.ShowBalloonTip(2000, "ClipSchuss", message, ToolTipIcon.Info);
            }
        }
    }
}