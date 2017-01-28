using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace LimitAxis
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
			// Получим панель для рисования
			GraphPane pane = zedGraph.GraphPane;

			// Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
			pane.CurveList.Clear ();

			// Создадим список точек
			PointPairList list = new PointPairList ();

			// Интервал, где есть данные
			double xmin = -50;
			double xmax = 50;

			double xmin_limit = -10;
			double xmax_limit = 80;

			double ymin_limit = -1.0;
			double ymax_limit = 1.0;

			// Заполняем список точек
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				list.Add (x, f(x));
			}

			LineItem myCurve = pane.AddCurve ("Sinc", list, Color.Blue, SymbolType.None);


			// !!!
			// Устанавливаем интересующий нас интервал по оси X
			pane.XAxis.Scale.Min = xmin_limit;
			pane.XAxis.Scale.Max = xmax_limit;

			// !!!
			// Устанавливаем интересующий нас интервал по оси Y
			pane.YAxis.Scale.Min = ymin_limit;
			pane.YAxis.Scale.Max = ymax_limit;

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			// В противном случае на рисунке будет показана только часть графика, 
			// которая умещается в интервалы по осям, установленные по умолчанию
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}