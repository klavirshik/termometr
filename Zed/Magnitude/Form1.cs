using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace Magnitude
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			DrawGraph ();
		}

		/// <summary>
		/// Отображаемая функция
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		private double f (double x)
		{
			double ky = 1.0e6;
			double kx = 1.0e9;

			if (x == 0)
			{
				return ky;
			}

			return ky * Math.Sin (x * kx) / (x * kx);
		}

		private void DrawGraph ()
		{
			GraphPane pane = zedGraph.GraphPane;

			pane.CurveList.Clear ();

			// Нарисуем график. По горизонтали у нас будут отложены маленькие значения,
			// а по вертикали - большие
			PointPairList list = new PointPairList ();

			// Интервал изменения координаты X
			double xmin = 0;
			double xmax = 15e-9;

			for (double x = xmin; x <= xmax; x += 20e-12)
			{
				list.Add (x, f(x));
			}

			LineItem myCurve = pane.AddCurve ("", list, Color.Blue, SymbolType.None);

			// !!! Установим параметры отображения больших и маленьких чисел на осях.

			// Параметры оси X
			// !!! Напишем, что по иси X у нас отложено время в нс
			pane.XAxis.Title.Text = "t, нс";

			// !!! Просто уберем отображение степени в подписи оси X
			pane.XAxis.Title.IsOmitMag = true;

			// !!! Сами установим коэффициент, на который умножается значение по оси X
			// !!! В данном случае значение будет умножаться на 10^-9
			pane.XAxis.Scale.Mag = -9;

			// Параметры оси Y
			// !!! Установим коэффициент, на который умножается значение по оси Y 
			// !!! В данном случае значение будет умножаться на 10^0 = 1, то есть умножения не будет
			pane.YAxis.Scale.Mag = 0;

			zedGraph.AxisChange ();
			zedGraph.Invalidate ();
		}
	}
}