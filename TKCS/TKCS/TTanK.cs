using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKCS
{
    public class TTanK : TMilitaryInstallation
    {
        public byte hvGift; //0-6，是否掉装备，0 无；1 五角星；2 防弹衣； 3 炸弹；4 时钟； 5 钢铁老窝； 6 加一条命；……
        public objInfo TankInfo { get => oInfo; set => oInfo = value;  }

        public ubPosition TankPosition{ get => oPosition; set => oPosition = value; }

        public objPower TankPower { get => oPower; set => oPower = value; }

        private TankShell tkShell;

        public void setShellSkin(string skinUrlH, string skinUrlV)
        {
            if (tkShell != null)
            {
                objInfo tmpInfo = tkShell.shlName;
                tmpInfo.ImgPath_horizontal = skinUrlH;
                tmpInfo.ImgPath_vertical = skinUrlV;
                tkShell.shlName = tmpInfo;
            }
        }

        public TTanK(System.Windows.Forms.Control parentMap, int px,int py,int pz,string tkSkinUrl)
        {
            this.Visible = false;

            objInfo tmpObj = new objInfo();
            tmpObj.ObjIdx = 0;
            tmpObj.ObjName = "no name";
            tmpObj.ObjCode = "no code";
            tmpObj.ImgPath_Up = tkSkinUrl;
            tmpObj.ImgPath_Down = tkSkinUrl;
            tmpObj.ImgPath_Left = tkSkinUrl;
            tmpObj.ImgPath_Right = tkSkinUrl;
            TankInfo = tmpObj;

            //string crtPath = System.Environment.CurrentDirectory;
            ubPosition tmpPos = TankPosition;
            tmpPos.pX = px;
            tmpPos.pY = py;
            tmpPos.pZ = pz;
            tmpPos.pDirection = 1;
            tmpPos.pHeight = 60;
            tmpPos.pWidth = 60;
            TankPosition = tmpPos;

            objPower tmpPow = TankPower;
            tmpPow.objType = 1;
            tmpPow.objLevel = 1;
            tmpPow.objArmor = 1;
            tmpPow.objSpeed = 30;
            TankPower = tmpPow;

            this.Image = System.Drawing.Image.FromFile(tkSkinUrl);
            this.Parent = parentMap;
            this.Location = new System.Drawing.Point(px, py);
            this.Width = 60;
            this.Height = 60;
            this.Visible = true;

            //准备炮弹
            tkShell = new TankShell(parentMap, px, py, pz, "");
        }

        //坦克掉头
        public void tkChgDir(int tkDirection)
        {
            ubPosition tmpPos = TankPosition;
            if (tkDirection != TankPosition.pDirection)
            {
                switch (tkDirection)
                {
                    case 1:
                        this.Image = System.Drawing.Image.FromFile(TankInfo.ImgPath_Up); break;
                    case 2:
                        this.Image = System.Drawing.Image.FromFile(TankInfo.ImgPath_Right); break;
                    case 3:
                        this.Image = System.Drawing.Image.FromFile(TankInfo.ImgPath_Down); break;
                    case 4:
                        this.Image = System.Drawing.Image.FromFile(TankInfo.ImgPath_Left); break;
                }
                tmpPos.pDirection = tkDirection;
                TankPosition = tmpPos;
            }
        }

        public void tkMove(int tkDirection)
        {
            ubPosition tmpPos = TankPosition;
            switch (TankPosition.pDirection)
            {
                case 1:
                    this.Location = new System.Drawing.Point(this.Location.X,this.Location.Y-TankPower.objSpeed); break;
                case 2:
                    this.Location = new System.Drawing.Point(this.Location.X + TankPower.objSpeed, this.Location.Y); break;
                case 3:
                    this.Location = new System.Drawing.Point(this.Location.X, this.Location.Y + TankPower.objSpeed); break;
                case 4:
                    this.Location = new System.Drawing.Point(this.Location.X - TankPower.objSpeed, this.Location.Y); break;
            }
            tmpPos.pX = this.Location.X;
            tmpPos.pY = this.Location.Y;
            TankPosition = tmpPos;
        }

        public void tankShell()
        {
            int shiftingPos = 23;
            switch (this.TankPosition.pDirection)
            {
                case 1: tkShell.Location = new System.Drawing.Point(this.Location.X+ shiftingPos, this.Location.Y);break;
                case 2: tkShell.Location = new System.Drawing.Point(this.Location.X, this.Location.Y+ shiftingPos);break;
                case 3: tkShell.Location = new System.Drawing.Point(this.Location.X+ shiftingPos, this.Location.Y);break;
                case 4: tkShell.Location = new System.Drawing.Point(this.Location.X, this.Location.Y+ shiftingPos);break;
            }
           
            tkShell.shTransmit(this.TankPosition.pDirection);
        }

        public void tankShell(string skinUrlH, string skinUrlV)
        {
            objInfo tmpInfo = tkShell.shlName;
            tmpInfo.ImgPath_horizontal = skinUrlH;
            tmpInfo.ImgPath_vertical = skinUrlV;
            tkShell.shlName = tmpInfo;
            int shiftingPos = 23;
            switch (this.TankPosition.pDirection)
            {
                case 1: tkShell.Location = new System.Drawing.Point(this.Location.X + shiftingPos, this.Location.Y); break;
                case 2: tkShell.Location = new System.Drawing.Point(this.Location.X, this.Location.Y + shiftingPos); break;
                case 3: tkShell.Location = new System.Drawing.Point(this.Location.X + shiftingPos, this.Location.Y); break;
                case 4: tkShell.Location = new System.Drawing.Point(this.Location.X, this.Location.Y + shiftingPos); break;
            }

            tkShell.shTransmit(this.TankPosition.pDirection);
        }
    }
}
