using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace StackLine
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			DrawGraph ();
		}

		private double f (double x, double h)
		{
			return Math.Sin (x) * Math.Cos (2.0 * x) + h;
		}

		private void DrawGraph ()
		{
			// Получим панель для рисования
			GraphPane pane = zedGraph.GraphPane;

			// Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
			pane.CurveList.Clear ();

			// Список точек для центрального графика
			PointPairList list_center = new PointPairList ();

			// Списки точек, для графиков, отсчитываемых относительно других графиков
			PointPairList list_top = new PointPairList ();
			PointPairList list_bottom = new PointPairList ();

			double xmin = 0;
			double xmax = 3 * Math.PI;

			// Заполняем список точек
			for (double x = xmin; x <= xmax; x += 0.2)
			{
				// Точки для основного (центрального) графика
				list_center.Add (x, f (x, 5));

				// Точки для нижнего графика. 
				// Его координаты отсчитываются относительно предыдущего графика
				list_bottom.Add (x, -2);

				// Точки для верхнего графика. 
				// Его координаты отсчитываются относительно предыдущего графика,
				// т.е. относительно нижнего.
				list_top.Add (x, 4);				
			}

			// Добавим кривые
			// Порядок добавления кривых важен, 
			// т.к. мы используем относительные координаты

			// Сначала добавим центральный график
			LineItem curve_center = pane.AddCurve ("Center Line", list_center, 
				Color.Black, SymbolType.None);

			// Теперь нижний график,
			// координаты которого отсчитываются относительно центрального графика
			LineItem curve_bottom = pane.AddCurve ("Bottom Line", list_bottom, 
				Color.Black, SymbolType.Circle);

			// Верхний график, 
			// координаты которого отсчитываются относительно нижнего графика
			LineItem curve_top = pane.AddCurve ("Top Line", list_top, 
				Color.Black, SymbolType.Star);

			// Установим заливку для верхней кривой
			curve_top.Line.Fill = new ZedGraph.Fill (Color.Yellow, Color.White, 90.0f);

			// Укажем, что мы используем относительные координаты
			pane.LineType = LineType.Stack;
			
			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}