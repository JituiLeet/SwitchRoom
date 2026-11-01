using System; using System.Drawing; using System.Windows.Forms;
namespace SwitchRoom {
public class SettingsForm:Form {
 ComboBox style; public string StyleName {get{return style.SelectedIndex==1?"NS2":"NS1";}}
 public SettingsForm(string current,bool en){
 Text=en?"Switch Room Settings":"Switch 缩放设置"; StartPosition=FormStartPosition.CenterParent; FormBorderStyle=FormBorderStyle.FixedDialog;
 ClientSize=new Size(390,180);MaximizeBox=false;MinimizeBox=false;Font=new Font("Segoe UI",10);
 Controls.Add(new Label{Left=22,Top=22,Width=340,Height=28,Text=en?"Magnifier interface style":"放大镜界面风格",Font=new Font("Segoe UI",13,FontStyle.Bold)});
 Controls.Add(new Label{Left=24,Top=65,Width=340,Text=en?"Select a visual style:":"选择界面样式："});
 style=new ComboBox{Left=24,Top=94,Width=340,DropDownStyle=ComboBoxStyle.DropDownList};
 style.Items.Add(en?"NS1 - Green frame":"NS1 - 青绿色边框");style.Items.Add(en?"NS2 - Blue frame":"NS2 - 蓝色边框");style.SelectedIndex=current=="NS2"?1:0;Controls.Add(style);
 Button ok=new Button{Text=en?"OK":"确定",Left=194,Top=135,Width=78,DialogResult=DialogResult.OK};
 Button cancel=new Button{Text=en?"Cancel":"取消",Left=284,Top=135,Width=78,DialogResult=DialogResult.Cancel};Controls.Add(ok);Controls.Add(cancel);AcceptButton=ok;CancelButton=cancel;
} } }