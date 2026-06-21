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
using Microsoft.Win32;
using static Guna.UI2.WinForms.Suite.Descriptions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.IdentityModel.Selectors;

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
            string Username= "", Password="";
            InitializeComponent();

            if (Properties.Settings.Default.RememberMe)
            {
                GetUsernameAndPassword(ref Username, ref Password);
                tbUsername.Text = Username;
                tbPassword.Text = Password;

       
                cbRememberMe.Checked = true;
            }
        }

        private bool RememberUsernameAndPassword(string Username , string Password)
        {
            // Storing Username and Password in Regedit 

            string keyPath = "HKEY_CURRENT_USER\\Software\\DVLD";
            string valueName1 = "Username";
            string valueName2 = "Password";

            try
            {
                Registry.SetValue(keyPath, valueName1, Username);
                Registry.SetValue(keyPath, valueName2, Password);

                Console.WriteLine($"Values {Username} and {Password} Successfuly Writed in Registry");

                return true;
            }

            catch (Exception ex)
            {
                Console.WriteLine($"An Error occured : {ex.Message}");
            }

            return false;
        }
        private bool GetUsernameAndPassword(ref string Username, ref string Password)
        {
            string keyPath = "HKEY_CURRENT_USER\\Software\\DVLD";
            string valueName1 = "Username";
            string valueName2 = "Password";

            try
            {
                Username = Registry.GetValue(keyPath, valueName1, null) as string;
                Password = Registry.GetValue(keyPath, valueName2, null) as string;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An Error occured : {ex.Message}");

            }
            return false;
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
                    RememberUsernameAndPassword(tbUsername.Text, tbPassword.Text);

                    Properties.Settings.Default.RememberMe = true;

                }
                else
                {
                    RememberUsernameAndPassword(string.Empty, string.Empty);
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
