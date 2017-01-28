using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace BarWidth
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

			int itemscount = 19;

			Random rnd = new Random ();

			// Высота столбиков
			double[] values = new double[itemscount];

			// Заполним данные
			for (int i = 0; i < itemscount; i++)
			{
				values[i] = rnd.NextDouble ();
			}

			// Создадим кривую-гистограмму
			// Первый параметр - название кривой для легенды
			// Второй параметр - значения для оси X
			// Третий параметр - значения для оси Y
			// Четвертый параметр - цвет
			BarItem bar = pane.AddBar ("Гистограмма", null, values, Color.Blue);

			// !!! Расстояния между кластерами (группами столбиков) гистограммы = 0.0
			// У нас в кластере только один столбик.
			pane.BarSettings.MinClusterGap = 0.0f;

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}