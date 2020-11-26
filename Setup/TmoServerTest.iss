; 脚本由 Inno Setup 脚本向导 生成！
; 有关创建 Inno Setup 脚本文件的详细资料请查阅帮助文档！

#define MyAppName "健康管理服务端"
#define MyAppVersion "1.2.6086.23760"
#define MyAppPublisher "百年养生"
#define MyAppExeName "TmoServer.exe"
#define FileDir "..\2-TmoServer\TmoServer\bin\Debug\"
#define SetupDir "..\Setup\"

[Setup]
; 注: AppId的值为单独标识该应用程序。
; 不要为其他安装程序使用相同的AppId值。
; (生成新的GUID，点击 工具|在IDE中生成GUID。)
AppId={{B0903AB3-2E23-4B0B-ABD2-8336B91555A3}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={pf}\TmoServer
DefaultGroupName={#MyAppName}
OutputDir={#SetupDir}Setup
OutputBaseFilename=TmoServerSetupTest
SetupIconFile={#SetupDir}ServerSetup.ico
Compression=lzma
SolidCompression=yes

[Languages]
Name: "chinesesimp"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: checkablealone
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: checkablealone

[Files]
Source: "{#FileDir}TmoServer.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#FileDir}DBBLL.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#FileDir}DBDAL.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#FileDir}DBInterface.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#FileDir}DBModel.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#FileDir}DBUtility.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#FileDir}log4net.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#FileDir}MySql.Data.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#FileDir}Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#FileDir}TmoCommon.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#FileDir}TmoPushData.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#FileDir}TmoRemotingServer.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#FileDir}TmoServer.exe.config"; DestDir: "{app}"; Flags: ignoreversion onlyifdoesntexist
Source: "{#FileDir}TmoTcpServer.dll"; DestDir: "{app}"; Flags: ignoreversion
; 注意: 不要在任何共享系统文件上使用“Flags: ignoreversion”

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent runascurrentuser

[code]
procedure CurUninstallStepChanged(CurUninstallStep: TUninstallStep);
var appWnd:HWND;
begin  appWnd:= FindWindowByWindowName('Tmo服务器');
  if(appWnd<>0) then
    PostMessage(appWnd,18,0,0);
  if CurUninstallStep = usDone then
    DelTree(ExpandConstant('{app}'), True, True, True);
end;