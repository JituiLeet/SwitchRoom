# Switch 放大镜 | Switch Magnifier

一个面向 Windows 桌面、灵感来自 Nintendo Switch 放大镜功能的屏幕局部放大工具。支持简体中文与 English，最低目标为 Windows 7。

A Windows desktop magnifier inspired by the Nintendo Switch magnifier feature. Supports Simplified Chinese and English, targeting Windows 7 and later.

## 功能 / Features

- 桌面局部放大，放大倍率 1.5×–5.0× / Magnify a desktop region at 1.5×–5.0×
- 放大镜窗口可拖动 / Draggable magnifier window
- 鼠标滚轮调整倍率 / Change zoom with the mouse wheel
- 简体中文与 English 界面切换 / Simplified Chinese and English UI
- Esc 关闭放大镜 / Press Esc to close the magnifier
- 目标框架：.NET Framework 4.0 / Target framework: .NET Framework 4.0

## 编译 / Build

1. 使用 Visual Studio 2010 或更新版本打开 `SwitchMagnifier.sln`。
2. 选择 `Release` 配置并编译。
3. 运行生成的 `SwitchMagnifier.exe`。目标电脑需要安装 .NET Framework 4.0 或更高版本。

Open `SwitchMagnifier.sln` in Visual Studio 2010 or later, build the **Release** configuration, then run `SwitchMagnifier.exe`. The target computer needs .NET Framework 4.0 or later.

## 当前版本说明 / Notes

这是初始源码项目。屏幕采集使用 Windows Forms 的 `CopyFromScreen`，不同显卡、远程桌面、UAC 安全桌面及多显示器配置下的行为可能不同。当前实现为基础原型，尚未包含安装程序、系统级快捷键、开机启动或自动更新。
