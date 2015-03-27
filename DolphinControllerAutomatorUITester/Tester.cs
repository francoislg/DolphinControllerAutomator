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
    }
}
