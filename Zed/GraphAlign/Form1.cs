using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace GraphAlign
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			DrawGraphs ();
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
			double k = 1000;

			if (x == 0)
			{
				return k;
			}

			return k * Math.Sin (x) / x;
		}


		private void DrawGraphs ()
		{
			// Получим панели для рисования
			GraphPane pane1 = zedGraph1.GraphPane;
			GraphPane pane2 = zedGraph2.GraphPane;

			// Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
			pane1.CurveList.Clear ();
			pane2.CurveList.Clear ();

			// Для большей наглядности сделаем так, 
			// чтобы шрифты около осей Y на графиках сильно отличались.
			// Тогда сдвиг между графиками будет более заметен.
			pane1.YAxis.Scale.FontSpec.Size = 10;
			pane2.YAxis.Scale.FontSpec.Size = 35;

			// Создадим список точек для двух графиков
			PointPairList list1 = new PointPairList ();
			PointPairList list2 = new PointPairList ();

			double xmin = -50;
			double xmax = 50;

			// Заполняем список точек
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				list1.Add (x, f1 (x));
				list2.Add (x, f2 (x));
			}

			// Создадим кривые
			LineItem myCurve1 = pane1.AddCurve ("", list1, Color.Blue, SymbolType.None);
			LineItem myCurve2 = pane2.AddCurve ("", list2, Color.Blue, SymbolType.None);

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			zedGraph1.AxisChange ();
			zedGraph2.AxisChange ();

			// !!! Теперь мы можем изменить область, которая ограничена осями графика.
			// Chart - это экземпляр класса, который отвечает за сам график.
			// В данном случае мы просто происваиваем размеры прямоугольника 
			// одного графика другому, в реальности могут понадобиться проверки
			// того, какой график занимает больше места или более умный расчет размеров.
			// Также надо учесть, что графики могут иметь разные размеры по высоте.
			pane1.Chart.Rect = new RectangleF (pane2.Chart.Rect.X,
				pane2.Chart.Rect.Y,
				pane2.Chart.Rect.Width,
				pane2.Chart.Rect.Height);

			// В нашем случае присваивание размеров можно записать более кратко:
			//pane1.Chart.Rect = pane2.Chart.Rect;
			

			// Обновляем график
			zedGraph1.Invalidate ();
			zedGraph2.Invalidate ();
		}
	}
}