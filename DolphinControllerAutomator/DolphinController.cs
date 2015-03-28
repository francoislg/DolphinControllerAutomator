using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DolphinControllerAutomator {
    public interface DolphinController {
        DolphinController joystickUp();
        DolphinController joystickDown();
        DolphinController joystickLeft();
        DolphinController joystickRight();
        DolphinController press(DolphinPOVButton button);
        DolphinController press(DolphinButton button);
        DolphinController hold(DolphinPOVButton button);
        DolphinController hold(DolphinButton button);
        DolphinController forMilliseconds(int milliseconds);
        DolphinController and();
        DolphinController delay(int delay);
        DolphinController setDelay(int delay);
        
    }
}
