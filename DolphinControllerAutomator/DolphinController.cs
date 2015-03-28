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
        DolphinController pressUp();
        DolphinController pressDown();
        DolphinController pressLeft();
        DolphinController pressRight();
        DolphinController pressA();
        DolphinController pressB();
        DolphinController pressX();
        DolphinController pressY();
        DolphinController pressL();
        DolphinController pressR();
        DolphinController pressZ();
        DolphinController pressStart();
        DolphinController delay(int delay);
        DolphinController setDelay(int delay);
        
    }
}
