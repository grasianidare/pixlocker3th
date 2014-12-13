/*
 * Created by SharpDevelop.
 * User: graco
 * Date: 6/15/2014
 * Time: 7:44 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace pixlocker3th
{
	/// <summary>
	/// Description of WinPixel.
	/// </summary>
	public static class WinPixel
	{
      [DllImport("user32.dll")]
      static extern IntPtr GetDC(IntPtr hwnd);

      [DllImport("user32.dll")]
      static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

      [DllImport("gdi32.dll")]
      static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);
      
      static public System.Drawing.Color GetPixelColor(int x, int y)
      {
       IntPtr hdc = GetDC(IntPtr.Zero);
       uint pixel = GetPixel(hdc, x, y);
       ReleaseDC(IntPtr.Zero, hdc);
       Color color = Color.FromArgb((int)(pixel & 0x000000FF),
                    (int)(pixel & 0x0000FF00) >> 8,
                    (int)(pixel & 0x00FF0000) >> 16);
       return color;
      }		
	}
	
	
}
