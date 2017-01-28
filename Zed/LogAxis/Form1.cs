using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace LogAxis
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

			return Math.Abs (Math.Sin (x) / x);
		}

		private void DrawGraph ()
		{
			// Получим панель для рисования
			GraphPane pane = zedGraph.GraphPane;

			// !!!
			// Установим логарифмический тип оси
			pane.YAxis.Type = AxisType.Log;

			// Отключим автоматический подбор масштаба
			pane.YAxis.Scale.MaxAuto = false;
			pane.YAxis.Scale.MinAuto = false;

			// Установим интервал изменения величин по оси от 10^(-3) до 1
			pane.YAxis.Scale.Max = 1.0;
			pane.YAxis.Scale.Min = 1e-3;

			
			// Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
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
			pane.AddCurve ("", list, Color.Blue, SymbolType.None);

			// Вызываем метод AxisChange (), чтобы обновить данные об осях
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}