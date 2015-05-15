using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinControllerAutomator.Async {
    public class JoystickCommand : AsyncButtonCommand {
        private DolphinJoystick joystick;
        private DolphinController controller;

        public JoystickCommand(DolphinController controller, DolphinJoystick joystick) {
            this.controller = controller;
            this.joystick = joystick;
        }

        public Task execute() {
            return Task.Factory.StartNew(() => controller.hold(joystick));
        }

        public void release() {
            controller.release(joystick);
        }
    }
}
