﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DolphinControllerAutomatorUITester {
    using System.Diagnostics;
    using DolphinControllerAutomator;
    using DolphinControllerAutomator.Controllers;

    public partial class Tester : Form {
        DolphinController controller;
        DolphinAsyncController asyncController;

        public Tester() {
            InitializeComponent();
            controller = new vJoyController(1);
            asyncController = new DolphinAsyncController(controller);
        }

        private void pressUp(object sender, EventArgs e) {
            controller.press(DolphinJoystick.UP);
        }

        private void pressDown(object sender, EventArgs e) {
            controller.press(DolphinJoystick.DOWN);
        }

        private void pressLeft(object sender, EventArgs e) {
            controller.press(DolphinJoystick.LEFT);
        }

        private void pressRight(object sender, EventArgs e) {
            controller.press(DolphinJoystick.RIGHT);
        }

        private void pressA(object sender, EventArgs e) {
            controller.press(DolphinButton.A);
        }

        private void pressB(object sender, EventArgs e) {
            controller.press(DolphinButton.B);
        }

        private void pressX(object sender, EventArgs e) {
            controller.press(DolphinButton.X);
        }

        private void pressY(object sender, EventArgs e) {
            controller.press(DolphinButton.Y);
        }

        private void pressStart(object sender, EventArgs e) {
            controller.press(DolphinButton.START);
        }

        private void pressL(object sender, EventArgs e) {
            controller.press(DolphinButton.L);
        }

        private void pressR(object sender, EventArgs e) {
            controller.press(DolphinButton.R);
        }

        private void pressZ(object sender, EventArgs e) {
            controller.press(DolphinButton.Z);
        }

        private void downPOVButton_Click(object sender, EventArgs e) {
            controller.press(DolphinPOVButton.DOWN);
        }

        private void rightPOVButton_Click(object sender, EventArgs e) {
            controller.press(DolphinPOVButton.RIGHT);
        }

        private void upPOVButton_Click(object sender, EventArgs e) {
            controller.press(DolphinPOVButton.UP);
        }

        private void leftPOVButton_Click(object sender, EventArgs e) {
            controller.press(DolphinPOVButton.LEFT);
        }

        private async void asyncTestButton_Click(object sender, EventArgs e) {
            asyncController.hold(DolphinJoystick.UP).forMilliseconds(250).then().hold(DolphinJoystick.RIGHT).forMilliseconds(50).then().press(DolphinButton.A);
            await asyncController.execute();
            await asyncController.press(DolphinButton.A).execute();
        }

        private void multipleTest_Click(object sender, EventArgs e) {
            controller.hold(DolphinJoystick.UP).forMilliseconds(250).and().hold(DolphinJoystick.RIGHT).forMilliseconds(50);
        }
    }
}
