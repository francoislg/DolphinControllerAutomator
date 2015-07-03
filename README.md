# DolphinControllerAutomator

Wrapper around vJoy to controller Dolphin with a virtual joystick.

## Usage

### Async mode (prefered)

``` C#
DolphinAsyncController controller = new DolphinAsyncController(new vJoyController(ID));
await controller.press(DolphinButton.A).then()
    .wait(1000).then()
    .hold(DolphinJoystick.RIGHT).forMilliseconds(1000).and().hold(DolphinJoystick.DOWN).forMilliseconds(500).then()
    .hold(DolphinButton.B).forMilliseconds(100)
    .execute();
```

Each call is pretty much self-explained. 

"then()" keyword tells to release the input. 

"hold()" let you hold a button or joystick. You have to couple it with "forMilliseconds(ms)" to tell how long you want to hold it. You can hold multiple buttons for the same length with "hold().and().hold().forMilliseconds()"

"execute()" execute the registered list of commands and then release everything.

You can read on async/await here : https://msdn.microsoft.com/en-ca/library/hh191443.aspx

### Sync

API is almost the same, but execute each command blocks the program.

``` C#
vJoyController controller = new vJoyController(ID);
  controller.hold(DolphinButton.B).forMilliseconds(1000);
  controller.press(DolphinButton.A);
```

It is prefered to use the async version since it is very easy to use and doesn't block after each command.
