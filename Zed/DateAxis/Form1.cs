using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace DateAxis
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

			// Первая дата на оси X
			DateTime startDate = new DateTime (2011, 02, 25);

			// Количество отсчетов по оси X (количество дней)
			int daysCount = 20;

			Random rnd = new Random();

			// Заполняем список точек
			for (int i = 0; i < daysCount; i++)
			{
				// Текущая точка по оси X. Один отсчет - один день
				DateTime currentDate = startDate.AddDays (i);

				// Текущее значение по оси Y
				double yValue = rnd.NextDouble () * 0.2 + 0.9;

				// Используем класс XDate для преобразования DateTime к типу Double.
				// Используется неявное приведение к типу Double.
				// Внутри даты хранятся в виде дробных чисел.
				// Вместо DateTime можно было бы сразу использовать тип XDate
				list.Add (new XDate (currentDate), yValue);
			}

			// Создаем кривую,
			// которая будет рисоваться голубым цветом (Color.Blue),
			// опорные точки выделяться не будут (SymbolType.None)
			// Создание кривой с использованием дат ничем не отличается от создания других кривых
			LineItem myCurve = pane.AddCurve ("", list, Color.Blue, SymbolType.None);

			// Установим, что по оси X у нас откладываются даты,
			pane.XAxis.Type = AxisType.Date;

			// Для наглядности установим масштаб по оси Y
			pane.YAxis.Scale.Min = 0;
			pane.YAxis.Scale.Max = 1.4;

			// Пример изменения масштаба по оси, где откладываютя даты
			pane.XAxis.Scale.Min = new XDate (2011, 02, 20);
			pane.XAxis.Scale.Max = new XDate (2011, 03, 20);

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}
