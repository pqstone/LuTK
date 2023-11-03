using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKCS
{
    //战场对象的抽象父类，如坦克类，由这个父类派生而来
    public class TMilitaryInstallation : System.Windows.Forms.PictureBox
    {
        private objType insType = objType.None;
        public objType InsType { get => insType; set => insType = value; }

        protected objInfo oInfo;
        public objInfo OInfo { get => oInfo; set => oInfo = value; }

        protected ubPosition oPosition;
        public ubPosition OPosition { get => oPosition; set => oPosition = value; }

        protected objPower oPower;
        //public objPower OPower { get => oPower; set => oPower = value; }

        public TMilitaryInstallation()
        {

            this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
        }

        public void DestroyObj()
        {
            this.Image = System.Drawing.Image.FromFile(OInfo.ImgPath_Blast);
            System.Threading.Thread.Sleep(500);
            this.Dispose();
            GC.Collect();
        }
    }
}
