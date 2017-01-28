using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace BarView
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

			// Очистим список кривых
			pane.CurveList.Clear ();

			// Количество столбцов в гистограмме
			int itemscount = 5;

			Random rnd = new Random ();

			// Высота столбцов
			double[] values = new double[itemscount];

			// Заполним данные
			for (int i = 0; i < itemscount; i++)
			{
				values[i] = rnd.NextDouble ();
			}

			// Создадим кривую-гистограмму
			BarItem curve = pane.AddBar ("Гистограмма", null, values, Color.Blue);

			// !!!
			// Установим цвет для столбцов гистограммы
			curve.Bar.Fill.Color = Color.YellowGreen;

			// Отключим градиентную заливку
			curve.Bar.Fill.Type = FillType.Solid;

			// Сделаем границы столбцов невидимыми
			curve.Bar.Border.IsVisible = false;

			// Обновить данные об осях
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}