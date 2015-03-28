using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vJoyInterfaceWrap;

using DolphinControllerAutomator;

namespace DolphinControllerAutomator.Controllers {
    public class vJoyController : DolphinController {
        private int DEFAULTDELAY = 50;

        private vJoy joystick;
        private vJoy.JoystickState joystickState;
        private uint deviceID = 1;
        private int currentDelay;
        private int currentHalfDelay;
        private long minYValue;
        private long maxYValue;
        private long minXValue;
        private long maxXValue;
        private enum Buttons {
            A = 1,
            B = 2,
            X = 3,
            Y = 4,
            L = 5,
            R = 6,
            Z = 7,
            START = 8
        }
        private enum POVButtons {
            UP = 0,
            RIGHT = 1,
            DOWN = 2,
            LEFT = 3,
            NIL = -1
        }

        public vJoyController(uint deviceID) {
            this.deviceID = deviceID;

            joystick = new vJoy();
            joystickState = new vJoy.JoystickState();

            VjdStat status = joystick.GetVJDStatus(deviceID);
            outputStatus(status);
            verifyVersions();
            acquireTarget(status);
            setJoystickBounds();
            setDelay(DEFAULTDELAY);
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
            currentDelay = delay;
            currentHalfDelay = delay / 2;
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
            press(Buttons.A);
            return this;
        }

        public DolphinController pressB() {
            press(Buttons.B);
            return this;
        }

        public DolphinController pressX() {
            press(Buttons.X);
            return this;
        }

        public DolphinController pressY() {
            press(Buttons.Y);
            return this;
        }

        public DolphinController pressL() {
            press(Buttons.L);
            return this;
        }

        public DolphinController pressR() {
            press(Buttons.R);
            return this;
        }

        public DolphinController pressZ() {
            press(Buttons.Z);
            return this;
        }

        public DolphinController pressStart() {
            press(Buttons.START);
            return this;
        }

        public DolphinController pressUp() {
            pressPOV(POVButtons.UP);
            return this;
        }

        public DolphinController pressDown() {
            pressPOV(POVButtons.DOWN);
            return this;
        }

        public DolphinController pressLeft() {
            pressPOV(POVButtons.LEFT);
            return this;
        }

        public DolphinController pressRight() {
            pressPOV(POVButtons.RIGHT);
            return this;
        }

        public DolphinController delay(int delay) {
            System.Threading.Thread.Sleep(delay);
            return this;
        }

        private vJoyController pushJoystick(long value, HID_USAGES hid_usage) {
            joystick.SetAxis((int)value, deviceID, hid_usage);
            halfDelay().reset().halfDelay();
            return this;
        }

        private vJoyController press(Buttons button) {
            joystick.SetBtn(true, deviceID, (uint)button);
            halfDelay().reset().halfDelay();
            return this;
        }

        private vJoyController pressPOV(POVButtons pov) {
            joystick.SetDiscPov((int)pov, deviceID, 1);
            halfDelay().reset().halfDelay();
            return this;
        }

        private vJoyController halfDelay() {
            delay(currentDelay);
            return this;
        }

        private vJoyController delay() {
            delay(currentDelay);
            return this;
        }

        private vJoyController reset() {
            joystick.ResetAll();
            return this;
        }
    }
}
