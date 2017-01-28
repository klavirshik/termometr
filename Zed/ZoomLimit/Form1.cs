using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace ZoomLimit
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			// Подпишемся на сообщение, уведомляющее о том, 
			// что пользователь изменяет масштаб графика
			zedGraph.ZoomEvent += new ZedGraphControl.ZoomEventHandler (zedGraph_ZoomEvent);

			DrawGraph ();
		}

		/// <summary>
		/// Обработчик события при изменении масштаба
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="oldState"></param>
		/// <param name="newState"></param>
		void zedGraph_ZoomEvent (ZedGraphControl sender, ZoomState oldState, ZoomState newState)
		{
			GraphPane pane = sender.GraphPane;

			// Для простоты примера будем ограничивать масштабирование 
			// только в сторону уменьшения размера графика

			// Проверим интервал для каждой оси и 
			// при необходимости скорректируем его

			if (pane.XAxis.Scale.Min <= -100)
			{
				pane.XAxis.Scale.Min = -100;
			}

			if (pane.XAxis.Scale.Max >= 100)
			{
				pane.XAxis.Scale.Max = 100;
			}

			if (pane.YAxis.Scale.Min <= -1)
			{
				pane.YAxis.Scale.Min = -1;
			}

			if (pane.YAxis.Scale.Max >= 2)
			{
				pane.YAxis.Scale.Max = 2;
			}
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

			double xmin = -50;
			double xmax = 50;

			// Заполняем список точек
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				// добавим в список точку
				list.Add (x, f (x));
			}

			// Создадим кривую с названием "Sinc", 
			// которая будет рисоваться голубым цветом (Color.Blue),
			// Опорные точки выделяться не будут (SymbolType.None)
			LineItem myCurve = pane.AddCurve ("Sinc", list, Color.Blue, SymbolType.None);

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			// В противном случае на рисунке будет показана только часть графика, 
			// которая умещается в интервалы по осям, установленные по умолчанию
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}