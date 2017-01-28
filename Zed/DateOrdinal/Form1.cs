using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace DateOrdinal
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

			// Начальная дата для построения гистограммы
			DateTime startDate = DateTime.Now;

			// Количество дней, для заполнения данных
			int count = 30;

			// Для построения графика даты нужно преобразовать к Double
			List<double> xvalues = new List<double> ();

			// Высота столбиков
			List<double> yvalues = new List<double> ();

			// Заполним данные
			for (int i = 0; i < count; i++)
			{
				// Дата для очередного столбика
				DateTime currentDate = startDate.AddDays (i);

				// Для выходных дней столбики создавать не будем
				if (currentDate.DayOfWeek != DayOfWeek.Saturday &&
					currentDate.DayOfWeek != DayOfWeek.Sunday)
				{
					// Значения по оси X (текущая дата)
					xvalues.Add (new XDate (currentDate));

					// Высота столбиков
					yvalues.Add (rnd.NextDouble ());
				}
			}

			// Создадим гистограмму
			BarItem bar = pane.AddBar ("", xvalues.ToArray(), yvalues.ToArray(), Color.Blue);

			// !!! Для оси X установим тип AxisType.DateAsOrdinal,
			// который обозначает, что отмеченные даты откладываются равномерно
			pane.XAxis.Type = AxisType.DateAsOrdinal;

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}