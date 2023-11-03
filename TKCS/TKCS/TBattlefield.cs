using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TKCS
{
    class TBattlefield : System.Windows.Forms.Panel
    {
        private int fieldWidth;
        private int fieldHeight;
        private int fieldAtomSize;
        private byte[,] positions;  //战场站位分割
        private TMilitaryInstallation[,] mapObj;  //地图信息,和战场站位信息保持同步，记录单位内容
        public int FieldWidth { get => fieldWidth; set => fieldWidth = value; } //战场原子尺寸，默认30像素
        public int FieldHeight { get => fieldHeight; set => fieldHeight = value; }
        public int FieldAtomSize { get => fieldAtomSize; set => fieldAtomSize = value; }
        public byte[,] Positions { get => positions; set => positions = value; }

        public TTanK p1Tank;

        //构造函数
        public TBattlefield(System.Windows.Forms.Form fieldParent, int fWidth, int fHeight)
        {
            this.Visible = false;
            FieldWidth = fWidth;
            FieldHeight = fHeight;
            fieldAtomSize = (ushort)constValue.fieldAtomSize;
            structField();
            this.Parent = fieldParent;
            this.Location = new Point(0, 0);
            this.Width = fieldWidth;
            this.Height = FieldHeight;

            this.Visible = true;
        }

        //构造函数
        public TBattlefield(int fWidth, int fHeight, int atomSize)
        {
            FieldWidth = fWidth;
            FieldHeight = fHeight;
            fieldAtomSize = atomSize;
            structField();
        }

        //战场初始化
        private void structField()
        {
            int hCount, vCount;
            hCount = FieldWidth / fieldAtomSize;
            vCount = FieldHeight / fieldAtomSize;

            System.Windows.Forms.MessageBox.Show(hCount.ToString() + " ** " + vCount.ToString());
            positions = new byte[hCount, vCount];
            mapObj = new TMilitaryInstallation[hCount, vCount];
            for (int i = 0; i < hCount; i++)
                for (int j = 0; j < vCount; j++)
                {
                    positions[i, j] = 0;
                    mapObj[i, j] = null;
                }
        }

        //在地图指定位置注册 军事对象
        public bool regObj(ubPointIdx objPosition, TMilitaryInstallation obj)
        {
            bool reBool = true;
            if (mapObj[objPosition.xIdx, objPosition.yIdx] == null)
                mapObj[objPosition.xIdx, objPosition.yIdx] = obj;
            else
                reBool = false;

            return reBool;
        }

        //取消地图指定位置的 军事对象注册
        public bool unRegObj(ubPointIdx objPosition, TMilitaryInstallation obj)
        {
            bool reBool = true;
            if (mapObj[objPosition.xIdx, objPosition.yIdx] != null)
                mapObj[objPosition.xIdx, objPosition.yIdx] = null;
            else
                reBool = false;

            return reBool;
        }

        //摧毁地图指定位置的军事对象
        public bool destroyObj(ubPointIdx objPosition)
        {
            bool reBool = true;
            if (mapObj[objPosition.xIdx, objPosition.yIdx] != null)
                mapObj[objPosition.xIdx, objPosition.yIdx].DestroyObj();
            else
                reBool = false;

            return reBool;
        }

        //返回坐标所处的战场单元
        public ubPointIdx pixToPoint(int pX, int pY)
        {
            ubPointIdx rePtIdx;
            int xIdx = pX / fieldAtomSize;  //计算x坐标所处单元
            int yIdx = pY / fieldAtomSize;  //计算y坐标所处单元
            rePtIdx.xIdx = xIdx;
            rePtIdx.yIdx = yIdx;
            return rePtIdx;
        }

        //计算点位是否处于自己当前位置
        private Boolean isSelfPosition(ubPointIdx tgrIdx, ubPointIdx myIdx,ubSizeIdx mySize)
        {
            Boolean reBool = false;

            if(tgrIdx.xIdx>=myIdx.xIdx && tgrIdx.xIdx<=(myIdx.xIdx+mySize.wIdxSize-1))
                if (tgrIdx.yIdx >= myIdx.yIdx && tgrIdx.yIdx <= (myIdx.yIdx + mySize.hIdxSize-1))
                    reBool = true;

            return reBool;
        }

        //计算点位是否处于自己当前位置
        private Boolean isSelfPosition(int pX, int pY, ubPosition myPosition)
        {
            Boolean reBool;
            ubPointIdx trgIdx,crtIdx;
            ubSizeIdx myIdxSize;

            trgIdx.xIdx = pX / fieldAtomSize;  //计算x坐标所处单元
            trgIdx.yIdx = pY / fieldAtomSize;  //计算y坐标所处单元

            crtIdx.xIdx = myPosition.pX / fieldAtomSize;
            crtIdx.yIdx = myPosition.pY / fieldAtomSize;

            myIdxSize.wIdxSize = myPosition.pWidth / fieldAtomSize - 1;     //计算物体，跨越多少单元
            myIdxSize.hIdxSize = myPosition.pHeight / fieldAtomSize - 1;     //计算物体，跨越多少单元

            reBool = isSelfPosition(trgIdx, crtIdx, myIdxSize);

            return reBool;
        }

        //计算 区域 是否处于自己当前位置
        private Boolean isSelfPosition(ubPosition myPosition, ubPointIdx trgIdx)
        {
            Boolean reBool;

            ubSizeIdx myIdxSize;
            myIdxSize.wIdxSize = myPosition.pWidth / fieldAtomSize - 1;     //计算物体，跨越多少单元
            myIdxSize.hIdxSize = myPosition.pHeight / fieldAtomSize - 1;     //计算物体，跨越多少单元

            ubPointIdx crtIdx;
            crtIdx.xIdx = myPosition.pX / fieldAtomSize;
            crtIdx.yIdx = myPosition.pY / fieldAtomSize;

            reBool = isSelfPosition(trgIdx, crtIdx, myIdxSize);

            return reBool;
        }


        //判断某区域是否可进入，如果可进入则直接更改坐标状态
        public bool enterable(ubPointIdx tgrIdxPs, ubPointIdx crtIdxPs,ubSizeIdx pIdxSize)
        {
            bool reBool = true;

            if ((positions.Length > 0) && (pIdxSize.wIdxSize >= 0) && (pIdxSize.hIdxSize >= 0) &&tgrIdxPs.xIdx>=0 && tgrIdxPs.yIdx>=0)
            {
                for (int i = tgrIdxPs.xIdx; i < (tgrIdxPs.xIdx + pIdxSize.wIdxSize); i++)
                    if (reBool)
                        for (int j = tgrIdxPs.yIdx; j < (tgrIdxPs.yIdx + pIdxSize.hIdxSize); j++)
                            try
                            {
                                if (positions[i, j] == 1)
                                {
                                    ubPointIdx ckPIdx;
                                    ckPIdx.xIdx = i;
                                    ckPIdx.yIdx = j;
                                    if (!isSelfPosition(ckPIdx, crtIdxPs, pIdxSize))
                                    { 
                                        reBool = false;
                                        break;
                                    }
                                }
                            }
                            catch(Exception e)
                            {
                                System.Windows.Forms.MessageBox.Show(e.ToString());
                            }
                    else
                        break;

                if (reBool)
                {
                    fLeave(crtIdxPs, pIdxSize);    //离开原来区域
                    fInto(tgrIdxPs, pIdxSize);//进入新区域
                }

            }
            else
                reBool = false;


            return reBool;
        }


        //判断某区域是否可进入，如果可进入则直接更改坐标状态
        public bool enterable(int pX, int pY, ubPosition pSize)
        {
            bool reBool;

            if(pX>=0 && pY >= 0)
            { 
                ubSizeIdx myIdxSize;
                myIdxSize.wIdxSize = pSize.pWidth / fieldAtomSize;     //计算请求进入的物体，跨越多少单元
                myIdxSize.hIdxSize = pSize.pHeight / fieldAtomSize;

                ubPointIdx crtIdx;
                crtIdx.xIdx = pSize.pX / fieldAtomSize;  //计算x坐标所处单元
                crtIdx.yIdx = pSize.pY / fieldAtomSize;  //计算y坐标所处单元

                ubPointIdx tgrIdxPs;
                tgrIdxPs.xIdx = pX / fieldAtomSize;  //计算x坐标所处单元
                tgrIdxPs.yIdx = pY / fieldAtomSize;  //计算y坐标所处单元

                reBool = enterable(tgrIdxPs, crtIdx, myIdxSize);
            }
            else
                reBool = false;
            return reBool;
        }

        //判断某区域是否可进入，如果可进入则直接更改坐标状态
        public bool enterable(int pX, int pY, int pSizeW,int pSizeH)
        {
            bool reBool;
            if (pX >= 0 && pY >= 0)
            {
                ubSizeIdx myIdxSize;
                myIdxSize.wIdxSize = pSizeW / fieldAtomSize;     //计算请求进入的物体，跨越多少单元
                myIdxSize.hIdxSize = pSizeH / fieldAtomSize;

                ubPointIdx crtIdx;
                crtIdx.xIdx = pX / fieldAtomSize;  //计算x坐标所处单元
                crtIdx.yIdx = pY / fieldAtomSize;  //计算y坐标所处单元

                ubPointIdx tgrIdxPs;
                tgrIdxPs.xIdx = pX / fieldAtomSize;  //计算x坐标所处单元
                tgrIdxPs.yIdx = pY / fieldAtomSize;  //计算y坐标所处单元

                reBool = enterable(tgrIdxPs, crtIdx, myIdxSize);
            }
            else
                reBool = false;

            return reBool;
        }

        //判断某区域是否有对象
        //
        public TMilitaryInstallation haveInstance(ubPointIdx tgrIdx)
        {
            return mapObj[tgrIdx.xIdx, tgrIdx.yIdx];
        }

        //炮弹经过,判断是否摧毁目标
        //参数：目标索引坐标,炮弹方向, 炮弹实例对象
        public bool shellComming(ubPointIdx tgrIdx, int shellDirecter, TMilitaryInstallation crtShell)
        {
            bool reBool = true;
            

            
            //1. 通过炮弹力量等级，判断覆盖的战场面积；计算面积所在区域的对象；
            //2. 通过 haveInstance 函数，返回目标区域函数；

            return reBool;
        }

        //更新区域可用状态
        private void updatePState(ubPointIdx crtP, ubSizeIdx crtSize, byte pState)
        {
            for (int i = crtP.xIdx; i < (crtP.xIdx + crtSize.wIdxSize); i++)
                for (int j = crtP.yIdx; j < (crtP.yIdx + crtSize.hIdxSize); j++)
                    positions[i, j] = pState;
        }

        //进入某区域
        public void fInto(ubPointIdx crtP, ubSizeIdx crtSize)
        {
            updatePState(crtP, crtSize, 1);
        }

        //从某区域 离开 或 消失
        public void fLeave(ubPointIdx crtP,ubSizeIdx crtSize)
        {
            updatePState(crtP, crtSize, 0);
        }
        
        //从某区域 离开 或 消失
        public void fLeave(int pX, int pY, int pSize)
        {
            ubSizeIdx mySize;
            mySize.wIdxSize = pSize / fieldAtomSize;     //计算物体，跨越多少单元
            mySize.hIdxSize = pSize / fieldAtomSize;

            ubPointIdx myP;
            myP.xIdx= pX / fieldAtomSize;  //计算x坐标所处单元
            myP.yIdx = pY / fieldAtomSize;  //计算y坐标所处单元

            fLeave(myP, mySize);
        }
    }

}

//结束