using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKCS
{
    public class TankShell: TMilitaryInstallation
    {
        public objInfo shlName{ get => oInfo; set => oInfo = value; }
        public ubPosition shPosition { get => oPosition; set => oPosition = value; }
        public objPower shPower { get => oPower;set=> oPower = value; }

        public TankShell(System.Windows.Forms.Control parentMap, int px, int py, int pz, string shSkinUrl)
        {
            this.Visible = false;
            objInfo tmpInfo = shlName;
            tmpInfo.ObjIdx = 0;
            tmpInfo.ObjName = "no name";
            tmpInfo.ObjCode = "no code";
            tmpInfo.ImgPath_horizontal = shSkinUrl;
            tmpInfo.ImgPath_vertical = shSkinUrl;
            shlName = tmpInfo;

            ubPosition tmpPos = shPosition;
            tmpPos.pX = px;
            tmpPos.pY = py;
            tmpPos.pZ = pz;
            tmpPos.pDirection = 1;
            shPosition = tmpPos;

            objPower tmpPow = shPower;
            tmpPow.objType = 1;
            tmpPow.objLevel = 1;
            tmpPow.objSpeed = 15;
            shPower = tmpPow;            

            if(shSkinUrl != "")
                this.Image = System.Drawing.Image.FromFile(shSkinUrl);
            this.Parent = parentMap;
            this.Location = new System.Drawing.Point(px, py);
            this.Width = 60;
            this.Height = 60;
            this.Visible = true;
        }

        private void shTransmitOneStep(int shellDirection)
        {
            if (shellDirection != shPosition.pDirection)
            {
                switch (shellDirection)
                {
                    case 1:
                    case 3:
                        this.Image = System.Drawing.Image.FromFile(shlName.ImgPath_vertical); break;
                    case 2:
                    case 4:
                        this.Image = System.Drawing.Image.FromFile(shlName.ImgPath_horizontal); break;
                }
                ubPosition tmpPos = shPosition;
                tmpPos.pDirection = shellDirection;
                shPosition = tmpPos;
            }

            switch (shPosition.pDirection)
            {
                case 1:
                    this.Location = new System.Drawing.Point(this.Location.X, this.Location.Y - shPower.objSpeed); break;
                case 2:
                    this.Location = new System.Drawing.Point(this.Location.X + shPower.objSpeed, this.Location.Y); break;
                case 3:
                    this.Location = new System.Drawing.Point(this.Location.X, this.Location.Y + shPower.objSpeed); break;
                case 4:
                    this.Location = new System.Drawing.Point(this.Location.X - shPower.objSpeed, this.Location.Y); break;
            }
        }

        public void shTransmit(int shellDirection)
        {
            int slpTime = 25;
            this.Visible = true;
            while (this.Location.X>=0 && this.Location.X<= Parent.Width && this.Location.Y>=0 && this.Location.Y<=Parent.Height)
            { 
                this.shTransmitOneStep(shellDirection);
                System.Threading.Thread.Sleep(slpTime);
            }
            this.Visible = false;
        }
    }
}
