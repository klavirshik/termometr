using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace ErrorBar
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
			return Math.Sin (x) * Math.Cos (2 * x);
		}


		private void DrawGraph ()
		{
			GraphPane pane = zedGraph.GraphPane;

			// Очистим список кривых
			pane.CurveList.Clear ();

			// Создадим список точек
			PointPairList dataList = new PointPairList ();

			// !!! Создадим список допусков
			PointPairList errorList = new PointPairList ();

			double xmin = 0;
			double xmax = 4 * Math.PI;

			// Величина допуска для всех точек
			double error = 0.1;

			// Заполняем список точек
			for (double x = xmin; x <= xmax; x += 0.3)
			{
				double curry = f (x);

				// Добавим в список точку
				dataList.Add (x, curry);

				// !!! Добавим допуск для этой же точки
				// Первый параметр - координата X,
				// Второй параметр - минимальное значение интервала
				// Третий параметр - максимальное значение интервала
				errorList.Add (x, curry - error, curry + error);
			}

			// Создадим кривую с данными
			LineItem myCurve = pane.AddCurve ("Data", dataList, Color.Blue, SymbolType.Circle);
			myCurve.Symbol.Size = 5.0f;

			// !!! Создадим кривую, отображающую допуски
			ErrorBarItem errorCurve = pane.AddErrorBar ("Error", errorList, Color.Black);

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}