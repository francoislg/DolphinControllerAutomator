using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vJoyInterfaceWrap;

using DolphinControllerAutomator;

namespace DolphinControllerAutomator.Controllers {
    public class vJoyController : DolphinController {
        private vJoy joystick;
        private vJoy.JoystickState joystickState;
        private uint deviceID = 1;
        private int defaultDelay = 20;
        private long minYValue;
        private long maxYValue;
        private long minXValue;
        private long maxXValue;
        private uint ABUTTON = 1;
        private uint BBUTTON = 2;
        private uint XBUTTON = 3;
        private uint YBUTTON = 4;
        private uint LBUTTON = 5;
        private uint RBUTTON = 6;
        private uint ZBUTTON = 7;
        private uint STARTBUTTON = 8;

        public vJoyController(uint deviceID) {
            this.deviceID = deviceID;

            joystick = new vJoy();
            joystickState = new vJoy.JoystickState();

            VjdStat status = joystick.GetVJDStatus(deviceID);
            outputStatus(status);
            verifyVersions();
            acquireTarget(status);
            setJoystickBounds();
        }

        private void outputStatus(VjdStat status) {
            switch (status) {
                case VjdStat.VJD_STAT_OWN:
                    Console.WriteLine("vJoy Device {0} is already owned by this feeder\n", deviceID);
                    break;
                case VjdStat.VJD_STAT_FREE:
                    Console.WriteLine("vJoy Device {0} is free\n", deviceID);
                    break;
                case VjdStat.VJD_STAT_BUSY:
                    Console.WriteLine("vJoy Device {0} is already owned by another feeder\nCannot continue\n", deviceID);
                    break;
                case VjdStat.VJD_STAT_MISS:
                    Console.WriteLine("vJoy Device {0} is not installed or disabled\nCannot continue\n", deviceID);
                    break;
                default:
                    Console.WriteLine("vJoy Device {0} general error\nCannot continue\n", deviceID);
                    break;
            };
        }

        private void verifyVersions() {
            UInt32 DllVer = 0, DrvVer = 0;
            bool match = joystick.DriverMatch(ref DllVer, ref DrvVer);
            if (match)
                Console.WriteLine("Version of Driver Matches DLL Version ({0:X})\n", DllVer);
            else
                Console.WriteLine("Version of Driver ({0:X}) does NOT match DLL Version ({1:X})\n", DrvVer, DllVer);
        }

        private void acquireTarget(VjdStat status) {
            if ((status == VjdStat.VJD_STAT_OWN) || ((status == VjdStat.VJD_STAT_FREE) && (!joystick.AcquireVJD(deviceID)))) {
                Console.WriteLine("Failed to acquire vJoy device number {0}.\n", deviceID);
                throw new CouldNotAcquireTarget();
            } else
                Console.WriteLine("Acquired: vJoy device number {0}.\n", deviceID);
        }

        private void setJoystickBounds() {
            joystick.GetVJDAxisMin(deviceID, HID_USAGES.HID_USAGE_X, ref minXValue);
            joystick.GetVJDAxisMin(deviceID, HID_USAGES.HID_USAGE_Y, ref minYValue);
            
            joystick.GetVJDAxisMax(deviceID, HID_USAGES.HID_USAGE_X, ref maxXValue);
            joystick.GetVJDAxisMax(deviceID, HID_USAGES.HID_USAGE_Y, ref maxYValue);
            
        }

        public DolphinController setDelay(int delay) {
            this.defaultDelay = delay;
            return this;
        }

        public DolphinController joystickUp() {
            pushJoystick(maxYValue, HID_USAGES.HID_USAGE_Y);
            return this;
        }

        public DolphinController joystickDown() {
            pushJoystick(minYValue, HID_USAGES.HID_USAGE_Y);
            return this;
        }

        public DolphinController joystickLeft() {
            pushJoystick(minXValue, HID_USAGES.HID_USAGE_X);
            return this;
        }

        public DolphinController joystickRight() {
            pushJoystick(maxXValue, HID_USAGES.HID_USAGE_X);
            return this;
        }

        public DolphinController pressA() {
            press(ABUTTON);
            return this;
        }

        public DolphinController pressB() {
            press(BBUTTON);
            return this;
        }

        public DolphinController pressX() {
            press(XBUTTON);
            return this;
        }

        public DolphinController pressY() {
            press(YBUTTON);
            return this;
        }

        public DolphinController pressL() {
            press(LBUTTON);
            return this;
        }

        public DolphinController pressR() {
            press(RBUTTON);
            return this;
        }

        public DolphinController pressZ() {
            press(ZBUTTON);
            return this;
        }

        public DolphinController pressStart() {
            press(STARTBUTTON);
            return this;
        }

        public DolphinController delay(int delay) {
            System.Threading.Thread.Sleep(delay);
            return this;
        }

        private vJoyController pushJoystick(long value, HID_USAGES hid_usage) {
            joystick.SetAxis((int)value, deviceID, hid_usage);
            delay().reset();
            return this;
        }

        private vJoyController press(uint button) {
            joystick.SetBtn(true, deviceID, button);
            delay().reset();
            return this;
        }

        private vJoyController delay() {
            delay(defaultDelay);
            return this;
        }

        private vJoyController reset() {
            joystick.ResetAll();
            return this;
        }
    }
}
