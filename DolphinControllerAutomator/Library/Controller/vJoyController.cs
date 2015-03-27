using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using vJoyInterfaceWrap;

using DolphinControllerAutomator.Library;
using DolphinControllerAutomator.Library.Controller;

namespace DolphinControllerAutomator.Library.Controllers {
    class vJoyController : DolphinController {
        private vJoy joystick;
        private vJoy.JoystickState joystickState;
        private uint deviceID = 1;
        private int delayMS = 20;
        private long maxYValue;

        public vJoyController(uint deviceID) {
            this.deviceID = deviceID;

            joystick = new vJoy();
            joystickState = new vJoy.JoystickState();

            VjdStat status = joystick.GetVJDStatus(deviceID);
            outputStatus(status);
            verifyVersions();
            acquireTarget(status);
            setMaxValues();
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

        private void setMaxValues() {
            joystick.GetVJDAxisMax(deviceID, HID_USAGES.HID_USAGE_Y, ref maxYValue);
        }

        private void delay() {
            System.Threading.Thread.Sleep(delayMS);
        }

        private void reset() {
            joystick.ResetAll();
        }

        public void setDelayInMS(int delayMS) {
            this.delayMS = delayMS;
        }

        public void joystickUp() {
            joystick.ResetVJD(deviceID);
            joystick.SetAxis((int)maxYValue, deviceID, HID_USAGES.HID_USAGE_Y);
            delay();
            reset();
        }

        public void joystickDown() {
            joystick.ResetVJD(deviceID);
            joystick.SetAxis((int)-maxYValue, deviceID, HID_USAGES.HID_USAGE_Y);
        }

        public void pressA() {
            joystick.SetBtn(true, deviceID, 1);
        }
    }
}
