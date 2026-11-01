using System; using System.Drawing; using System.Windows.Forms; using Microsoft.Win32;
namespace SwitchRoom {
public class MainForm:Form {
 const int HK_TOGGLE=1,HK_PLUS=2,HK_MINUS=3; Button start,settings,lang,plus,minus; Label title,desc,zoomLabel,info; CheckBox startup;
 bool en=false; string skin="NS1"; float zoom=2f; MagnifierForm magnifier; NotifyIcon tray;
 public MainForm(){
 Text="Switch 缩放";ClientSize=new Size(460,350);MinimumSize=new Size(460,350);StartPosition=FormStartPosition.CenterScreen;
 FormBorderStyle=FormBorderStyle.FixedSingle;MaximizeBox=false;BackColor=Color.FromArgb(245,247,250);Font=new Font("Segoe UI",9);
 title=new Label{Left=24,Top=20,Width=330,Height=38,Font=new Font("Segoe UI",21,FontStyle.Bold)};Controls.Add(title);
 lang=new Button{Left=375,Top=24,Width=60,Text="English"};lang.Click+=delegate{en=!en;UpdateUi();};Controls.Add(lang);
 desc=new Label{Left=25,Top=68,Width=410,Height=42};Controls.Add(desc);
 start=new Button{Left=25,Top=120,Width=410,Height=45,BackColor=Color.FromArgb(0,115,200),ForeColor=Color.White,FlatStyle=FlatStyle.Flat};start.FlatAppearance.BorderSize=0;start.Click+=delegate{Toggle();};Controls.Add(start);
 minus=new Button{Left=25,Top=180,Width=55,Height=32,Text="−"};minus.Click+=delegate{SetZoom(zoom-.25f);};Controls.Add(minus);
 zoomLabel=new Label{Left=90,Top=185,Width=280,Height=24,TextAlign=ContentAlignment.MiddleCenter};Controls.Add(zoomLabel);
 plus=new Button{Left=380,Top=180,Width=55,Height=32,Text="+"};plus.Click+=delegate{SetZoom(zoom+.25f);};Controls.Add(plus);
 settings=new Button{Left=25,Top=230,Width=410,Height=34};settings.Click+=delegate{using(SettingsForm f=new SettingsForm(skin,en))if(f.ShowDialog(this)==DialogResult.OK){skin=f.StyleName;if(magnifier!=null&&!magnifier.IsDisposed)magnifier.SetSkin(skin);UpdateUi();}};Controls.Add(settings);
 startup=new CheckBox{Left=25,Top=275,Width=410,Height=24};startup.CheckedChanged+=delegate{try{using(RegistryKey k=Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run",true)){if(k!=null){if(startup.Checked)k.SetValue("SwitchRoom","\""+Application.ExecutablePath+"\"");else k.DeleteValue("SwitchRoom",false);}}}catch{}};Controls.Add(startup);
 info=new Label{Left=25,Top=310,Width=410,Height=30};Controls.Add(info);
 try{using(RegistryKey k=Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run"))startup.Checked=k!=null&&k.GetValue("SwitchRoom")!=null;}catch{}
 tray=new NotifyIcon{Icon=SystemIcons.Information,Text="Switch Room"};ContextMenuStrip menu=new ContextMenuStrip();menu.Items.Add("Open / 打开",null,delegate{Show();WindowState=FormWindowState.Normal;});menu.Items.Add("Exit / 退出",null,delegate{tray.Visible=false;Application.Exit();});tray.ContextMenuStrip=menu;tray.DoubleClick+=delegate{Show();WindowState=FormWindowState.Normal;};
 RegisterHotKey(Handle,HK_TOGGLE,MOD_CONTROL|MOD_ALT,(uint)Keys.M);RegisterHotKey(Handle,HK_PLUS,MOD_CONTROL|MOD_ALT,(uint)Keys.Oemplus);RegisterHotKey(Handle,HK_MINUS,MOD_CONTROL|MOD_ALT,(uint)Keys.OemMinus);UpdateUi();
 }
 const uint MOD_CONTROL=2,MOD_ALT=1;
 void RegisterHotKey(IntPtr h,int id,uint m,uint k){NativeMethods.RegisterHotKey(h,id,m,k);}
 void UpdateUi(){Text=en?"Switch Room":"Switch 缩放";title.Text=Text;lang.Text=en?"中文":"English";desc.Text=en?"Nintendo Switch-inspired desktop magnifier.":"一款参考 Nintendo Switch 缩放功能的桌面工具。";start.Text=magnifier!=null&&!magnifier.IsDisposed&&magnifier.Visible?(en?"Close magnifier":"关闭缩放"):(en?"Start magnifier":"开启缩放");settings.Text=en?"Appearance settings ("+skin+")":"界面风格设置（"+skin+"）";startup.Text=en?"Start with Windows":"开机自动启动";zoomLabel.Text=(en?"Magnification: ":"缩放倍率：")+zoom.ToString("0.00").TrimEnd('0').TrimEnd('.')+"×";info.Text=en?"Global: Ctrl+Alt+M toggle · Ctrl+Alt+=/- zoom · Version 1.0.0":"全局快捷键：Ctrl+Alt+M 开关，Ctrl+Alt+=/- 缩放 · 版本 1.0.0";}
 void SetZoom(float v){zoom=Math.Max(1.25f,Math.Min(6,v));if(magnifier!=null&&!magnifier.IsDisposed)magnifier.Zoom=zoom;UpdateUi();}
 void Toggle(){if(magnifier!=null&&!magnifier.IsDisposed&&magnifier.Visible){magnifier.Close();magnifier=null;}else{magnifier=new MagnifierForm(zoom,skin,en);magnifier.FormClosed+=delegate{UpdateUi();};magnifier.Show();}UpdateUi();}
 protected override void WndProc(ref Message m){if(m.Msg==NativeMethods.WM_HOTKEY){int id=m.WParam.ToInt32();if(id==HK_TOGGLE)Toggle();else if(id==HK_PLUS)SetZoom(zoom+.25f);else if(id==HK_MINUS)SetZoom(zoom-.25f);}base.WndProc(ref m);}
 protected override void OnResize(EventArgs e){base.OnResize(e);if(WindowState==FormWindowState.Minimized){Hide();tray.Visible=true;}}
 protected override void OnFormClosing(FormClosingEventArgs e){NativeMethods.UnregisterHotKey(Handle,HK_TOGGLE);NativeMethods.UnregisterHotKey(Handle,HK_PLUS);NativeMethods.UnregisterHotKey(Handle,HK_MINUS);if(magnifier!=null&&!magnifier.IsDisposed)magnifier.Close();tray.Visible=false;base.OnFormClosing(e);}
} }