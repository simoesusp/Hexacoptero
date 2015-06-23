using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using MissionPlanner.Controls;
using MissionPlanner.Utilities;
using MissionPlanner.Keyboard;

namespace MissionPlanner.Keyboard
{
    public partial class KeyboardSetup : Form
    {
        globalKeyboardHook gkh = new globalKeyboardHook();        

        public KeyboardSetup()
        {
            InitializeComponent();
            MissionPlanner.Utilities.Tracking.AddPage(this.GetType().ToString(), this.Text);
        }

        public void Keyboard_Load(object sender, EventArgs e)
        {            
            gkh.HookedKeys.Add(Keys.Left);
            gkh.HookedKeys.Add(Keys.Right);
            gkh.HookedKeys.Add(Keys.Up);
            gkh.HookedKeys.Add(Keys.Down);
            gkh.HookedKeys.Add(Keys.PageUp);
            gkh.HookedKeys.Add(Keys.PageDown);
            gkh.HookedKeys.Add(Keys.Escape);
            gkh.HookedKeys.Add(Keys.W);
            gkh.HookedKeys.Add(Keys.S);
            gkh.HookedKeys.Add(Keys.A);
            gkh.HookedKeys.Add(Keys.D);
            gkh.KeyDown += new KeyEventHandler(gkh_KeyDown);
            gkh.KeyUp += new KeyEventHandler(gkh_KeyUp);    
            
            if (MainV2.keyboard)
            {
                BUT_enable.Text = "Disable";
                timer1.Start();
            }
        }

        private void BUT_enable_Click(object sender, EventArgs e)
        {
            if (MainV2.keyboard == false)
            {
                MainV2.comPort.MAV.cs.rcoverridech1 = checkChannel(1, "trim");
                MainV2.comPort.MAV.cs.rcoverridech2 = checkChannel(2, "trim");
                MainV2.comPort.MAV.cs.rcoverridech3 = checkChannel(3, "trim");
                MainV2.comPort.MAV.cs.rcoverridech4 = checkChannel(4, "trim");
                gkh.hook();
                MainV2.keyboard = true;
                BUT_enable.Text = "Disable";
            }
            else
            {
                gkh.unhook();
                MainV2.keyboard = false;
                clearRCOverride();
                BUT_enable.Text = "Enable";
            }
        }

        void gkh_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    MainV2.comPort.MAV.cs.rcoverridech1 = (ushort)(1500);
                    break;
                case Keys.Right:
                    MainV2.comPort.MAV.cs.rcoverridech1 = (ushort)(1500);
                    break;
                case Keys.Up:
                    MainV2.comPort.MAV.cs.rcoverridech2 = (ushort)(1500);
                    break;
                case Keys.Down:
                    MainV2.comPort.MAV.cs.rcoverridech2 = (ushort)(1500);
                    break;
                case Keys.W:
                    MainV2.comPort.MAV.cs.rcoverridech3 = (ushort)(1425);
                    break;
                case Keys.S:
                    MainV2.comPort.MAV.cs.rcoverridech3 = (ushort)(1425);
                    break;
                case Keys.A:
                    MainV2.comPort.MAV.cs.rcoverridech4 = (ushort)(1500);
                    break;
                case Keys.D:
                    MainV2.comPort.MAV.cs.rcoverridech4 = (ushort)(1500);
                    break;
            }
       
        }

        void gkh_KeyDown(object sender, KeyEventArgs e) 
        {
            switch (e.KeyCode)
            {
                case Keys.PageUp:
                    MainV2.instance.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate()
                    {
                        try
                        {
                            MainV2.comPort.doARM(true);
                        }
                        catch { CustomMessageBox.Show("Failed to Arm"); }
                    });
                    break;
                case Keys.Left:
                    /*if (MainV2.comPort.MAV.cs.rcoverridech1 > checkChannel(1, "min"))
                        MainV2.comPort.MAV.cs.rcoverridech1 -= (ushort)(100);*/
                    MainV2.comPort.MAV.cs.rcoverridech1 = (ushort)(1400);
                    break;
                case Keys.Right:
                    /*if (MainV2.comPort.MAV.cs.rcoverridech1 < checkChannel(1, "max"))
                        MainV2.comPort.MAV.cs.rcoverridech1 += (ushort)(100);*/
                    MainV2.comPort.MAV.cs.rcoverridech1 = (ushort)(1600);
                    break;
                case Keys.Up:
                    /*if (MainV2.comPort.MAV.cs.rcoverridech2 > checkChannel(2, "min"))
                        MainV2.comPort.MAV.cs.rcoverridech2 -= (ushort)(100);*/
                    MainV2.comPort.MAV.cs.rcoverridech2 = (ushort)(1400);
                    break;
                case Keys.Down:
                    /*if (MainV2.comPort.MAV.cs.rcoverridech2 < checkChannel(2, "max"))
                        MainV2.comPort.MAV.cs.rcoverridech2 += (ushort)(100);*/
                    MainV2.comPort.MAV.cs.rcoverridech2 = (ushort)(1600);
                    break;
                case Keys.W:
                    if (MainV2.comPort.MAV.cs.rcoverridech3 < checkChannel(3, "max"))
                        MainV2.comPort.MAV.cs.rcoverridech3 += (ushort)(100);
                    break;
                case Keys.S:
                    if (MainV2.comPort.MAV.cs.rcoverridech3 > checkChannel(3, "min"))
                        MainV2.comPort.MAV.cs.rcoverridech3 -= (ushort)(100);
                    break;
                case Keys.A:
                    /*if (MainV2.comPort.MAV.cs.rcoverridech4 > checkChannel(4, "min"))
                        MainV2.comPort.MAV.cs.rcoverridech4 -= (ushort)(100);*/
                    MainV2.comPort.MAV.cs.rcoverridech4 = (ushort)(1400);
                    break;
                case Keys.D:
                    /*if (MainV2.comPort.MAV.cs.rcoverridech4 < checkChannel(4, "max"))
                        MainV2.comPort.MAV.cs.rcoverridech4 += (ushort)(100);*/
                    MainV2.comPort.MAV.cs.rcoverridech4 = (ushort)(1600);
                    break;
                case Keys.Escape:
                    MainV2.comPort.MAV.cs.rcoverridech1 = (ushort)0;
                    MainV2.comPort.MAV.cs.rcoverridech2 = (ushort)0;
                    MainV2.comPort.MAV.cs.rcoverridech3 = (ushort)0;
                    MainV2.comPort.MAV.cs.rcoverridech4 = (ushort)0;
                    clearRCOverride();
                    break;
            }            
        }

        public void clearRCOverride()
        {

            MAVLink.mavlink_rc_channels_override_t rc = new MAVLink.mavlink_rc_channels_override_t();

            rc.target_component = MainV2.comPort.MAV.compid;
            rc.target_system = MainV2.comPort.MAV.sysid;

            rc.chan1_raw = 0;
            rc.chan2_raw = 0;
            rc.chan3_raw = 0;
            rc.chan4_raw = 0;
            rc.chan5_raw = 0;
            rc.chan6_raw = 0;
            rc.chan7_raw = 0;
            rc.chan8_raw = 0;

            try
            {
                MainV2.comPort.sendPacket(rc);
                MainV2.comPort.sendPacket(rc);
                MainV2.comPort.sendPacket(rc);
            }
            catch { }
        }

        private ushort checkChannel(int chan, string minmaxtrim)
        {
            ushort min, max, trim = 0;
            if (MainV2.comPort.MAV.param.Count > 0)
            {
                try
                {
                    if (MainV2.comPort.MAV.param.ContainsKey("RC" + chan + "_MIN"))
                    {
                        min = (ushort)(float)(MainV2.comPort.MAV.param["RC" + chan + "_MIN"]);
                        max = (ushort)(float)(MainV2.comPort.MAV.param["RC" + chan + "_MAX"]);
                        trim = (ushort)(float)(MainV2.comPort.MAV.param["RC" + chan + "_TRIM"]);
                    }
                    else
                    {
                        min = 1000;
                        max = 2000;
                        trim = 1500;
                    }
                }
                catch
                {
                    min = 1000;
                    max = 2000;
                    trim = 1500;
                }
            }
            else
            {
                min = 1000;
                max = 2000;
                trim = 1500;
            }
            if (minmaxtrim == "min")
                return min;
            if (minmaxtrim == "max")
                return max;
            if (minmaxtrim == "trim")
                return trim;
            else
                return 0;
        }

        /*protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    return true;
                case Keys.Shift | Keys.Right:
                case Keys.Shift | Keys.Left:
                case Keys.Shift | Keys.Up:
                case Keys.Shift | Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            MAVLink.mavlink_rc_channels_override_t rc = new MAVLink.mavlink_rc_channels_override_t();
            rc.target_component = MainV2.comPort.MAV.compid;
            rc.target_system = MainV2.comPort.MAV.sysid;
            rc.chan1_raw = 0;
            rc.chan2_raw = 0;
            rc.chan3_raw = 0;
            rc.chan4_raw = 0;
            rc.chan5_raw = 0;
            rc.chan6_raw = 0;
            rc.chan7_raw = 0;
            rc.chan8_raw = 0;
            switch (e.KeyCode)
            {
                case Keys.Left:
                    //MainV2.comPort.MAV.cs.rcoverridech1 = (ushort)(1400);
                    rc.chan1_raw = (ushort)(1400);
                    MainV2.comPort.sendPacket(rc);
                    break;
                case Keys.Right:
                    //MainV2.comPort.MAV.cs.rcoverridech1 = (ushort)(1600);
                    rc.chan1_raw = (ushort)(1600);
                    MainV2.comPort.sendPacket(rc);
                    break;
                case Keys.Up:
                    //MainV2.comPort.MAV.cs.rcoverridech2 = (ushort)(1400);
                    rc.chan2_raw = (ushort)(1400);
                    MainV2.comPort.sendPacket(rc);
                    break;
                case Keys.Down:
                    //MainV2.comPort.MAV.cs.rcoverridech2 = (ushort)(1600);
                    rc.chan2_raw = (ushort)(1600);
                    MainV2.comPort.sendPacket(rc);
                    break;
                case Keys.Escape:
                    //MainV2.comPort.MAV.cs.rcoverridech1 = (ushort)0;
                    //MainV2.comPort.MAV.cs.rcoverridech2 = (ushort)0;
                    //MainV2.joystick.clearRCOverride();
                    rc.chan1_raw = (ushort)(0);
                    rc.chan2_raw = (ushort)(0);
                    MainV2.comPort.sendPacket(rc);
                    break;
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            MAVLink.mavlink_rc_channels_override_t rc = new MAVLink.mavlink_rc_channels_override_t();
            rc.target_component = MainV2.comPort.MAV.compid;
            rc.target_system = MainV2.comPort.MAV.sysid;
            rc.chan1_raw = 0;
            rc.chan2_raw = 0;
            rc.chan3_raw = 0;
            rc.chan4_raw = 0;
            rc.chan5_raw = 0;
            rc.chan6_raw = 0;
            rc.chan7_raw = 0;
            rc.chan8_raw = 0;
            switch (e.KeyCode)
            {
                case Keys.Left:
                    //MainV2.comPort.MAV.cs.rcoverridech1 = (ushort)(1500);
                    rc.chan1_raw = (ushort)(1500);
                    MainV2.comPort.sendPacket(rc);
                    break;
                case Keys.Right:
                    //MainV2.comPort.MAV.cs.rcoverridech1 = (ushort)(1500);
                    rc.chan1_raw = (ushort)(1500);
                    MainV2.comPort.sendPacket(rc);
                    break;
                case Keys.Up:
                    //MainV2.comPort.MAV.cs.rcoverridech2 = (ushort)(1500);
                    rc.chan2_raw = (ushort)(1500);
                    MainV2.comPort.sendPacket(rc);
                    break;
                case Keys.Down:
                    //MainV2.comPort.MAV.cs.rcoverridech2 = (ushort)(1500);
                    rc.chan2_raw = (ushort)(1500);
                    MainV2.comPort.sendPacket(rc);
                    break;
            }
        }*/

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBarRoll.Value = MainV2.comPort.MAV.cs.rcoverridech1;
            progressBarPitch.Value = MainV2.comPort.MAV.cs.rcoverridech2;
            progressBarThrottle.Value = MainV2.comPort.MAV.cs.rcoverridech3;
            progressBarYaw.Value = MainV2.comPort.MAV.cs.rcoverridech4;

            try
            {
                progressBarRoll.Maximum = checkChannel(1,"max");
                progressBarPitch.Maximum = checkChannel(2, "max");
                progressBarThrottle.Maximum = checkChannel(3, "max");
                progressBarYaw.Maximum = checkChannel(4, "max");

                progressBarRoll.Minimum = checkChannel(1, "min");
                progressBarPitch.Minimum = checkChannel(2, "min");
                progressBarThrottle.Minimum = checkChannel(3, "min");
                progressBarYaw.Minimum = checkChannel(4, "min");
            
            }
            catch
            {
                //Exception Error in the application. -2147024866 (DIERR_INPUTLOST)

            }
        }
    }
}
