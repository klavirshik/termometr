using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace BoundedRanges
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

			// Создадим список точек
			PointPairList list = new PointPairList ();

			double xmin = -50;
			double xmax = 50;

			// Заполняем список точек
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				list.Add (x, f (x));
			}

			// Создадим кривую
			LineItem myCurve = pane.AddCurve ("", list, Color.Blue, SymbolType.None);

			// Установим такой интервал изменения по оси X, чтобы наибольшие значения графика
			// остались за пределами отображаемой области
			pane.XAxis.Scale.Min = 10;
			pane.XAxis.Scale.Max = 50;

			// По оси Y установим автоматический подбор масштаба
			pane.YAxis.Scale.MinAuto = true;
			pane.YAxis.Scale.MaxAuto = true;

			// !!! Установим значение параметра IsBoundedRanges как true.
			// !!! Это означает, что при автоматическом подборе масштаба 
			// !!! нужно учитывать только видимый интервал графика
			pane.IsBoundedRanges = true;

			// Вызываем метод AxisChange (), чтобы обновить данные об осях.
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}