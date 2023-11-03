using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using TKCS;

namespace TKCS
{
    public partial class Form1 : Form
    {
        private TBattlefield crtField;  //战场

        private TTanK luhtTk;           //己方坦克
        private TankShell tkShell;      //己方炮弹

        private TTanK enemyTk;          //敌方坦克
        private TankShell enemyShell;   //敌方炮弹

        private TObstacle obst1;    //1号障碍物

        public Form1()
        {
            InitializeComponent();
        }

        private void myTankMove(TTanK tk,int tkDirection)
        {
            int nextX = -1;
            int nextY = -1;
            switch (tkDirection)
            {
                case 1:
                    nextX = tk.Location.X;
                    nextY = tk.Location.Y - tk.TankPower.objSpeed;break;
                case 2:
                    nextX = tk.Location.X + tk.TankPower.objSpeed;
                    nextY = tk.Location.Y; break;
                case 3:
                    nextX = tk.Location.X;
                    nextY = tk.Location.Y + tk.TankPower.objSpeed; break;
                case 4:
                    nextX = tk.Location.X - tk.TankPower.objSpeed;
                    nextY = tk.Location.Y; break;
            }

            tk.tkChgDir(tkDirection);
            if (crtField.enterable(nextX, nextY, tk.TankPosition))
            {
                crtField.unRegObj(crtField.pixToPoint(tk.Location.X, tk.Location.Y), tk);
                tk.tkMove(tkDirection);
                crtField.regObj(crtField.pixToPoint(nextX, nextY), tk);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("HI");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Width = 1100;
            this.Height = 850;

            if (crtField.enterable(0, 0, 60,60))
            {
                string crtPath = System.Environment.CurrentDirectory;
                string imgPath = crtPath + "\\src\\img\\p1tankU.gif";
                luhtTk = new TTanK(crtField, 0, 0, 0, imgPath);
                crtField.regObj(crtField.pixToPoint(0, 0), luhtTk);

                objInfo tmpInfo = luhtTk.TankInfo;

                tmpInfo.ImgPath_Down = crtPath + "\\src\\img\\p1tankD.gif";
                tmpInfo.ImgPath_Left = crtPath + "\\src\\img\\p1tankL.gif";
                tmpInfo.ImgPath_Right = crtPath + "\\src\\img\\p1tankR.gif";
                tmpInfo.ImgPath_Blast = crtPath + "\\src\\img\\blast6.gif";
                luhtTk.TankInfo = tmpInfo;

                string imgPathH = crtPath + "\\src\\img\\Shell_H.png";
                string imgPathV = crtPath + "\\src\\img\\Shell_V.png";
                luhtTk.setShellSkin(imgPathH, imgPathV);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            myTankMove(luhtTk,1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            myTankMove(luhtTk, 3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            myTankMove(luhtTk, 4);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            myTankMove(luhtTk, 2);
        }

        private void button6_Click(object sender, EventArgs e)
        {            


        }

        private void button7_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(luhtTk.tankShell));
            thread.Start();
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //moveP1(sender, e);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void button6_KeyDown(object sender, KeyEventArgs e)
        {
            //.moveP1(sender, e);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            if (crtField.enterable(0, 0, 60,60))
            { 
                string crtPath = System.Environment.CurrentDirectory;
                string imgPath = crtPath + "\\src\\img\\enemy1U.gif";
                enemyTk = new TTanK(crtField, 0, 0, 0, imgPath);
                crtField.regObj(crtField.pixToPoint(0, 0), enemyTk);

                objInfo tmpInfo = enemyTk.TankInfo;
                tmpInfo.ImgPath_Down = crtPath + "\\src\\img\\enemy1D.gif";
                tmpInfo.ImgPath_Left = crtPath + "\\src\\img\\enemy1L.gif";
                tmpInfo.ImgPath_Right = crtPath + "\\src\\img\\enemy1R.gif";
                tmpInfo.ImgPath_Blast = crtPath + "\\src\\img\\blast6.gif";
                enemyTk.TankInfo = tmpInfo;

                string imgPathH = crtPath + "\\src\\img\\Shell_H.png";
                string imgPathV = crtPath + "\\src\\img\\Shell_V.png";
                enemyTk.setShellSkin(imgPathH, imgPathV);
                Control.CheckForIllegalCrossThreadCalls = false;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            myTankMove(enemyTk, 1);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            myTankMove(enemyTk,3);
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            myTankMove(enemyTk,4);
        }

        private void button10_Click_1(object sender, EventArgs e)
        {
            myTankMove(enemyTk, 2);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(enemyTk.tankShell));
            thread.Start();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string crtPath = System.Environment.CurrentDirectory;
            string imgPath = crtPath + "\\src\\img\\steel.gif";


            if (crtField.enterable(90, 90,30,30))
            { 
                obst1 = new TObstacle(crtField, 90, 90, 0, imgPath);
                crtField.regObj(crtField.pixToPoint(obst1.Location.X, obst1.Location.Y), obst1);
            }
            objInfo tmpInfo = obst1.obsName;
            tmpInfo.ImgPath_Blast = crtPath + "\\src\\img\\blast6.gif";
            obst1.obsName = tmpInfo;


        }

        private void button15_Click(object sender, EventArgs e)
        {
            crtField = new TBattlefield(this, 1280, 800);
            //crtField.KeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler (moveP1);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            TMilitaryInstallation pCls = null;
            TTanK tank = new TTanK(this, 0, 0, 0, System.Environment.CurrentDirectory + "\\src\\img\\steel.gif");
            pCls = tank;

            objInfo tmpInfo = new objInfo();
            tmpInfo.ObjName = "SSSS";
            tank.TankInfo = tmpInfo;
            MessageBox.Show(pCls.OInfo.ObjName);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            crtField.fLeave(90, 90, 30);

            Thread thd= new System.Threading.Thread(new ThreadStart(obst1.DestroyObj));
            thd.Start();

        }
    }
}
