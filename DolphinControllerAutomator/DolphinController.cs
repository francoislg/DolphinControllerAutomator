
namespace DolphinControllerAutomator {
    public interface DolphinController {
        DolphinController press(DolphinPOVButton button);
        DolphinController press(DolphinButton button);
        DolphinController press(DolphinJoystick joystick);
        DolphinController hold(DolphinPOVButton button);
        DolphinController hold(DolphinButton button);
        DolphinController hold(DolphinJoystick joystick);
        DolphinController forMilliseconds(int milliseconds);
        DolphinController and();
        DolphinController delay(int delay);
        DolphinController setDelay(int delay);
        DolphinController setDelayAfterRelease(int delay);
    }
}
