using System;
using System.Windows.Forms;

namespace MissionPlanner.Keyboard
{
    public class Keyboard
    {
        private globalKeyboardHook gkh;
        private string setMode;
        private bool enabled = false;

        private float rollValue;
        private float throttleValue;
        private float pitchValue;
        private float yawValue;

        private Keys accelerate;
        private Keys decelerate;
        private Keys rollLeft;
        private Keys rollRight;
        private Keys steerLeft;
        private Keys steerRight;
        private Keys pitchForward;
        private Keys pitchBackward;
        private Keys arm;
        private Keys desarm;
        private Keys setModeKey;

        public Keyboard()
        {
            gkh = globalKeyboardHook.UniqueInstance;
            gkh.HookedKeys.Add(Keys.Control);
        }

        private void gkh_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == rollLeft)
            {
                MainV2.comPort.MAV.cs.rcoverridech1 = checkChannel(1, "trim");
            }
            if (e.KeyCode == rollRight)
            {
                MainV2.comPort.MAV.cs.rcoverridech1 = checkChannel(1, "trim");
            }
            if (e.KeyCode == pitchForward)
            {
                MainV2.comPort.MAV.cs.rcoverridech2 = checkChannel(2, "trim");
            }
            if (e.KeyCode == pitchBackward)
            {
                MainV2.comPort.MAV.cs.rcoverridech2 = checkChannel(2, "trim");
            }
            if (e.KeyCode == steerLeft)
            {
                MainV2.comPort.MAV.cs.rcoverridech4 = checkChannel(4, "trim");
            }
            if (e.KeyCode == steerRight)
            {
                MainV2.comPort.MAV.cs.rcoverridech4 = checkChannel(4, "trim");
            }
            if (MainV2.comPort.MAV.aptype == MAVLink.MAV_TYPE.GROUND_ROVER)
            {
                if (e.KeyCode == accelerate)
                {
                    MainV2.comPort.MAV.cs.rcoverridech3 = checkChannel(3, "trim");
                }
                if (e.KeyCode == decelerate)
                {
                    MainV2.comPort.MAV.cs.rcoverridech3 = checkChannel(3, "trim");
                }
            }
            e.Handled = true;
        }

        private void gkh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == arm)
            {
                MainV2.instance.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate ()
                {
                    try
                    {
                        MainV2.comPort.doARM(true);
                    }
                    catch { CustomMessageBox.Show("Failed to Arm"); }
                });
            }
            if (e.KeyCode == desarm)
            {
                MainV2.instance.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate ()
                {
                    try
                    {
                        MainV2.comPort.doARM(false);
                    }
                    catch { CustomMessageBox.Show("Failed to Disarm"); }
                });
            }
            if (e.KeyCode == setModeKey)
            {
                MainV2.instance.BeginInvoke((System.Windows.Forms.MethodInvoker)delegate ()
                {
                    try
                    {
                        MainV2.comPort.setMode(setMode);
                    }
                    catch { CustomMessageBox.Show("Failed to change Modes"); }
                });
            }
            if (e.KeyCode == rollLeft)
            {
                if (Control.ModifierKeys == Keys.Control)
                    MainV2.comPort.MAV.cs.rcoverridech1 = checkChannel(1, "min");
                else
                    MainV2.comPort.MAV.cs.rcoverridech1 = (ushort)(checkChannel(1, "trim") - Convert.ToUInt16(rollValue));
            }
            if (e.KeyCode == rollRight)
            {
                if (Control.ModifierKeys == Keys.Control)
                    MainV2.comPort.MAV.cs.rcoverridech1 = checkChannel(1, "max");
                else
                    MainV2.comPort.MAV.cs.rcoverridech1 = (ushort)(checkChannel(1, "trim") + Convert.ToUInt16(rollValue));
            }
            if (e.KeyCode == pitchForward)
            {
                if (Control.ModifierKeys == Keys.Control)
                    MainV2.comPort.MAV.cs.rcoverridech2 = checkChannel(2, "min");
                else
                    MainV2.comPort.MAV.cs.rcoverridech2 = (ushort)(checkChannel(2, "trim") - Convert.ToUInt16(pitchValue));
            }
            if (e.KeyCode == pitchBackward)
            {
                if (Control.ModifierKeys == Keys.Control)
                    MainV2.comPort.MAV.cs.rcoverridech2 = checkChannel(2, "max");
                else
                    MainV2.comPort.MAV.cs.rcoverridech2 = (ushort)(checkChannel(2, "trim") + Convert.ToUInt16(pitchValue));
            }
            if (MainV2.comPort.MAV.aptype == MAVLink.MAV_TYPE.GROUND_ROVER)
            {
                if (e.KeyCode == accelerate)
                {
                    if (Control.ModifierKeys == Keys.Control)
                        MainV2.comPort.MAV.cs.rcoverridech3 = checkChannel(3, "min");
                    else
                        MainV2.comPort.MAV.cs.rcoverridech3 = (ushort)(checkChannel(3, "trim") - Convert.ToUInt16(throttleValue));
                }
                if (e.KeyCode == decelerate)
                {
                    if (Control.ModifierKeys == Keys.Control)
                        MainV2.comPort.MAV.cs.rcoverridech3 = checkChannel(3, "max");
                    else
                        MainV2.comPort.MAV.cs.rcoverridech3 = (ushort)(checkChannel(3, "trim") + Convert.ToUInt16(throttleValue));
                }

            }
            else
            {
                if (e.KeyCode == accelerate)
                {
                    if (MainV2.comPort.MAV.cs.rcoverridech3 < checkChannel(3, "max"))
                        MainV2.comPort.MAV.cs.rcoverridech3 += (ushort)(100);
                }
                if (e.KeyCode == decelerate)
                {
                    if (MainV2.comPort.MAV.cs.rcoverridech3 > checkChannel(3, "min"))
                        MainV2.comPort.MAV.cs.rcoverridech3 -= (ushort)(100);
                }

            }
            if (e.KeyCode == steerLeft)
            {
                if (Control.ModifierKeys == Keys.Control)
                    MainV2.comPort.MAV.cs.rcoverridech4 = checkChannel(4, "min");
                else
                    MainV2.comPort.MAV.cs.rcoverridech4 = (ushort)(checkChannel(4, "trim") - Convert.ToUInt16(yawValue));
            }
            if (e.KeyCode == steerRight)
            {
                if (Control.ModifierKeys == Keys.Control)
                    MainV2.comPort.MAV.cs.rcoverridech4 = checkChannel(4, "max");
                else
                    MainV2.comPort.MAV.cs.rcoverridech4 = (ushort)(checkChannel(4, "trim") + Convert.ToUInt16(yawValue));
            }
            e.Handled = true;
        }

        private void clearRCOverride()
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
                MainV2.comPort.sendPacket(rc, rc.target_system, rc.target_component);
                System.Threading.Thread.Sleep(20);
                MainV2.comPort.sendPacket(rc, rc.target_system, rc.target_component);
                System.Threading.Thread.Sleep(20);
                MainV2.comPort.sendPacket(rc, rc.target_system, rc.target_component);
                System.Threading.Thread.Sleep(20);
                MainV2.comPort.sendPacket(rc, rc.target_system, rc.target_component);
                System.Threading.Thread.Sleep(20);
                MainV2.comPort.sendPacket(rc, rc.target_system, rc.target_component);
                System.Threading.Thread.Sleep(20);
                MainV2.comPort.sendPacket(rc, rc.target_system, rc.target_component);

                MainV2.comPort.sendPacket(rc, rc.target_system, rc.target_component);
                MainV2.comPort.sendPacket(rc, rc.target_system, rc.target_component);
                MainV2.comPort.sendPacket(rc, rc.target_system, rc.target_component);
            }
            catch { }
        }

        public ushort checkChannel(int chan, string minmaxtrim)
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

        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                
                enabled = value;
                if (enabled)
                {
                    MainV2.comPort.MAV.cs.rcoverridech1 = checkChannel(1, "trim");
                    MainV2.comPort.MAV.cs.rcoverridech2 = checkChannel(2, "trim");
                    MainV2.comPort.MAV.cs.rcoverridech3 = checkChannel(3, "min");
                    MainV2.comPort.MAV.cs.rcoverridech4 = checkChannel(4, "trim");
                }
                else
                {
                    clearRCOverride();
                }
            }

        }

        public Keys Accelerate {
            get
            {
                return accelerate;
            }
            set
            {
                gkh.HookedKeys.Add(value);
                accelerate = value;
            }
                
        }

        public Keys Decelerate {
            get
            {
                return decelerate;
            }
            set
            {
                gkh.HookedKeys.Add(value);
                decelerate = value;
            }

        }

        public Keys RollLeft {
            get
            {
                return rollLeft;
            }
            set
            {
                gkh.HookedKeys.Add(value);
                rollLeft = value;
            }

        }

        public Keys RollRight {
            get
            {
                return rollRight;
            }
            set
            {
                gkh.HookedKeys.Add(value);
                rollRight = value;
            }

        }

        public Keys SteerLeft {
            get
            {
                return steerLeft;
            }
            set
            {
                gkh.HookedKeys.Add(value);
                steerLeft = value;
            }

        }

        public Keys SteerRight {
            get
            {
                return steerRight;
            }
            set
            {
                gkh.HookedKeys.Add(value);
                steerRight = value;
            }

        }

        public Keys PitchForward
        {
            get
            {
                return pitchForward;
            }
            set
            {
                gkh.HookedKeys.Add(value);
                pitchForward = value;
            }

        }

        public Keys PitchBackward
        {
            get
            {
                return pitchBackward;
            }
            set
            {
                gkh.HookedKeys.Add(value);
                pitchBackward = value;
            }

        }

        public Keys Arm
        {
            get
            {
                return arm;
            }
            set
            {
                gkh.HookedKeys.Add(value);
                arm = value;
            }

        }

        public Keys Desarm
        {
            get
            {
                return desarm;
            }
            set
            {
                gkh.HookedKeys.Add(value);
                desarm = value;
            }

        }

        public Keys SetModeKey
        {
            get
            {
                return setModeKey;
            }
            set
            {
                gkh.HookedKeys.Add(value);
                setModeKey = value;
            }

        }

        public float RollValue
        {
            get
            {
                return rollValue;
            }

            set
            {
                rollValue = value;
            }
        }

        public float ThrottleValue
        {
            get
            {
                return throttleValue;
            }

            set
            {
                throttleValue = value;
            }
        }

        public float PitchValue
        {
            get
            {
                return pitchValue;
            }

            set
            {
                pitchValue = value;
            }
        }

        public float YawValue
        {
            get
            {
                return yawValue;
            }

            set
            {
                yawValue = value;
            }
        }

        public string SetMode
        {
            get
            {
                return setMode;
            }

            set
            {
                setMode = value;
            }
        }

        public void Hook()
        {
            gkh.KeyDown += new KeyEventHandler(gkh_KeyDown);
            gkh.KeyUp += new KeyEventHandler(gkh_KeyUp);
            gkh.hook();
        }

        public void Unhook()
        {
            gkh.KeyDown -= new KeyEventHandler(gkh_KeyDown);
            gkh.KeyUp -= new KeyEventHandler(gkh_KeyUp);
            gkh.HookedKeys.Clear();
            gkh.unhook();
        }
    }
}
