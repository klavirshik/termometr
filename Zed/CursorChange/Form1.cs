using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace CursorChange
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			// Подпишемся на событие, которое срабатывает при изменении курсора
			zedGraph.CursorChanged += new EventHandler (zedGraph_CursorChanged);

			DrawGraph ();
		}


		/// <summary>
		/// Событие при изменении курсора
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void zedGraph_CursorChanged (object sender, EventArgs e)
		{
			// Разрешим ZedGraph'у изменять курсор при выделении участка на графике, 
			// а также при перемещении графика.
			// В обоих случаях курсор будет "захвачен"
			// Если курсор не "захвачен", то установим курсор обратно в виде стрелки.
			// Если нужно запретить изменять курсор в любом случае, 
			// то достаточно просто убрать условие. 

			if (!zedGraph.Capture)
			{
				zedGraph.Cursor = Cursors.Arrow;
			}
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
			GraphPane pane = zedGraph.GraphPane;

			// Очистим список кривых
			pane.CurveList.Clear ();

			// Создадим список точек
			PointPairList list = new PointPairList ();

			double xmin = -50;
			double xmax = 50;

			// Заполняем список точек
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				// добавим в список точку
				list.Add (x, f (x));
			}

			// Создадим кривую
			LineItem myCurve = pane.AddCurve ("", list, Color.Blue, SymbolType.None);

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}