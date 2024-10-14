using Windows.System;

namespace Marimo.WindowsCalculator.WinUI3;

public record KeyPress(VirtualKey key, bool IsCtrlPressed);
