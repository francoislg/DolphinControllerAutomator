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
            int n = commands.Count;
            Task[] tasks = new Task[n];
            for (int i = 0; i < n; i++) {
                tasks[i] = commands[i].execute();
            }
            Task.WaitAll(tasks);
        }
    }
}
