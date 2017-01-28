using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace AxisTicks
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

			// Зделаем чуть помальче шрифт, чтобы уместилось побольше меток
			pane.XAxis.Scale.FontSpec.Size = 10;
			pane.YAxis.Scale.FontSpec.Size = 10; 

			// !!!
			// Крупные риски по оси X будут идти с периодом 5
			pane.XAxis.Scale.MajorStep = 5.0;

			// Мелкие риски будут идти с периодом 1
			// Таким образом, между крупными рисками будет 5 делений или 4 риски
			pane.XAxis.Scale.MinorStep = 1.0;

			// Крупные риски по оси Y будут идти с периодом 0.1
			pane.YAxis.Scale.MajorStep = 0.1;

			// А мелкие риски - с периодом 0.05
			// Между крупными рисками по оси Y будет два отсчета или одна риска
			pane.YAxis.Scale.MinorStep = 0.05;

			// Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
			pane.CurveList.Clear ();

			// Создадим список точек
			PointPairList list = new PointPairList ();

			double xmin = -30;
			double xmax = 30;

			// Заполняем список точек
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				// добавим в список точку
				list.Add (x, f (x));
			}

			// Создадим кривую
			LineItem myCurve = pane.AddCurve ("", list, Color.Blue, SymbolType.None);

			// Обновим данные об осях
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}