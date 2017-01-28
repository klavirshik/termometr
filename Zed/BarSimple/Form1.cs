using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace BarSimple
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

			int itemscount = 5;

			Random rnd = new Random ();

			// Подписи под столбиками
			string[] names = new string[itemscount];

			// Высота столбиков
			double[] values = new double[itemscount];

			// Заполним данные
			for (int i = 0; i < itemscount; i++)
			{
				names[i] = string.Format ("Текст {0}", i);
				values[i] = rnd.NextDouble ();
			}

			// Создадим кривую-гистограмму
			// Первый параметр - название кривой для легенды
			// Второй параметр - значения для оси X, т.к. у нас по этой оси будет идти текст, а функция ожидает тип параметра double[], то пока передаем null
			// Третий параметр - значения для оси Y
			// Четвертый параметр - цвет
			BarItem curve = pane.AddBar ("Гистограмма", null, values, Color.Blue);

			// Настроим ось X так, чтобы она отображала текстовые данные
			pane.XAxis.Type = AxisType.Text;

			// Уставим для оси наши подписи
			pane.XAxis.Scale.TextLabels = names;

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}