﻿using Windows.System;

namespace Marimo.WindowsCalculator.WinUI3;

public record KeyPress(VirtualKey Key, bool IsCtrlPressed);
