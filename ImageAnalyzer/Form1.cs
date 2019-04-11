using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Programmer: Kevin Swarner
 * Date of first version: 3/31/2019
 * Date of latest revision: 4/1/2019
 * 
 * Purpose: To provide an array of image analysis functions and to display the results of each function to the user.
 * 
 * Method: Program ingests an image as provided by the user through the Open File Dialog. Program then assigns
 * pertinent data, such as image width, height, and other metadata, to appropriate fields. The image is then analyzed
 * for pixel color information; that data being stored in an array. The array is then put into a tree where like
 * entries are grouped for quicker access.
 * 
 * TO DO:
 *      -- Develop further functionality to speed up initial breakdown process. Take averages of pixel areas; 8x8, or
 *         16x16 areas (keeping in mind that image dimensions may not be evenly divisible by those values). Average the
 *         values of each of those areas and only insert them into the tree. May require a "range" to be established so
 *         as to not incur too many unique entries. Essentially, intentional color "banding" will improve efficiency.
 *      
 *      -- Develop "Average Color Used" function
 *      
 *      -- Develop "Top Three Colors Used" function
 *      
 *      -- Develop "Complementary Colors" function
 *      
 *      -- Develop "Analogous Colors" function
 *      
 *      -- Develop "Triad Colors" function
 *      
 *      -- Develop "Quad Colors" function
 */

namespace ImageAnalyzer
{
    public partial class Form1 : Form
    {
        private OpenFileDialog loadFile;
        Image newImage = null;
        Image imgDefaultImage;
        Size szImageSize;
        //Size szScaledSize;
        int imageDimensions = 0;
        int checksum = 0;

        //Thread thdAnalyzeImageF = new Thread(AnalyzeImageForward);
        //Thread thdAnalyzeImageL = new Thread(AnalyzeImageBackward);
        //Thread thdAnalyzeImageR = new Thread(AnalyzeImageMiddleOut());
        //Thread thdAnalyzeImageB = new Thread(AnalyzeImageMiddleOut(1));

        //Thread t = new Thread (new ParameterizedThreadStart(myMethod));
        //t.Start(imageDimensions);

        TreeNode colorTree = new TreeNode();

        public StructColors[,] pixelColorRA = new StructColors[0, 0];

        public struct StructColors
        {
            public int R;
            public int G;
            public int B;

            public string strHexValue;
        }



        //public Thread StartTheThread(int param1, ref int param2)
        //{
        //    var t = new Thread(() => RealStart(param1, param2));
        //    t.Start();
        //    return t;
        //}

        //private static void RealStart(SomeType param1, ref int param2)
        //{
        //}
  
        //private static void AnalyzeImageForward ()
        //{

        //}
        //private static void AnalyzeImageBackward()
        //{

        //}

        //private static void AnalyzeImageMiddleOut(int direction)
        //{

        //}

        public Form1()
        {
            InitializeComponent();

            imgDefaultImage = picbxImage.Image;

            loadFile = new OpenFileDialog();
            {
                loadFile.Filter = "Image files (*.bmp)|*.bmp|(*.jpg)|*.jpg";
                loadFile.Title = "Open image file";
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DoTheThings();
        }

        private void DoTheThings()
        {
            int mostIndexedColor = 0;
            string hexString = "";

            StructColors col = new StructColors();
            //Thread thdIndexForward = new Thread(IndexPixelColors);
            //Thread thdIndexBackward = new Thread(IndexPixelColors);

            loadImage();

            if (newImage != null)
            {
                IndexPixelColors(ref pixelColorRA);

                //pixelColorRA = new StructColors[newImage.Height, newImage.Width];

                //thdIndexForward.Start(1);
                //thdIndexBackward.Start(-1);

                //while (checksum != 2)
                //{

                //}

                BuildColorTree(ref colorTree, pixelColorRA);
                CompareColorIndex(colorTree, ref mostIndexedColor, ref hexString);

                col = PopulateStructFromHex(hexString);

                txtMostIndexedColor.Text = "#" + hexString;
                txtTimesIndexed.Text = mostIndexedColor.ToString();
                createSwatch(col);
            }

            else
            {
                lblStatusLabel.Text = "Warning: No image selected!";
            }
        }

        private void createSwatch(StructColors colorInfo)
        {
            Bitmap bmp = new Bitmap(96, 96);
            Graphics gfx = Graphics.FromImage(bmp);
            SolidBrush brush;

            brush = new SolidBrush(Color.FromArgb(colorInfo.R, colorInfo.G, colorInfo.B));

            gfx.FillRectangle(brush, 0, 0, 96, 96);

            picbxSwatch.Image = bmp;
        }

        private StructColors PopulateStructFromHex(string strHexValue)
        {
            StructColors col = new StructColors();

            col.R = 16 * ParseHexValue(strHexValue[0]) + ParseHexValue(strHexValue[1]);
            col.G = 16 * ParseHexValue(strHexValue[2]) + ParseHexValue(strHexValue[3]);
            col.B = 16 * ParseHexValue(strHexValue[4]) + ParseHexValue(strHexValue[5]);

            col.strHexValue = strHexValue;

            return col;
        }

        private int ParseHexValue(char chrHex)
        {
            if (chrHex >= '0' && chrHex <= '9')
            {
                return chrHex - 48;
            }

            else if (chrHex >= 65 && chrHex <= 70)
            {
                return chrHex - 55;
            }

            return 0;
        }

        private void picbxImage_Click(object sender, EventArgs e)
        {
            FormReset();

            DoTheThings();
        }

        private void picbxSwatch_Click(object sender, EventArgs e)
        {
            if (txtMostIndexedColor.Text != "")
            {
                Clipboard.SetText(txtMostIndexedColor.Text);

                lblStatusLabel.Text = "Hex value copied to clipboard.";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            FormReset();
        }

        private void FormReset()
        {
            //CLEAR ALL FIELDS
            txtHeight.Text = "";
            txtWidth.Text = "";
            txtTimesIndexed.Text = "";
            txtMostIndexedColor.Text = "";

            lblMessageLabel.Text = "";
            lblStatusLabel.Text = "Form cleared.";

            if(picbxImage.Image != null)
            {
                picbxImage.Image.Dispose();
            }
            
            picbxImage.Image = imgDefaultImage;
            picbxSwatch.Image = imgDefaultImage;

            newImage = null;
        }

        private void loadImage()
        {
            if (loadFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filePath = loadFile.FileName;
                    newImage = Image.FromFile(filePath);

                    picbxImage.Image = newImage;
                    picbxImage.SizeMode = PictureBoxSizeMode.Zoom;

                    szImageSize = new Size(newImage.Width, newImage.Height);

                    imageDimensions = newImage.Width * newImage.Height;

                    lblMessageLabel.Text = filePath;
                }

                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void BuildColorTree(ref TreeNode colorTree, StructColors[,] pixelRA)
        {
            for (int loopCountH = 0; loopCountH < newImage.Height; loopCountH++)
            {
                for (int loopCountW = 0; loopCountW < newImage.Width; loopCountW++)
                {
                    colorTree.AddChild(pixelRA[loopCountH, loopCountW].strHexValue);
                }
            }
        }

        private void CompareColorIndex(TreeNode lclColorTree, ref int greatestCount, ref string strHexVal)
        {
            if (lclColorTree.less != null)
            {
                CompareColorIndex(lclColorTree.less, ref greatestCount, ref strHexVal);
            }

            if (lclColorTree.GetInstanceCount() > greatestCount)
            {
                greatestCount = lclColorTree.GetInstanceCount();
                strHexVal = lclColorTree.GetLocalData();
            }

            if (lclColorTree.greater != null)
            {
                CompareColorIndex(lclColorTree.greater, ref greatestCount, ref strHexVal);
            }
        }

        private void IndexPixelColors(ref StructColors[,] clrLocalColorRA)
        {
            clrLocalColorRA = new StructColors[newImage.Height, newImage.Width];
            Bitmap bmp = new Bitmap(newImage);

            for (int loopCountH = 0; loopCountH < newImage.Height; loopCountH++)
            {
                for (int loopCountW = 0; loopCountW < newImage.Width; loopCountW++)
                {
                    Color pixel = bmp.GetPixel(loopCountW, loopCountH);
                    clrLocalColorRA[loopCountH, loopCountW].R = pixel.R;
                    clrLocalColorRA[loopCountH, loopCountW].G = pixel.G;
                    clrLocalColorRA[loopCountH, loopCountW].B = pixel.B;

                    clrLocalColorRA[loopCountH, loopCountW].strHexValue = string.Format("{0:X2}{1:X2}{2:X2}", pixel.R, pixel.G, pixel.B);
                }
            }
        }

        //private void IndexPixelColors(object data)
        //{
        //    int loopCountW;
        //    int loopCountH;

        //    int direction = int.Parse(data.ToString());

        //    if (direction > 0)
        //    {
        //        loopCountH = 0;
        //        loopCountW = 0;
        //    }

        //    else
        //    {
        //        loopCountH = newImage.Height-1;
        //        loopCountW = newImage.Width-1;
        //    }

        //    Bitmap bmp = new Bitmap(newImage);

        //    while (loopCountH != newImage.Height/2)
        //    {
        //        while (loopCountW != newImage.Width/2)
        //        {
        //            Color pixel = bmp.GetPixel(loopCountW, loopCountH);
        //            pixelColorRA[loopCountH, loopCountW].R = pixel.R;
        //            pixelColorRA[loopCountH, loopCountW].G = pixel.G;
        //            pixelColorRA[loopCountH, loopCountW].B = pixel.B;

        //            pixelColorRA[loopCountH, loopCountW].strHexValue = string.Format("{0:X2}{1:X2}{2:X2}", pixel.R, pixel.G, pixel.B);

        //            loopCountW += direction;
        //        }
        //        loopCountH += direction;
        //    }

        //    checksum++;
        //}

        public int determineImageScaleFactor()
        {
            int height = newImage.Height;
            int width = newImage.Width;

            int scaleFactor = height / picbxImage.Height;

            return scaleFactor;
        }
    }

    //szScaledSize = new Size(newImage.Width / scaleFactor, newImage.Height / scaleFactor);
}