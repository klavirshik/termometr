using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace LineBreak
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

			// Общий интервал для построения графика
			double xmin = -20;
			double xmax = 20;

			// На интервале (xbreak1; xbreak2) график будет обрываться
			double xbreak1 = -2;
			double xbreak2 = 2;

			// Заполняем первую часть графика до разрыва
			for (double x = xmin; x <= xbreak1; x += 0.01)
			{
				list.Add (x, f (x));
			}

			// Добавим точку разрыва.
			// Формально PointPair.Missing - это значение Double.MaxValue,
			// но лучше всегда использовать именно PointPair.Missing.
			list.Add (PointPairBase.Missing, PointPairBase.Missing);

			// Заполняем вторую часть графика после разрыва
			for (double x = xbreak2; x <= xmax; x += 0.01)
			{
				list.Add (x, f (x));
			}

			// Создадим кривую, в которую входит разрыв
			LineItem myCurve = pane.AddCurve ("", list, Color.Blue, SymbolType.None);

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}