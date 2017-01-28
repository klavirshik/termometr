using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace Titles
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

			// !!!
			// Изменим тест надписи по оси X
			pane.XAxis.Title.Text = "Ось X";

			// Изменим параметры шрифта для оси X
			pane.XAxis.Title.FontSpec.IsUnderline = true;
			pane.XAxis.Title.FontSpec.IsBold = false;
			pane.XAxis.Title.FontSpec.FontColor = Color.Blue;

			// Изменим текст по оси Y
			pane.YAxis.Title.Text = "Ось Y";

			// Изменим текст заголовка графика
			pane.Title.Text = "Sinc";

			// В параметрах шрифта сделаем заливку красным цветом
			pane.Title.FontSpec.Fill.Brush = new SolidBrush (Color.Red);
			pane.Title.FontSpec.Fill.IsVisible = true;

			// Сделаем шрифт не полужирным
			pane.Title.FontSpec.IsBold = false;

			
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

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}