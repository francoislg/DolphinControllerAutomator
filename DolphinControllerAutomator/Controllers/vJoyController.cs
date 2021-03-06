﻿namespace DolphinControllerAutomator.Controllers {
    using System;
    using vJoyInterfaceWrap;

    public class vJoyController : DolphinController {
        private int DEFAULTDELAY = 50;
        private int DEFAULTDELAYAFTERRELEASE = 50;

        private vJoy joystick;
        private vJoy.JoystickState joystickState;
        private uint deviceID = 1;
        private int currentDelay;
        private int currentDelayAfterRelease;
        private long minYValue;
        private long maxYValue;
        private long minXValue;
        private long maxXValue;

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
            setDelayAfterRelease(DEFAULTDELAYAFTERRELEASE);
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
            return this;
        }

        public DolphinController setDelayAfterRelease(int delay) {
            currentDelayAfterRelease = delay;
            return this;
        }

        public DolphinController delay(int delay) {
            System.Threading.Thread.Sleep(delay);
            return this;
        }

        public DolphinController hold(DolphinJoystick joystick) {
            switch (joystick) {
                case DolphinJoystick.UP:
                    pushJoystick(maxYValue, HID_USAGES.HID_USAGE_Y);
                    break;
                case DolphinJoystick.DOWN:
                    pushJoystick(minYValue, HID_USAGES.HID_USAGE_Y);
                    break;
                case DolphinJoystick.LEFT:
                    pushJoystick(minXValue, HID_USAGES.HID_USAGE_X);
                    break;
                case DolphinJoystick.RIGHT:
                    pushJoystick(maxXValue, HID_USAGES.HID_USAGE_X);
                    break;
            }
            return this;
        }

        public DolphinController hold(DolphinButton button) {
            joystick.SetBtn(true, deviceID, (uint)button);
            return this;
        }

        public DolphinController hold(DolphinPOVButton button) {
            joystick.SetDiscPov((int)button, deviceID, 1);
            return this;
        }

        public DolphinController forMilliseconds(int milliseconds) {
            delay(milliseconds);
            releaseAll();
            delay(currentDelayAfterRelease);
            return this;
        }

        public DolphinController press(DolphinButton button) {
            hold(button).forMilliseconds(currentDelay);
            return this;
        }

        public DolphinController press(DolphinPOVButton button) {
            hold(button).forMilliseconds(currentDelay);
            return this;
        }

        public DolphinController press(DolphinJoystick joystick) {
            hold(joystick).forMilliseconds(currentDelay);
            return this;
        }

        public DolphinController release(DolphinButton button) {
            joystick.SetBtn(false, deviceID, (uint)button);
            return this;
        }

        public DolphinController releasePOV() {
            joystick.SetDiscPov((int)DolphinPOVButton.NIL, deviceID, 1);
            return this;
        }

        public DolphinController release(DolphinJoystick joystick) {
            switch (joystick) {
                case DolphinJoystick.UP:
                case DolphinJoystick.DOWN:
                    releaseVerticalJoystick();
                    break;
                case DolphinJoystick.LEFT:
                case DolphinJoystick.RIGHT:
                    releaseHorizontalJoystick();
                    break;
            }
            return this;
        }

        private void releaseHorizontalJoystick() {
            pushJoystick((maxXValue + minXValue) / 2, HID_USAGES.HID_USAGE_X);
        }

        private void releaseVerticalJoystick() {
            pushJoystick((maxYValue + minYValue) / 2, HID_USAGES.HID_USAGE_Y);
        }

        public DolphinController and() {
            return this;
        }

        public DolphinController releaseAll() {
            joystick.ResetButtons(deviceID);
            joystick.ResetPovs(deviceID);
            releaseHorizontalJoystick();
            releaseVerticalJoystick();
            return this;
        }

        private vJoyController pushJoystick(long value, HID_USAGES hid_usage) {
            joystick.SetAxis((int)value, deviceID, hid_usage);
            return this;
        }

        private vJoyController delay() {
            delay(currentDelay);
            return this;
        }
    }
}
