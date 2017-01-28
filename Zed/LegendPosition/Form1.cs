using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace LegendPosition
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			DrawGraph ();
		}


		private double func1 (double x)
		{
			return -x * x + 20;
		}


		private double func2 (double x)
		{
			return -2 * x * x + 10;
		}


		private void DrawGraph ()
		{
			GraphPane pane = zedGraph.GraphPane;

			// Очистим список кривых
			pane.CurveList.Clear ();

			// Создадим списки точек для двух кривых
			PointPairList list1 = new PointPairList ();
			PointPairList list2 = new PointPairList ();

			double xmin = -5;
			double xmax = 5;

			// Заполняем списки точек
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				// Добавим точку в список
				list1.Add (x, func1 (x));
				list2.Add (x, func2 (x));
			}

			// Создадим кривые
			LineItem curve1 = pane.AddCurve ("-x ^ 2 + 20", list1, Color.Blue, SymbolType.None);
			LineItem curve2 = pane.AddCurve ("-2 * x ^ 2 + 10", list2, Color.Black, SymbolType.None);


			// !!!
			// Указываем, что расположение легенды мы будет задавать 
			// в виде координат левого верхнего угла
			pane.Legend.Position = LegendPos.Float;

			// Координаты будут отсчитываться в системе координат окна графика
			pane.Legend.Location.CoordinateFrame = CoordType.ChartFraction;

			// Задаем выравнивание, относительно которого мы будем задавать координаты
			// В данном случае мы будем располагать легенду справа внизу
			pane.Legend.Location.AlignH = AlignH.Right;
			pane.Legend.Location.AlignV = AlignV.Bottom;

			// Задаем координаты легенды 
			// Вычитаем 0.02f, чтобы был небольшой зазор между осями и легендой
			pane.Legend.Location.TopLeft = new PointF (1.0f - 0.02f, 1.0f - 0.02f);			


			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}

}