using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinControllerAutomator.Async {
    public class ButtonCommand : AsyncButtonCommand {
        private DolphinButton button;
        private DolphinController controller;

        public ButtonCommand(DolphinController controller, DolphinButton button) {
            this.controller = controller;
            this.button = button;
        }

        public Task execute() {
            return Task.Factory.StartNew(() => controller.hold(button));
        }

        public void release() {
            controller.release(button);
        }
    }
}
