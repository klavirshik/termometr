using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace VerticalGraph
{
	public partial class Form1 : Form
	{
		public Form1 ()
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

			// Заполняем список точек
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				// добавим в список точку, но меняем местами координаты
				list.Add (f(x), x);
			}

			LineItem myCurve = pane.AddCurve ("Sinc", list, Color.Blue, SymbolType.None);

			// !!!
			// Теперь линия по нулевому уровню должна быть перпендикулярна оси X
			pane.XAxis.MajorGrid.IsZeroLine = true;

			// !!!
			// Линию по нулевому уровню, перпендикулярную оси Y отключаем
			pane.YAxis.MajorGrid.IsZeroLine = false;
			

			// !!!
			// Поменяем названия осей, чтобы еще больше запутать противника :)
			pane.XAxis.Title.Text = "YAxis";
			pane.YAxis.Title.Text = "XAxis";

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			// В противном случае на рисунке будет показана только часть графика, 
			// которая умещается в интервалы по осям, установленные по умолчанию
			zedGraph.AxisChange ();

			zedGraph.Invalidate ();
		}
	}
}