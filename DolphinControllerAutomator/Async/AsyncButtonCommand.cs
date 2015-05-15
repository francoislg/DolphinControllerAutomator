using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DolphinControllerAutomator.Async {
    public interface AsyncButtonCommand : AsyncCommand {
        void release();
    }
}
