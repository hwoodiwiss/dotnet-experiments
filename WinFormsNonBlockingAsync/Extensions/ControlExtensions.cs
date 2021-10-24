using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsNonBlockingAsync.Extensions;

public static class ControlExtensions
{
    public static async Task DisableUntilTaskComplete(this Control control, Task asyncTask, Action? preFireAction = null, Action? postFireAction = null)
    {
        control.Enabled = false;
        preFireAction?.Invoke();
        await asyncTask;
        postFireAction?.Invoke();
        control.Enabled = true;
    }
}
