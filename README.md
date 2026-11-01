# Switch 缩放 | Switch Room
版本号暂时保持 **1.0.0**。

## NS1 / NS2 两种界面
- NS1：深色顶部操作栏、青绿色边框、底部操作提示。
- NS2：蓝色边框、底部黑色控制提示栏。
- 在主界面的「界面风格设置」中切换，开启中的放大界面也会同步更新。

## 功能
- 全局快捷键：Ctrl+Alt+M 开关，Ctrl+Alt+= 放大，Ctrl+Alt+- 缩小。
- 方向键移动放大区域，滚轮调整倍率，Esc 退出。
- 可选登录 Windows 后自动启动；最小化后驻留系统托盘。
- 简体中文 / English；.NET Framework 4.0；最低目标 Windows 7。

## 编译与安装
Visual Studio 2010 或更新版本打开 SwitchRoom.sln，选择 Release 编译。使用 Inno Setup 6 打开 installer.iss 编译安装程序。

## 兼容性说明
版本号按要求保持 1.0.0。尚未在真实 Windows 7 设备上完成验证。屏幕捕获采用 CopyFromScreen，采集时会短暂隐藏程序窗口；受保护视频和安全桌面无法捕获。
## GitHub Actions 云端编译
1. 将源码推送到 GitHub 仓库。
2. 打开仓库的 **Actions** 页面，选择 **Build Switch Room**。
3. 点击 **Run workflow**，默认同时编译安装程序。
4. 工作流完成后，在运行记录底部的 **Artifacts** 下载 `SwitchRoom-Windows-1.0.0`。

工作流仅由手动 `workflow_dispatch` 触发，不会因普通 push 自动编译。编译环境使用 Windows Server 2022，并通过 NuGet 获取 .NET Framework 4.0 reference assemblies；生成的程序仍以 .NET Framework 4.0 为目标。Windows 7 实机兼容性仍需实际测试。
