using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DolphinControllerAutomator.Library;
using DolphinControllerAutomator.Library.Controllers;

namespace DolphinControllerAutomator {
    public partial class Tester : Form {
        DolphinController controller;
        
        public Tester() {
            InitializeComponent();
            controller = new vJoyController(1);
        }

        private void button1_Click(object sender, EventArgs e) {
            controller.joystickUp();
        }

        private void button2_Click(object sender, EventArgs e) {
            controller.joystickDown();
        }

        private void button3_Click(object sender, EventArgs e) {
            controller.pressA();
        }
    }
}
