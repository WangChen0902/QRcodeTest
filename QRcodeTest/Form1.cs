//import packages
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

//确定命名空间
namespace QRcodeTest
{
    //Form1类
    public partial class Form1 : Form
    {
        //文件名常量
        public static string FILE_NAME = Environment.CurrentDirectory+"\\temp.png";
        //初始化生成QRCode的函数
        static void InitQRCode(string qrText)
        {
            //新建QrEncoder类的对象
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
            //新建QrCode类的对象，传入形参
            QrCode qrCode = qrEncoder.Encode(qrText);
            //新建GraphicsRenderer类的对象
            GraphicsRenderer render = new GraphicsRenderer(new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);
            //初始化FileStream类的对象，以便生成二维码文件
            using (FileStream stream = new FileStream(FILE_NAME, FileMode.Create))
            {
                //将二维码输出到文件
                render.WriteToStream(qrCode.Matrix, ImageFormat.Png, stream);
            }
        }

        //生成QRCode的函数
        public void DrawQRCode(string qrText)
        {
            //pictureBox1重新生成
            pictureBox1.Refresh();
            //新建QrEncoder类的对象
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.M);
            //新建QrCode类的对象，传入形参
            QrCode qrCode = qrEncoder.Encode(qrText);
            //新建GraphicsRenderer类的对象
            GraphicsRenderer render = new GraphicsRenderer(new FixedModuleSize(5, QuietZoneModules.Two), Brushes.Black, Brushes.White);
            //确定绘图初始点
            Point padding = new Point(0, 0);
            //实例化Graphics类
            Graphics graphics = pictureBox1.CreateGraphics();
            //将graphics对象导入pictureBox1
            render.Draw(graphics, qrCode.Matrix, padding);
        }

        //Form1构造函数，接收arg参数
        public Form1(string arg)
        {
            //初始化内容
            InitializeComponent();
            //重新定义textBox3的文本内容
            textBox3.Text = arg;
            //利用arg参数初始化二维码
            InitQRCode(arg);
            //初始化PictBox1的图片
            pictureBox1.Image = Image.FromFile(FILE_NAME);
        }

        //button1点击事件
        private void button1_Click(object sender, EventArgs e)
        {
            //文本不符合规则
            if (textBox1.Text.Length > 100||textBox1.Text.Length==0)
            {
                //弹出提示窗口
                MessageBox.Show("文本信息不符合规则！");
                //刷新pictureBox1图片
                pictureBox1.Refresh();
            }
            //文本符合规则
            else
            {
                //绘制二维码
                DrawQRCode(textBox1.Text);
            }
        }
    }
}
