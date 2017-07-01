using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using MissionPlanner.Utilities;

namespace MissionPlanner.Keyboard
{
    public partial class KeyboardSetup : Form
    {

        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        globalKeyboardHook gkh =  globalKeyboardHook.UniqueInstance;

        public KeyboardSetup()
        {
            InitializeComponent();
            MissionPlanner.Utilities.Tracking.AddPage(this.GetType().ToString(), this.Text);
            
        }

        public void Keyboard_Load(object sender, EventArgs e)
        {
            //fixed border
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //maximize button removed
            this.MaximizeBox = false;
            setModeComboBox.DataSource = Common.getModesList(MainV2.comPort.MAV.cs);
            setModeComboBox.ValueMember = "Key";
            setModeComboBox.DisplayMember = "Value";

            if (MainV2.keyboard != null && (MainV2.keyboard.Enabled))
            {
                BUT_enable.Text = "Disable";
            }

            if (MainV2.comPort.BaseStream.IsOpen)
            {
                lblVehicleConnected.Text = MainV2.comPort.MAV.aptype.ToString();
            }
        }

        private void BUT_enable_Click(object sender, EventArgs e)
        {
            if (MainV2.keyboard != null && (!MainV2.keyboard.Enabled))
            {
                if (MainV2.comPort.BaseStream.IsOpen)
                {
                    lblVehicleConnected.Text = MainV2.comPort.MAV.aptype.ToString();                    
                    try
                    {                       
                        MainV2.keyboard.Accelerate = (Keys)System.Enum.Parse(typeof(Keys), accelerateBox.Text, true);
                        MainV2.keyboard.Decelerate = (Keys)System.Enum.Parse(typeof(Keys), decelerateBox.Text, true);
                        MainV2.keyboard.RollLeft = (Keys)System.Enum.Parse(typeof(Keys), rollLeftBox.Text, true);
                        MainV2.keyboard.RollRight = (Keys)System.Enum.Parse(typeof(Keys), rollRightBox.Text, true);
                        MainV2.keyboard.SteerLeft = (Keys)System.Enum.Parse(typeof(Keys), steerLeftBox.Text, true);
                        MainV2.keyboard.SteerRight = (Keys)System.Enum.Parse(typeof(Keys), steerRightBox.Text, true);
                        MainV2.keyboard.PitchForward = (Keys)System.Enum.Parse(typeof(Keys), pitchForwardBox.Text, true);
                        MainV2.keyboard.PitchBackward = (Keys)System.Enum.Parse(typeof(Keys), pitchBackwardBox.Text, true);
                        MainV2.keyboard.Arm = (Keys)System.Enum.Parse(typeof(Keys), armBox.Text, true);
                        MainV2.keyboard.Desarm = (Keys)System.Enum.Parse(typeof(Keys), desarmBox.Text, true);
                        MainV2.keyboard.SetModeKey = (Keys)System.Enum.Parse(typeof(Keys), setModeBox.Text, true);
                        MainV2.keyboard.SetMode = setModeComboBox.Text;
                        MainV2.keyboard.PitchValue = pitchTrackBar.Value;
                        MainV2.keyboard.YawValue = yawTrackBar.Value;
                        MainV2.keyboard.RollValue = rollTrackBar.Value;
                        MainV2.keyboard.ThrottleValue = throttleTrackBar.Value;
                    }
                    catch
                    {
                        MainV2.instance.Invoke((System.Action)
                        delegate
                        {
                            CustomMessageBox.Show("Please insert a key in all boxes before pressing enable", "Empty Boxes");
                        });
                        return;
                    }                    
                    MainV2.keyboard.Hook();
                    MainV2.keyboard.Enabled = true;
                    BUT_enable.Text = "Disable";
                    timer1.Start();

                }
                else
                    CustomMessageBox.Show("Please connect a UAV first", "Open ComPort");
            }
            else
            {
                MainV2.keyboard.Unhook();
                MainV2.keyboard.Enabled = false;
                timer1.Stop();
                BUT_enable.Text = "Enable";
            }            
        }                      

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (MainV2.keyboard != null)
            {
                progressBarRoll.Value = MainV2.comPort.MAV.cs.rcoverridech1;
                progressBarPitch.Value = MainV2.comPort.MAV.cs.rcoverridech2;
                progressBarThrottle.Value = MainV2.comPort.MAV.cs.rcoverridech3;
                progressBarYaw.Value = MainV2.comPort.MAV.cs.rcoverridech4;

                try
                {
                    progressBarRoll.Maximum = MainV2.keyboard.checkChannel(1, "max");
                    progressBarPitch.Maximum = MainV2.keyboard.checkChannel(2, "max");
                    progressBarThrottle.Maximum = MainV2.keyboard.checkChannel(3, "max");
                    progressBarYaw.Maximum = MainV2.keyboard.checkChannel(4, "max");

                    progressBarRoll.Minimum = MainV2.keyboard.checkChannel(1, "min");
                    progressBarPitch.Minimum = MainV2.keyboard.checkChannel(2, "min");
                    progressBarThrottle.Minimum = MainV2.keyboard.checkChannel(3, "min");
                    progressBarYaw.Minimum = MainV2.keyboard.checkChannel(4, "min");

                }
                catch
                {
                    //Exception Error in the application. -2147024866 (DIERR_INPUTLOST)

                }

                try
                {
                    rollTrackBar.Maximum = (MainV2.keyboard.checkChannel(1, "max") - MainV2.keyboard.checkChannel(1, "min")) / 2;
                    pitchTrackBar.Maximum = (MainV2.keyboard.checkChannel(2, "max") - MainV2.keyboard.checkChannel(2, "min")) / 2;
                    yawTrackBar.Maximum = (MainV2.keyboard.checkChannel(4, "max") - MainV2.keyboard.checkChannel(4, "min")) / 2;
                    throttleTrackBar.Maximum = (MainV2.keyboard.checkChannel(3, "max") - MainV2.keyboard.checkChannel(3, "min")) / 2;
                    rollTrackBarMaxValue.Text = rollTrackBar.Maximum.ToString();
                    pitchTrackBarMaxValue.Text = pitchTrackBar.Maximum.ToString();
                    yawTrackBarMaxValue.Text = yawTrackBar.Maximum.ToString();
                    throttleTrackBarMaxValue.Text = throttleTrackBar.Maximum.ToString();

                    rollTrackBar.Minimum = 100;
                    pitchTrackBar.Minimum = 100;
                    yawTrackBar.Minimum = 100;
                    throttleTrackBar.Minimum = 100;
                    rollTrackBarMinValue.Text = rollTrackBar.Minimum.ToString();
                    pitchTrackBarMinValue.Text = pitchTrackBar.Minimum.ToString();
                    yawTrackBarMinValue.Text = yawTrackBar.Minimum.ToString();
                    throttleTrackBarMinValue.Text = throttleTrackBar.Minimum.ToString();

                }
                catch
                {
                    //Exception Error in the application. -2147024866 (DIERR_INPUTLOST)

                }
            }
        }

        string auxText;

        public void allTextBox_Click(object sender, EventArgs e)
        {
            TextBox tbox = (TextBox)sender;
            string auxText = tbox.Text;
            if (MainV2.keyboard != null)
            {
                tbox.Cursor = Cursors.Default;
                if (tbox.ReadOnly)
                {
                    tbox.ReadOnly = false;
                    tbox.Text = string.Empty;
                    tbox.BackColor = TextBox.DefaultBackColor;
                    tbox.ForeColor = TextBox.DefaultForeColor;
                }
            }
            HideCaret(tbox.Handle);
            
        }

        private void allTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tbox = (TextBox)sender;
            if(!tbox.ReadOnly)
            {
                switch(e.KeyCode)
                {
                    case Keys.ControlKey:
                        CustomMessageBox.Show("Control Key not allowed.", "Warning");
                        tbox.Text = auxText;
                        break;
                    case Keys.LControlKey:
                        CustomMessageBox.Show("Control Key not allowed.", "Warning");
                        tbox.Text = auxText;
                        break;
                    case Keys.Escape:
                        tbox.Text = auxText;
                        break;
                    default:
                        tbox.AppendText(e.KeyCode.ToString());
                        break;
                }
                tbox.BackColor = ThemeManager.ControlBGColor;
                tbox.ForeColor = ThemeManager.TextColor;
                tbox.ReadOnly = true;
                
            }
        }

        private void allTextBox_Leave(object sender, EventArgs e)
        {
            TextBox tbox = (TextBox)sender;
            if (!tbox.ReadOnly)
            {
                tbox.Text = auxText;
                tbox.BackColor = ThemeManager.ControlBGColor;
                tbox.ForeColor = ThemeManager.TextColor;
                tbox.ReadOnly = true;
            }
        }

        private void allTextBox_MouseEnter(object sender, EventArgs e)
        {
            TextBox tbox = (TextBox)sender;
            if (MainV2.keyboard != null && MainV2.keyboard.Enabled)
            {
                tbox.Cursor = Cursors.No;
            }
            if (MainV2.keyboard != null && !MainV2.keyboard.Enabled)
            {
                tbox.Cursor = Cursors.Hand;
            }
        }

        private void BUT_help_Click(object sender, EventArgs e)
        {
            new Keyboard_Help().ShowDialog();
        }

    }
}
