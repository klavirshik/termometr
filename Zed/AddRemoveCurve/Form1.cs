using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace AddRemoveCurve
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();
		}

		// Массив цветов, из которых будем выбирать случайным образом цвет для графика
		Color[] _colors = new Color[] {Color.Black, 
			Color.Blue, 
			Color.Brown,
			Color.Gray,
			Color.Green,
			Color.Indigo,
			Color.Orange,
			Color.Red,
			Color.YellowGreen};

		/// <summary>
		/// Обработчик нажатия кнопки "Добавить"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void addBtn_Click (object sender, EventArgs e)
		{
			// Для генерации случайных точек и случайного цвета кривой
			Random rnd = new Random ();

			GraphPane pane = zedGraph.GraphPane;

			// Создадим список точек
			PointPairList list = new PointPairList ();

			double xmin = -50;
			double xmax = 50;

			// Заполняем список точек. Приращение по оси X тоже случайно
			for (double x = xmin; x <= xmax; x += rnd.NextDouble () * 10 + 1)
			{
				// Случайная координата по Y
				double y = rnd.NextDouble () * 10 - 5;

				// добавим в список точку
				list.Add (x, y);
			}

			// Выберем случайный цвет для графика
			Color curveColor = _colors[rnd.Next (_colors.Length)];
			LineItem myCurve = pane.AddCurve ("", list, curveColor, SymbolType.None);

			// Включим сглаживание
			myCurve.Line.IsSmooth = true;

			// Обновим график
			zedGraph.AxisChange ();
			zedGraph.Invalidate ();
		}

		private void removeBtn_Click (object sender, EventArgs e)
		{
			// Генератор случайных чисел для выбора номера графика, который нужно удалить
			Random rnd = new Random ();

			GraphPane pane = zedGraph.GraphPane;

			// Если есть что удалять
			if (pane.CurveList.Count > 0)
			{
				// Номер графика для удаления
				int index = rnd.Next (pane.CurveList.Count);

				// Удалим кривую по индексу
				pane.CurveList.RemoveAt (index);

				// Обновим график
				zedGraph.AxisChange ();
				zedGraph.Invalidate ();
			}
		}
	}
}