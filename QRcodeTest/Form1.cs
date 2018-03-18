using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QRcodeTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            button1.Text = "确定";
            textBox2.Text = "请输入文本(小于100个字符)：";
            Graphics graphics = pictureBox1.CreateGraphics();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 100)
            {
                MessageBox.Show("文本信息过长！");
            }
            else
            {
                pictureBox1.Refresh();
                QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
                QrCode qrCode = qrEncoder.Encode(textBox1.Text);
                GraphicsRenderer render = new GraphicsRenderer(new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);
                Point padding = new Point(0, 0);
                Graphics graphics = pictureBox1.CreateGraphics();
                render.Draw(graphics, qrCode.Matrix, padding);
            }
        }
    }
}
