using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuTK.common
{
    // *************** 公共数据类型
    public enum constValue : ushort
    {
        fieldAtomSize = 30  //战场原子尺寸，默认30像素

    }

    //设施类型
    public enum objType : ushort
    {
        None = 0,
        P1Tank = 1,
        P2Tank = 2,
        enemyTank1 = 10,
        enemyTank2 = 11,
        enemyTank3 = 12,
        Brick = 21,
        Stell = 22,
        OutlierReading = 200
    }

    //名称、编号等基础信息
    public struct objInfo
    {
        private int objIdx;
        private string objName;
        private string objCode;
        private string imgPath;   //图片
        private string imgPath_Up;
        private string imgPath_Down;
        private string imgPath_Left;
        private string imgPath_Right;
        private string imgPath_horizontal;
        private string imgPath_vertical;
        private string imgPath_Blast;
        public int ObjIdx { get => objIdx; set { objIdx = value; } }
        public string ObjName { get => objName; set { objName = value; } }
        public string ObjCode { get => objCode; set { objCode = value; } }
        public string ImgPath { get => imgPath; set { imgPath = value; } }
        public string ImgPath_Up { get => imgPath_Up; set { imgPath_Up = value; } }
        public string ImgPath_Down { get => imgPath_Down; set { imgPath_Down = value; } }
        public string ImgPath_Left { get => imgPath_Left; set { imgPath_Left = value; } }
        public string ImgPath_Right { get => imgPath_Right; set { imgPath_Right = value; } }
        public string ImgPath_horizontal { get => imgPath_horizontal; set { imgPath_horizontal = value; } }
        public string ImgPath_vertical { get => imgPath_vertical; set { imgPath_vertical = value; } }
        public string ImgPath_Blast
        {
            get => imgPath_Blast; set { imgPath_Blast = value; }
        }
    }

    //坐标位置
    public struct ubPosition
    {
        public int pX;
        public int pY;
        public int pZ;
        public int pWidth;  //尺寸 宽度
        public int pHeight; //尺寸 高度
        public int pDirection; //当前坦克方向 1 上，2右， 3 下 4左 顺时针
    }

    //配置信息
    public struct objPower
    {
        public int objType; //坦克类型，1-9……，1 小p坦克,2 油老鼠坦克,3 厚装甲坦克
        public int objLevel;  //等级，从1-5，分别为 1星、2星……
        public int objArmor;    //坦克装甲，从1-5，分别为防御1炮，防御2炮，防御3炮，……
                                //障碍物装甲，从0-5，分别防御0级炮，防御1级炮，防御2级炮，防御3级炮，……
                                //如果是负数 <0,则炮弹可穿过该对象
        public int objSpeed;    //行驶速度，按照像素计算，如15px，40px；
    }

    //以索引位计量单位的位置信息
    public struct ubPointIdx
    {
        public int xIdx;
        public int yIdx;
    }

    //以索引位计量单位的尺寸信息
    public struct ubSizeIdx
    {
        public int wIdxSize;    //横向
        public int hIdxSize;    //纵向
    }
}
