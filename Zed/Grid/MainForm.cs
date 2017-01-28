using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace Grid
{
	public partial class MainForm : Form
	{
		public MainForm ()
		{
			InitializeComponent ();

			DrawGraph ();
		}

		private double f (double x)
		{
			if (x == 0)
			{
				return 1;
			}

			return Math.Sin (x) / x;
		}

		private void DrawGraph ()
		{
			GraphPane pane = zedGraph.GraphPane;

			pane.CurveList.Clear ();

			PointPairList list = new PointPairList ();

			double xmin = -50;
			double xmax = 50;

			for (double x = xmin; x <= xmax; x += 0.01)
			{
				list.Add (x, f (x));
			}

			LineItem myCurve = pane.AddCurve ("Sinc", list, Color.Blue, SymbolType.None);

			// !!!
			// Включаем отображение сетки напротив крупных рисок по оси X
			pane.XAxis.MajorGrid.IsVisible = true;
			
			// Задаем вид пунктирной линии для крупных рисок по оси X:
			// Длина штрихов равна 10 пикселям, ... 
			pane.XAxis.MajorGrid.DashOn = 10;

			// затем 5 пикселей - пропуск
			pane.XAxis.MajorGrid.DashOff = 5;


			// Включаем отображение сетки напротив крупных рисок по оси Y
			pane.YAxis.MajorGrid.IsVisible = true;

			// Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
			pane.YAxis.MajorGrid.DashOn = 10;
			pane.YAxis.MajorGrid.DashOff = 5;


			// Включаем отображение сетки напротив мелких рисок по оси X
			pane.YAxis.MinorGrid.IsVisible = true;

			// Задаем вид пунктирной линии для крупных рисок по оси Y: 
			// Длина штрихов равна одному пикселю, ... 
			pane.YAxis.MinorGrid.DashOn = 1;

			// затем 2 пикселя - пропуск
			pane.YAxis.MinorGrid.DashOff = 2;

			// Включаем отображение сетки напротив мелких рисок по оси Y
			pane.XAxis.MinorGrid.IsVisible = true;

			// Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
			pane.XAxis.MinorGrid.DashOn = 1;
			pane.XAxis.MinorGrid.DashOff = 2;

			zedGraph.AxisChange ();

			zedGraph.Invalidate ();
		}
	}
}