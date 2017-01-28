using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace DateBar
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

			Random rnd = new Random (100);

			// Даты, которым будут соответствовать столбики
			XDate[] dates = new XDate[] { new XDate (2011, 02, 25), new XDate (2011, 02, 26),
				new XDate (2011, 02, 27), new XDate (2011, 03, 01), new XDate (2011, 03, 02),
				new XDate (2011, 03, 04), new XDate (2011, 03, 06)};

			// Для построения графика даты нужно преобразовать к Double
			double[] xvalues = new double[dates.Length];

			// Высота столбиков
			double[] yvalues = new double[dates.Length];

			// Заполним данные
			for (int i = 0; i < dates.Length; i++)
			{
				// Значения по оси X
				xvalues[i] = dates[i];

				// Высота столбиков
				yvalues[i] = rnd.NextDouble ();
			}

			// Создадим кривую-гистограмму
			BarItem curve = pane.AddBar ("", xvalues, yvalues, Color.Blue);

			// !!! Для оси X установим календарный тип
			pane.XAxis.Type = AxisType.Date;

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}