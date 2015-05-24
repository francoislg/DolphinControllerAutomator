using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DolphinControllerAutomator.Async {
    public class GroupCommands : AsyncCommand {
        private DolphinController controller;
        private List<AsyncCommand> commands;

        public GroupCommands(DolphinController controller, List<AsyncCommand> commands) {
            this.controller = controller;
            this.commands = commands;
        }

        public Task execute() {
            return Task.Factory.StartNew(() => executeCommmands());
        }

        private void executeCommmands() {
            Parallel.ForEach(commands, command => command.execute().Wait());
        }
    }
}
