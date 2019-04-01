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
        Image newImage;
        Size szImageSize;
        //Size szScaledSize;
        int imageDimensions = 0;

        TreeNode colorTree = new TreeNode();

        public StructColors[,] pixelColorRA = new StructColors[0,0];

        public struct StructColors
        {
            public int R;
            public int G;
            public int B;

            public string strHexValue;
        }

        public Form1()
        {
            InitializeComponent();

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

            loadImage();
            IndexPixelColors(ref pixelColorRA);
            BuildColorTree(ref colorTree, pixelColorRA);
            CompareColorIndex(colorTree, ref mostIndexedColor, ref hexString);

            txtMostIndexedColor.Text = "#" + hexString + "x";
            txtTimesIndexed.Text = mostIndexedColor.ToString();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DoTheThings();
        }

        private void loadImage()
        {
            if (loadFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filePath = loadFile.FileName;
                    newImage = Image.FromFile(filePath);

                    pictureBox1.Image = newImage;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                    szImageSize = new Size(newImage.Width, newImage.Height);

                    imageDimensions = newImage.Width * newImage.Height;
                }

                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
            }
        }

        private void BuildColorTree(ref TreeNode colorTree, StructColors[,] pixelRA )
        {
            for(int loopCountH = 0; loopCountH < newImage.Height; loopCountH++)
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

        public int determineImageScaleFactor()
        {
            int height = newImage.Height;
            int width = newImage.Width;

            int scaleFactor = height / pictureBox1.Height;

            return scaleFactor;
        }
    }

    //szScaledSize = new Size(newImage.Width / scaleFactor, newImage.Height / scaleFactor);
}
