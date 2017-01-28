using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace SeveralGraphs
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			DrawGraph ();
		}

		private double f1 (double x)
		{
			if (x == 0)
			{
				return 1;
			}

			return Math.Sin (x) / x;
		}

		private double f2 (double x)
		{
			return Math.Sin (x / 2) / 2;
		}

		private void DrawGraph ()
		{
			// Получим панель для рисования
			GraphPane pane = zedGraph.GraphPane;

			// Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
			pane.CurveList.Clear ();

			// Создадим список точек для кривой f1(x)
			PointPairList f1_list = new PointPairList ();

			// Создадим список точек для кривой f2(x)
			PointPairList f2_list = new PointPairList ();

			double xmin = -50;
			double xmax = 50;

			// !!!
			// Заполним массив точек для кривой f1(x)
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				f1_list.Add (x, f1 (x));
			}

			// !!!
			// Заполним массив точек для кривой f2(x)
			// Интервал и шаги по X могут не совпадать на разных кривых
			for (double x = 0; x <= xmax; x += 0.5)
			{
				f2_list.Add (x, f2 (x));
			}

			// !!!
			// Создадим кривую с названием "Sinc", 
			// которая будет рисоваться голубым цветом (Color.Blue),
			// Опорные точки выделяться не будут (SymbolType.None)
			LineItem f1_curve = pane.AddCurve ("Sinc", f1_list, Color.Blue, SymbolType.None);

			// !!!
			// Создадим кривую с названием "Sin", 
			// которая будет рисоваться красным цветом (Color.Red),
			// Опорные точки будут выделяться плюсиками (SymbolType.Plus)
			LineItem f2_curve = pane.AddCurve ("Sin", f2_list, Color.Red, SymbolType.Plus);

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			// В противном случае на рисунке будет показана только часть графика, 
			// которая умещается в интервалы по осям, установленные по умолчанию
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}