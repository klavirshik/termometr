using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;


namespace AxisColor
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

			// Установка цветов происходит в методе SetColors()
			SetColors (pane);			

			// Очистим список кривых
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

			// Создадим кривую
			LineItem myCurve = pane.AddCurve ("", list, Color.Yellow, SymbolType.None);

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}


		private static void SetColors (GraphPane pane)
		{
			// !!!
			// Установим цвет рамки для всего компонента
			pane.Border.Color = Color.Red;

			// Установим цвет рамки вокруг графика
			pane.Chart.Border.Color = Color.Green;

			// Закрасим фон всего компонента ZedGraph
			// Заливка будет сплошная
			pane.Fill.Type = FillType.Solid;
			pane.Fill.Color = Color.Silver;

			// Закрасим область графика (его фон) в черный цвет
			pane.Chart.Fill.Type = FillType.Solid;
			pane.Chart.Fill.Color = Color.Black;

			// Включим показ оси на уровне X = 0 и Y = 0, чтобы видеть цвет осей
			pane.XAxis.MajorGrid.IsZeroLine = true;
			pane.YAxis.MajorGrid.IsZeroLine = true;
			// Установим цвет осей
			pane.XAxis.Color = Color.Gray;
			pane.YAxis.Color = Color.Gray;

			// Включим сетку
			pane.XAxis.MajorGrid.IsVisible = true;
			pane.YAxis.MajorGrid.IsVisible = true;
			// Установим цвет для сетки
			pane.XAxis.MajorGrid.Color = Color.Cyan;
			pane.YAxis.MajorGrid.Color = Color.Cyan;

			// Установим цвет для подписей рядом с осями
			pane.XAxis.Title.FontSpec.FontColor = Color.White;
			pane.YAxis.Title.FontSpec.FontColor = Color.White;

			// Установим цвет подписей под метками
			pane.XAxis.Scale.FontSpec.FontColor = Color.GreenYellow;
			pane.YAxis.Scale.FontSpec.FontColor = Color.GreenYellow;

			// Установим цвет заголовка над графиком
			pane.Title.FontSpec.FontColor = Color.Khaki;
		}
	}
}