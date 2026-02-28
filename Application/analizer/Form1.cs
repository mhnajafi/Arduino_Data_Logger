using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;


namespace analizer
{
    public partial class Form1 : Form
    {
        public Bitmap bitmap1;
        public Graphics ModelGraphic;
        public Pen Liner = new Pen(Color.Red, 1);
        public Pen Lineb = new Pen(Color.Blue, 1);
        public Pen Linew = new Pen(Color.White, 1);
        public Pen Lineg = new Pen(Color.Green, 1);
        
        public Form1()
        {
            InitializeComponent();
        }

        

        int count = 0,lx=0,ly=0,lz=0,cc=0;
        int v1 = 0, v2 = 0, v3 = 0;


        private void Form1_Load(object sender, EventArgs e)
        {


            bitmap1 = new Bitmap(1200, 600, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            ModelGraphic = Graphics.FromImage(bitmap1);
            //ModelGraphic.DrawLine(Linew, 0, 200, 1200, 200);
            //ModelGraphic.DrawLine(Linew, 0, 400, 1200, 400);//dsdfsdfsd

            //ModelGraphic.DrawLine(Lineg, 0, 100, 1200, 100);
            //ModelGraphic.DrawLine(Lineg, 0, 300, 1200, 300);
            //ModelGraphic.DrawLine(Lineg, 0, 500, 1200, 500);
            ModelGraphic.DrawLine(Linew, 0, 120, 1200, 120);
            ModelGraphic.DrawLine(Linew, 0, 240, 1200, 240);
            ModelGraphic.DrawLine(Linew, 0, 360, 1200, 360);
            ModelGraphic.DrawLine(Linew, 0, 480, 1200, 480);


            ModelGraphic.DrawLine(Linew, 1, 1, 1, 599);
            ModelGraphic.DrawLine(Linew, 1, 1, 1200, 0);
            ModelGraphic.DrawLine(Linew, 0, 599, 1199, 599);
            ModelGraphic.DrawLine(Linew, 1199, 599, 1199, 1);
            pictureBox1.Image = bitmap1;


        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
          
        }




        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string s;
            int x=0, y=0, z=0;
            
            s = serialPort1.ReadLine();
        
            var val = s.Split('|');
            try
            {
                v1 = Convert.ToInt32(val[0]);
                v2 = Convert.ToInt32(val[1]);
                v3 = Convert.ToInt32(val[2]);

                x = Convert.ToInt32(v1 / 4.55);
                y = Convert.ToInt32(v2 / 1.8);
                z = Convert.ToInt32(v3 / 90.9);
            }
            catch
            {

            }

            
            this.Invoke(new EventHandler(show));

            if (z > 120) z = 120;
            if (z < 0) z = 0;
            if (y > 120) y = 120;
            if (y < 0) y = 0;
            if (x > 120) x = 120;
            if (x < 0) x = 0;

            ModelGraphic.DrawLine(Liner, count, 120 - lx, count + 1, 120 - x);
            ModelGraphic.DrawLine(Lineb, count, 240 - ly, count + 1, 240 - y);
            ModelGraphic.DrawLine(Lineg, count, 360 - lz, count + 1, 360 - z);

            lx = x;
            ly = y;
            lz = z;

            if (cc == 10)
            {
                pictureBox1.Image = bitmap1;
                cc = 1;
            }
                
            else cc++;

            if (count < 1200) count+=3;
            else
            {
                bitmap1 = new Bitmap(1200, 600, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                ModelGraphic = Graphics.FromImage(bitmap1);
                ModelGraphic.DrawLine(Linew, 0, 120, 1200, 120);
                ModelGraphic.DrawLine(Linew, 0, 240, 1200, 240);
                ModelGraphic.DrawLine(Linew, 0, 360, 1200, 360);
                ModelGraphic.DrawLine(Linew, 0, 480, 1200, 480);

                //ModelGraphic.DrawLine(Lineg, 0, 100, 1200, 100);
                //ModelGraphic.DrawLine(Lineg, 0, 300, 1200, 300);
                //ModelGraphic.DrawLine(Lineg, 0, 500, 1200, 500);

                ModelGraphic.DrawLine(Linew, 1, 1, 1, 599);
                ModelGraphic.DrawLine(Linew, 1, 1, 1200, 0);
                ModelGraphic.DrawLine(Linew, 0, 599, 1199, 599);
                ModelGraphic.DrawLine(Linew, 1199, 599, 1199, 1);
                pictureBox1.Image = bitmap1;
                count = 0;
            }

                

        }

        private void show(object sender, EventArgs e)
        {
            label7.Text = (v1 / 100.00).ToString();
            label8.Text = v2.ToString();
            label9.Text = v3.ToString();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = "COM" + numericUpDown1.Value;
            try
            {
                serialPort1.Open();
                numericUpDown1.Visible = false;
                button2.Visible = false;
                label2.Text = "Connected on COM" + numericUpDown1.Value.ToString();
            }
            catch
            {
                MessageBox.Show("The COM Port dos not exist!!");
                numericUpDown1.Focus();
            }
        }
    }
}

