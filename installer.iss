#define AppName "Switch Room"
#define AppVersion "1.0.0"
[Setup]
AppId={{7FBB0B88-9A7D-4D58-AC28-72E45A8F6D2C}
AppName={#AppName}
AppVersion={#AppVersion}
DefaultDirName={autopf}\Switch Room
DefaultGroupName=Switch Room
OutputDir=installer-output
OutputBaseFilename=SwitchRoom-Setup-{#AppVersion}
PrivilegesRequired=lowest
MinVersion=6.1
Compression=lzma
SolidCompression=yes
[Files]
Source: "bin\Release\Switch Room.exe"; DestDir: "{app}"; Flags: ignoreversion
[Icons]
Name: "{group}\Switch Room"; Filename: "{app}\Switch Room.exe"
Name: "{userdesktop}\Switch Room"; Filename: "{app}\Switch Room.exe"; Tasks: desktopicon
[Tasks]
Name: "desktopicon"; Description: "Create desktop shortcut"; GroupDescription: "Additional icons:"
[Run]
Filename: "{app}\Switch Room.exe"; Flags: nowait postinstall skipifsilent