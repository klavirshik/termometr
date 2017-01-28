using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;


namespace FilteredList
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			DrawCurves ();
		}

		/// <summary>
		/// Рисование всех кривых
		/// </summary>
		private void DrawCurves ()
		{
			// Подготовим большие массивы данных по осям X и Y
			double xmin = -20;
			double xmax = 20;

			// Поличество точек в полном массиве
			int count = 10000;

			// Массивы с координатами точек по X и Y
			double[] xlist = new double[count];
			double[] ylist = new double[count];

			// Шаг дискретизации
			double dx = (xmax - xmin) / count;

			// Заполним массив из большого количества элементов
			for (int i = 0; i < count; i++)
			{
				double currx = xmin + i * dx;

				xlist[i] = currx;
				ylist[i] = f(currx);
			}

			// Сначала нарисуем кривую по полноценным массивам
			PointPairList fullList = new PointPairList (xlist, ylist);
			DrawGraph (fullList, Color.Red);

			// Теперь воспользуемся классом FilteredPointList, 
			// чтобы уменьшить количество отображаемых точек.
			// В конструктор класса передаются полноценные массивы
			FilteredPointList filteredList = new FilteredPointList (xlist, ylist);

			// Параметры фильтрации точек
			// Нас интересует только интервал от -15 до 15
			double filteredXMin = -15;
			double filteredXMax = 15;

			// Нам достаточно 20-ти точек
			int filteredCount = 20;

			// Установим параметры фильтрации
			filteredList.SetBounds (filteredXMin, filteredXMax, filteredCount);

			// Нарисуем кривую по отфильтрованным точкам
			DrawGraph (filteredList, Color.Blue);
		}


		/// <summary>
		/// Рисование одной кривой
		/// </summary>
		/// <param name="points">Точки для кривой</param>
		/// <param name="color">Цвет для кривой</param>
		private void DrawGraph (IPointList points, Color color)
		{
			// Получим панель для рисования
			GraphPane pane = zedGraph.GraphPane;

			// Создадим кривую, в названии которой содержится 
			// количество фактически отображаемых точек
			pane.AddCurve (points.Count.ToString() + " точек", 
				points, 
				color, 
				SymbolType.None);

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}


		private double f (double x)
		{
			if (x == 0)
			{
				return 1;
			}

			return Math.Sin (x) / x;
		}
	}
}