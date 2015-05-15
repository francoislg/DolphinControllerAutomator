using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinControllerAutomator.Async {
    public class WaitCommand : AsyncCommand {
        private DolphinController controller;
        private int timeInMilliseconds;
        public WaitCommand(DolphinController controller, int timeInMilliseconds) {
            this.controller = controller;
            this.timeInMilliseconds = timeInMilliseconds;
        }

        public Task execute() {
            return Task.Factory.StartNew(() => controller.delay(timeInMilliseconds));
        }
    }
}
