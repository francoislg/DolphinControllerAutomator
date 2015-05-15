using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinControllerAutomator.Async {
    public class ConcurentButtonsCommands : AsyncCommand {
        private DolphinController controller;
        private int holdTimeInMilliseconds;
        private List<AsyncButtonCommand> commands;

        public ConcurentButtonsCommands(DolphinController controller, List<AsyncButtonCommand> commands, int holdTimeInMilliseconds) {
            this.controller = controller;
            this.commands = commands;
            this.holdTimeInMilliseconds = holdTimeInMilliseconds;
        }

        public Task execute() {
            return Task.Factory.StartNew(() => executeCommmands());
        }

        private void executeCommmands() {
            commands.ForEach(command => command.execute());
            controller.delay(holdTimeInMilliseconds);
            commands.ForEach(command => command.release());
        }
    }
}
