using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKCS
{
    class TObstacle : TMilitaryInstallation
    {
        public objInfo obsName { get => oInfo; set => oInfo = value; }
        public ubPosition obsPosition;
        public objPower obsPower;

        public TObstacle(System.Windows.Forms.Control parentMap, int px, int py, int pz, string obSkinUrl)
        {
            this.Visible = false;

            objInfo tmpInfo = obsName;
            tmpInfo.ObjIdx = 0;
            tmpInfo.ObjName = "no name";
            tmpInfo.ObjCode = "no code";
            tmpInfo.ImgPath = obSkinUrl;
            obsName = tmpInfo;

            obsPosition.pX = px;
            obsPosition.pY = py;
            obsPosition.pZ = pz;

            obsPower.objType = 1;
            obsPower.objLevel = 1;
            obsPower.objArmor = 0;

            if (obSkinUrl != "")
                this.Image = System.Drawing.Image.FromFile(obSkinUrl);
            this.Parent = parentMap;
            this.Location = new System.Drawing.Point(px, py);
            this.Width = (int)constValue.fieldAtomSize;
            this.Height = (int)constValue.fieldAtomSize;
            this.Visible = true;
        }
    }
}
