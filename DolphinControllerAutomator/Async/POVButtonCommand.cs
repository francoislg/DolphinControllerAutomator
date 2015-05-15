using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinControllerAutomator.Async {
    public class POVButtonCommand : AsyncButtonCommand {
        private DolphinPOVButton button;
        private DolphinController controller;

        public POVButtonCommand(DolphinController controller, DolphinPOVButton button) {
            this.controller = controller;
            this.button = button;
        }

        public Task execute() {
            return Task.Factory.StartNew(() => controller.hold(button));
        }

        public void release() {
            controller.releasePOV();
        }
    }
}
