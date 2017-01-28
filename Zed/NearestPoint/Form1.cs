using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace NearestPoint
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			DrawGraph ();
		}


		/// <summary>
		/// Обработка события MouseClick - клик по графику
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void zedGraph_MouseClick (object sender, MouseEventArgs e)
		{
			// Сюда будет сохранена кривая, рядом с которой был произведен клик
			CurveItem curve;

			// Сюда будет сохранен номер точки кривой, ближайшей к точке клика
			int index;

			GraphPane pane = zedGraph.GraphPane;

			// Максимальное расстояние от точки клика до кривой в пикселях, 
			// при котором еще считается, что клик попал в окрестность кривой.
			GraphPane.Default.NearestTol = 10;

			bool result = pane.FindNearestPoint (e.Location, out curve, out index);

			if (result)
			{
				// Максимально расстояние от точки клика до кривой не превысило NearestTol

				// Добавим точку на график, вблизи которой произошел клик
				PointPairList point = new PointPairList ();

				point.Add (curve[index]);

				// Кривая, состоящая из одной точки. Точка будет отмечена синим кругом
				LineItem curvePount = pane.AddCurve ("",
					new double[] { curve[index].X },
					new double[] { curve[index].Y },
					Color.Blue,
					SymbolType.Circle);

				// 
				curvePount.Line.IsVisible = false;

				// Цвет заполнения круга - колубой
				curvePount.Symbol.Fill.Color = Color.Blue;

				// Тип заполнения - сплошная заливка
				curvePount.Symbol.Fill.Type = FillType.Solid;

				// Размер круга
				curvePount.Symbol.Size = 7;
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

			// Включим отображение сетки
			pane.XAxis.MajorGrid.IsVisible = true;
			pane.YAxis.MajorGrid.IsVisible = true;

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			// В противном случае на рисунке будет показана только часть графика, 
			// которая умещается в интервалы по осям, установленные по умолчанию
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}