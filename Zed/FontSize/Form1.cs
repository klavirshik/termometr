using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace FontSize
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

			// Размеры шрифтов для разных элементов графика
			int labelsXfontSize = 25;
			int labelsYfontSize = 20;

			int titleXFontSize = 25;
			int titleYFontSize = 20;

			int legendFontSize = 15;

			int mainTitleFontSize = 30;

			// Установим размеры шрифтов для меток вдоль осей
			pane.XAxis.Scale.FontSpec.Size = labelsXfontSize;
			pane.YAxis.Scale.FontSpec.Size = labelsYfontSize;

			// Установим размеры шрифтов для подписей по осям
			pane.XAxis.Title.FontSpec.Size = titleXFontSize;
			pane.YAxis.Title.FontSpec.Size = titleYFontSize;

			// Установим размеры шрифта для легенды
			pane.Legend.FontSpec.Size = legendFontSize;

			// Установим размеры шрифта для общего заголовка
			pane.Title.FontSpec.Size = mainTitleFontSize;
			pane.Title.FontSpec.IsUnderline = true;


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