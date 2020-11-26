; 脚本由 Inno Setup 脚本向导 生成！
; 有关创建 Inno Setup 脚本文件的详细资料请查阅帮助文档！

#define MyAppName "移动设备数据同步工具"
#define MyAppVersion "1.0.5814.41331"
#define MyAppPublisher "百年养生"
#define MyAppExeName "MonitorDataTool.exe"
#define FileDir "..\3-MonitorDataTool\MonitorDataTool\bin\x86\Debug\"
#define SetupDir "..\Setup\"

[Setup]
; 注: AppId的值为单独标识该应用程序。
; 不要为其他安装程序使用相同的AppId值。
; (生成新的GUID，点击 工具|在IDE中生成GUID。)
AppId={{AE4CDA27-7509-4B8D-B371-58D311161BE2}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={pf}\DataImportTool
DefaultGroupName={#MyAppName}
OutputDir={#SetupDir}Setup
OutputBaseFilename=DataImportToolsetup
SetupIconFile={#SetupDir}DataImportToolSetup.ico
Compression=lzma
SolidCompression=yes

[Languages]
Name: "chinesesimp"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: checkablealone
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: checkablealone

[Files]
Source: "{#FileDir}MonitorDataTool.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#FileDir}EnvLib.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#FileDir}SyncDataLib.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#FileDir}TmoCommon.dll"; DestDir: "{app}"; Flags: ignoreversion 
Source: "{#FileDir}log4net.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#FileDir}TmoSkin.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#FileDir}Update.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#FileDir}WebService_MonitorService.dll"; DestDir: "{app}"; Flags: ignoreversion
; 注意: 不要在任何共享系统文件上使用“Flags: ignoreversion”

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\Update.exe"; Description: "初始化程序"; StatusMsg: "建立软件运行环境"
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent runascurrentuser

[code]procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
var appWnd:HWND;
begin
  appWnd:= FindWindowByWindowName('移动设备数据同步工具');
  if(appWnd<>0) then
    PostMessage(appWnd,18,0,0);
  if CurUninstallStep = usDone then
    DelTree(ExpandConstant('{app}'), True, True, True);
end;