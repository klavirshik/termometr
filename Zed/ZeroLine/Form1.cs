using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace ZeroLine
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			DrawGraph ();
		}

		private void DrawGraph ()
		{
			// Получим панель для рисования
			GraphPane pane = zedGraph.GraphPane;

			// Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
			pane.CurveList.Clear ();

			// Создадим список точек
			PointPairList list = new PointPairList ();

			// Интервал, в котором будут лежать точки
			int xmin = -100;
			int xmax = 100;

			int ymin = -100;
			int ymax = 100;

			int pointsCount = 50;

			Random rnd = new Random ();

			// Заполняем список точек
			for (int i = 0; i < pointsCount; i++)
			{
				// Случайным образом сгенерим точку
				int x = rnd.Next (xmin, xmax);
				int y = rnd.Next (ymin, ymax);

				list.Add (x, y);
			}

			// Создадим кривую с названием "Scatter".
			// Обводка ромбиков будут рисоваться голубым цветом (Color.Blue),
			// Опорные точки - ромбики (SymbolType.Diamond)
			LineItem myCurve = pane.AddCurve ("Scatter", list, Color.Blue, SymbolType.Diamond);

			myCurve.Line.IsVisible = false;
			myCurve.Symbol.Fill.Color = Color.Blue;
			myCurve.Symbol.Fill.Type = FillType.Solid;
			myCurve.Symbol.Size = 7;

			// !!!
			// Горизонтальная линия на уровне y = 0 рисоваться не будет
			pane.YAxis.MajorGrid.IsZeroLine = false;

			// Устанавливаем интересующий нас интервал по оси X
			pane.XAxis.Scale.Min = xmin;
			pane.XAxis.Scale.Max = xmax;

			// Устанавливаем интересующий нас интервал по оси Y
			pane.YAxis.Scale.Min = ymin;
			pane.YAxis.Scale.Max = ymax;

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			// В противном случае на рисунке будет показана только часть графика, 
			// которая умещается в интервалы по осям, установленные по умолчанию
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}