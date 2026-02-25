using DVLD_BusinessAccess;
using DVLD_PresentationAccess.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Guna.UI2.WinForms.Suite.Descriptions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace DVLD_PresentationAccess
{
   

    public partial class LoginScreen: Form
    {

        // Import Windows API functions
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        // Constants for the message
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        private Timer blinkTimer;

        private bool labelVisible = true;


        public LoginScreen()
        {
            InitializeComponent();

            if (Properties.Settings.Default.RememberMe)
            {
                tbUsername.Text = Properties.Settings.Default.RememberUserName;
                tbPassword.Text = Properties.Settings.Default.RememberPassword;
                cbRememberMe.Checked = true;
            }
        }


        private void LoginScreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }


        private void Hide_Show_Click(object sender, EventArgs e)
        {
            if (HideShowPassword.Checked)
            {
                // Show password
                HideShowPassword.Checked = false;
                HideShowPassword.BackgroundImage = Properties.Resources.show;
                tbPassword.PasswordChar = '\0'; // Set to null character to unmask
            }
            else
            {
                // Hide password
                HideShowPassword.Checked = true;
                HideShowPassword.BackgroundImage = Properties.Resources.hide;
                tbPassword.PasswordChar = '*'; // Set back to asterisk to mask
            }

        }


        private void Login_Click(object sender, EventArgs e)
        {
            // Attempt login
            clsUser user = clsUser.Login(tbUsername.Text, tbPassword.Text);

            if (user != null)
            {
                // ✅ Remember Me handling
                if (cbRememberMe.Checked)
                {
                    Properties.Settings.Default.RememberUserName = tbUsername.Text;
                    Properties.Settings.Default.RememberPassword = tbPassword.Text;
                    Properties.Settings.Default.RememberMe = true;
                }
                else
                {
                    Properties.Settings.Default.RememberUserName = string.Empty;
                    Properties.Settings.Default.RememberPassword = string.Empty;
                    Properties.Settings.Default.RememberMe = false;
                }
                Properties.Settings.Default.Save(); // Save the settings

                // ✅ Start session
                clsPerson person = clsPerson.Find(user.PersonID);
                Session.Start(user, person);

                // ✅ Open dashboard
                this.Hide();
                Dashboard frm = new Dashboard();
                frm.ShowDialog();
                this.Close();
            }
            else
            {
                // ❌ Invalid login → blink message
                ShowBlinkMessage();
            }
        }


        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void ShowBlinkMessage()
        {
            lblMessage.Visible = true;

            blinkTimer = new Timer();
            blinkTimer.Interval = 300; // 0.3 seconds
            blinkTimer.Tick += (s, e) =>
            {
                labelVisible = !labelVisible;
                lblMessage.Visible = labelVisible;
            };
            blinkTimer.Start();

            // Optional: stop blinking after 2 seconds
            Task.Delay(2000).ContinueWith(_ =>
            {
                blinkTimer.Stop();
                lblMessage.Visible = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }




    }
}
