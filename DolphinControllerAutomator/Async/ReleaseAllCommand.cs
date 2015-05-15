using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinControllerAutomator.Async {
    public class ReleaseAllCommand : AsyncCommand {
        private DolphinController controller;
        private int delayInMilliseconds;

        public ReleaseAllCommand(DolphinController controller, int delayAfterReleaseInMilliseconds) {
            this.controller = controller;
            this.delayInMilliseconds = delayAfterReleaseInMilliseconds;
        }

        public Task execute() {
            return Task.Factory.StartNew(() => controller.releaseAll().delay(delayInMilliseconds));
        }
    }
}
