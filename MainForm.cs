/*
 * Created by SharpDevelop.
 * User: Grasiani
 * Date: 06/12/2014
 * Time: 09:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;


namespace pixlocker3th
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		private int lastPixelX;
		private int lastPixelY;
		
		private bool OP_alwaysOnTop;
		private bool OP_calculateMediaPx;
		private int OP_radiusMedia;
		private int OP_refreshRate;
		
		struct MyPoint
		{
			public int x;
			public int y;

			public bool IsValid()
			{
				return ((x >= 0) && (y >= 0) && (x <= (SystemInformation.VirtualScreen.Width -1)) && (y <= (SystemInformation.VirtualScreen.Height -1)));
			}
		} 
		private bool[] mouseLocked;
		private Point[] freezedMouse;
		
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void MainFormLoad(object sender, EventArgs e)
		{
			
			//label2.Text = ConfigurationManager.AppSettings["RadiusMedia"];
			
			mouseLocked = new bool[3];
			freezedMouse = new Point[3];
			lastPixelX = SystemInformation.VirtualScreen.Width -1;
			lastPixelY = SystemInformation.VirtualScreen.Height -1;
			
			OP_alwaysOnTop = Convert.ToBoolean(ConfigurationManager.AppSettings["AlwaysOnTop"]);
			OP_calculateMediaPx = Convert.ToBoolean(ConfigurationManager.AppSettings["CalculateMediaPixel"]);
			OP_radiusMedia = Convert.ToInt32(ConfigurationManager.AppSettings["RadiusMedia"]);
			OP_refreshRate = Convert.ToInt32(ConfigurationManager.AppSettings["RefreshRate"]);
			time2GO.Interval = OP_refreshRate;
			time2GO.Enabled = true;
			this.TopMost = OP_alwaysOnTop;
		}
		void Time2GOTick(object sender, EventArgs e)
		{
			//first
			Point rato = (mouseLocked[0] ? freezedMouse[0] : System.Windows.Forms.Cursor.Position);
			Color pxCor;
			if (OP_calculateMediaPx)
				pxCor = this.SeaOfPixels(rato, OP_radiusMedia);
			else
				pxCor = WinPixel.GetPixelColor(rato.X, rato.Y);
			
			pnCor1st.BackColor = pxCor;
			pnCtrl1st.BackColor = this.ColorfromLum(pxCor.GetBrightness());
			pnR1st.Width = Convert.ToInt32(pxCor.R);
			pnG1st.Width = Convert.ToInt32(pxCor.G);
			pnB1st.Width = Convert.ToInt32(pxCor.B);
			lbR1st.Text = pxCor.R.ToString();
			lbG1st.Text = pxCor.G.ToString();
			lbB1st.Text = pxCor.B.ToString();
			lbH1st.Text = pxCor.GetHue().ToString("N3");
			lbS1st.Text = pxCor.GetSaturation().ToString("N3");
			lbL1st.Text = pxCor.GetBrightness().ToString("N3");
			
			
			//second...
			rato = (mouseLocked[1] ? freezedMouse[1] : System.Windows.Forms.Cursor.Position);
			if (OP_calculateMediaPx)
				pxCor = this.SeaOfPixels(rato, OP_radiusMedia);
			else
				pxCor = WinPixel.GetPixelColor(rato.X, rato.Y);
			
			pnCor2nd.BackColor = pxCor;
			pnCtrl2nd.BackColor = this.ColorfromLum(pxCor.GetBrightness());
			pnR2nd.Width = Convert.ToInt32(pxCor.R);
			pnG2nd.Width = Convert.ToInt32(pxCor.G);
			pnB2nd.Width = Convert.ToInt32(pxCor.B);
			lbR2nd.Text = pxCor.R.ToString();
			lbG2nd.Text = pxCor.G.ToString();
			lbB2nd.Text = pxCor.B.ToString();
			lbH2nd.Text = pxCor.GetHue().ToString("N3");
			lbS2nd.Text = pxCor.GetSaturation().ToString("N3");
			lbL2nd.Text = pxCor.GetBrightness().ToString("N3");
			
			//third
			rato = (mouseLocked[2] ? freezedMouse[2] : System.Windows.Forms.Cursor.Position);
			if (OP_calculateMediaPx)
				pxCor = this.SeaOfPixels(rato, OP_radiusMedia);
			else
				pxCor = WinPixel.GetPixelColor(rato.X, rato.Y);
			
			pnCor3rd.BackColor = pxCor;
			pnCtrl3rd.BackColor = this.ColorfromLum(pxCor.GetBrightness());
			pnR3rd.Width = Convert.ToInt32(pxCor.R);
			pnG3rd.Width = Convert.ToInt32(pxCor.G);
			pnB3rd.Width = Convert.ToInt32(pxCor.B);
			lbR3rd.Text = pxCor.R.ToString();
			lbG3rd.Text = pxCor.G.ToString();
			lbB3rd.Text = pxCor.B.ToString();
			lbH3rd.Text = pxCor.GetHue().ToString("N3");
			lbS3rd.Text = pxCor.GetSaturation().ToString("N3");
			lbL3rd.Text = pxCor.GetBrightness().ToString("N3");
			
		}
		void MainFormKeyDown(object sender, KeyEventArgs e)
		{
			
			if (e.KeyCode == Keys.F2)
			{
				mouseLocked[0] = !mouseLocked[0];
				freezedMouse[0] = System.Windows.Forms.Cursor.Position;
				lbKey1st.Text = "F2 to " + (mouseLocked[0] ? "un" : "") + "lock";
			}
			if (e.KeyCode == Keys.F3)
			{
				mouseLocked[1] = !mouseLocked[1];
				freezedMouse[1] = System.Windows.Forms.Cursor.Position;
				lbKey2nd.Text = "F3 to " + (mouseLocked[1] ? "un" : "") + "lock";
			}
			if (e.KeyCode == Keys.F4)
			{
				mouseLocked[2] = !mouseLocked[2];
				freezedMouse[2] = System.Windows.Forms.Cursor.Position;
				lbKey3rd.Text = "F4 to " + (mouseLocked[2] ? "un" : "") + "lock";
			}
			
		}
		private Color SeaOfPixels(Point position, int radius)
		{
			int length = (radius * 2) + 1; //yeah, I know, but Murphy...
			MyPoint[,] squared = new MyPoint[length, length];
			Color[,] rainbow = new Color[length, length];
			int qtd = 0;
			int x0 = position.X - radius;
			int y0 = position.Y - radius;
			
			//for (int row = (position.X - length); row <= (position.X + length); row++)
			for (int row = 0; row < length; row++)
			{
				for (int col = 0; col < length; col++)
				{
					if (PXisValid(x0+row, y0+col))
					{
						qtd++;
						rainbow[row,col] = WinPixel.GetPixelColor(x0+row, y0+col);
					}
				}
			}
			
			//calculating media...
			int myR = 0;
			int myG = 0;
			int myB = 0;
			for (int row = 0; row < length; row++)
			{
				for (int col = 0; col < length; col++)
				{
					if (PXisValid(x0+row, y0+col)) //only in the pixels I read before
					{
					myR += rainbow[row,col].R;
					myG += rainbow[row,col].G;
					myB += rainbow[row,col].B;
					}
				}
			}
			
			int meR = Convert.ToInt32(myR / qtd);
			int meG = Convert.ToInt32(myG / qtd);
			int meB = Convert.ToInt32(myB / qtd);
				
			return Color.FromArgb(255, meR, meG, meB);
		}
		private bool PXisValid(Point px)
		{
			return this.PXisValid(px.X, px.Y);
		}
		private bool PXisValid(int pxX, int pxY)
		{
			return ((pxX >= 0) && (pxX < lastPixelX) && (pxY >= 0) && (pxY < lastPixelY));
		}
		private Color GetRgb(double r, double g, double b)
        {
            return Color.FromArgb(255, (byte)(r * 255.0), (byte)(g * 255.0), (byte)(b * 255.0));
        }
		private Color ColorfromLum(float lum)
		{
			double h = 0;
			double s = 0;
			double v = lum;
			
            int hi = (int)Math.Floor(h / 60.0) % 6;
            double f = (h / 60.0) - Math.Floor(h / 60.0);

            double p = v * (1.0 - s);
            double q = v * (1.0 - (f * s));
            double t = v * (1.0 - ((1.0 - f) * s));

            Color ret;

            switch (hi)
            {
                case 0:
                    ret = GetRgb(v, t, p);
                    break;
                case 1:
                    ret = GetRgb(q, v, p);
                    break;
                case 2:
                    ret = GetRgb(p, v, t);
                    break;
                case 3:
                    ret = GetRgb(p, q, v);
                    break;
                case 4:
                    ret = GetRgb(t, p, v);
                    break;
                case 5:
                    ret = GetRgb(v, p, q);
                    break;
                default:
                    ret = Color.FromArgb(0xFF, 0x00, 0x00, 0x00);
                    break;
            }
            return ret;	
		}
	} //class
}//namespace