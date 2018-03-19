using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace QRcodeTest
{
    public partial class Form1 : Form
    {
        public static string FILE_NAME = Environment.CurrentDirectory+"\\temp.png";
        static void InitQRCode(string qrText)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
            QrCode qrCode = qrEncoder.Encode(qrText);
            GraphicsRenderer render = new GraphicsRenderer(new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);
            using (FileStream stream = new FileStream(FILE_NAME, FileMode.Create))
            {
                render.WriteToStream(qrCode.Matrix, ImageFormat.Png, stream);
            }
        }

        public void DrawQRCode(string qrText)
        {
            pictureBox1.Refresh();
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
            QrCode qrCode = qrEncoder.Encode(qrText);
            GraphicsRenderer render = new GraphicsRenderer(new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);
            Point padding = new Point(0, 0);
            Graphics graphics = pictureBox1.CreateGraphics();
            render.Draw(graphics, qrCode.Matrix, padding);
        }

        public Form1(string arg)
        {
            InitializeComponent();
            this.Text = "QRcode";
            button1.Text = "确定";
            textBox2.Text = "请输入文本(大于0且小于100个字符)：";
            textBox3.Text = arg;
            InitQRCode(arg);
            pictureBox1.Image = Image.FromFile(FILE_NAME);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DrawQRCode(textBox3.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 100||textBox1.Text.Length==0)
            {
                MessageBox.Show("文本信息不符合规则！");
            }
            else
            {
                DrawQRCode(textBox1.Text);
            }
        }

    }
}
