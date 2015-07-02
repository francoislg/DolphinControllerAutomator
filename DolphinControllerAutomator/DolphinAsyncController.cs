using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DolphinControllerAutomator {
    using Async;
    using System.Threading.Tasks;
    public class DolphinAsyncController {
        private const int DEFAULTWAIT = 100;
        private const int DEFAULTWAITAFTERRELEASE = 100;
        private DolphinController controller;
        private List<AsyncCommand> currentList;
        private List<AsyncButtonCommand> currentButtons;
        private List<AsyncCommand> commandsGroupList;
        private int holdTimeInMilliseconds;
        private int releaseTimeInMilliseconds;

        public DolphinAsyncController(DolphinController controller) : this(controller, DEFAULTWAIT, DEFAULTWAITAFTERRELEASE) { }

        public DolphinAsyncController(DolphinController controller, int defaultHoldTimeInMilliseconds, int defaultReleaseTimeInMilliseconds) {
            this.controller = controller;
            this.controller.releaseAll();
            this.holdTimeInMilliseconds = defaultHoldTimeInMilliseconds;
            this.releaseTimeInMilliseconds = defaultReleaseTimeInMilliseconds;
            this.currentList = new List<AsyncCommand>();
            this.currentButtons = new List<AsyncButtonCommand>();
            this.commandsGroupList = new List<AsyncCommand>();
        }

        public DolphinAsyncController hold(DolphinPOVButton button) {
            currentButtons.Add(new POVButtonCommand(controller, button));
            return this;
        }

        public DolphinAsyncController hold(DolphinButton button) {
            currentButtons.Add(new ButtonCommand(controller, button));
            return this;
        }

        public DolphinAsyncController hold(DolphinJoystick joystick) {
            currentButtons.Add(new JoystickCommand(controller, joystick));
            return this;
        }

        public DolphinAsyncController press(DolphinPOVButton button) {
            hold(button).forMilliseconds(holdTimeInMilliseconds);
            return this;
        }

        public DolphinAsyncController press(DolphinButton button) {
            hold(button).forMilliseconds(holdTimeInMilliseconds);
            return this;
        }

        public DolphinAsyncController press(DolphinJoystick joystick) {
            hold(joystick).forMilliseconds(holdTimeInMilliseconds);
            return this;
        }

        public DolphinAsyncController forMilliseconds(int milliseconds) {
            clearCurrentButtons(milliseconds);
            return this;
        }

        public DolphinAsyncController wait(int milliseconds) {
            currentList.Add(new WaitCommand(controller, milliseconds));
            return this;
        }

        private void clearCurrentButtons(int milliseconds) {
            if (currentButtons.Count > 0) {
                currentList.Add(new ConcurentButtonsCommands(controller, new List<AsyncButtonCommand>(currentButtons), milliseconds));
                currentButtons.Clear();
            }
        }

        public DolphinAsyncController and() {
            return this;
        }

        public DolphinAsyncController then() {
            clearCurrentList();
            return this;
        }

        public Task execute() {
            lock (commandsGroupList) {
                clearCurrentList();
                List<AsyncCommand> list = new List<AsyncCommand>(commandsGroupList);
                commandsGroupList.Clear();
                this.controller.releaseAll();
                return Task.Factory.StartNew(() => executeCommands(list));
            }
        }

        private void clearCurrentList(){
            clearCurrentButtons(holdTimeInMilliseconds);
            commandsGroupList.Add(new GroupCommands(controller, new List<AsyncCommand>(currentList)));
            currentList.Clear();
        }

        private void executeCommands(List<AsyncCommand> list) {
            list.ForEach(action => action.execute().Wait());
            this.controller.releaseAll().delay(releaseTimeInMilliseconds);
        }
    }
}
