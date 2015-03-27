using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DolphinControllerAutomator;
using DolphinControllerAutomator.Controllers;

namespace DolphinControllerAutomatorUITester {
    public partial class Tester : Form {
        DolphinController controller;

        public Tester() {
            InitializeComponent();
            controller = new vJoyController(1);
        }

        private void pressUp(object sender, EventArgs e) {
            controller.joystickUp();
        }

        private void pressDown(object sender, EventArgs e) {
            controller.joystickDown();
        }

        private void pressA(object sender, EventArgs e) {
            controller.pressA();
        }

        private void pressB(object sender, EventArgs e) {
            controller.pressB();
        }

        private void pressX(object sender, EventArgs e) {
            controller.pressX();
        }

        private void pressY(object sender, EventArgs e) {
            controller.pressY();
        }

        private void pressLeft(object sender, EventArgs e) {
            controller.joystickLeft();
        }

        private void pressRight(object sender, EventArgs e) {
            controller.joystickRight();
        }

        private void pressStart(object sender, EventArgs e) {
            controller.pressStart();
        }

        private void pressL(object sender, EventArgs e) {
            controller.pressL();
        }

        private void pressR(object sender, EventArgs e) {
            controller.pressR();
        }

        private void pressZ(object sender, EventArgs e) {
            controller.pressZ();
        }
    }
}
