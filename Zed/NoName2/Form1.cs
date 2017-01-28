using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace NoName2
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

		/// <summary>
		/// Нарисовать кривую
		/// </summary>
		/// <param name="pane"></param>
		/// <param name="k">Коэффициент для функции</param>
		/// <param name="name">Имя кривой</param>
		/// <param name="color">Цвет для графика</param>
		private void AddCurve (GraphPane pane, double k, string name, bool labelVisible, Color color)
		{
			PointPairList list = new PointPairList ();

			double xmin = -20;
			double xmax = 20;

			for (double x = xmin; x <= xmax; x += 0.1)
			{
				list.Add (x, f (k * x));
			}

			LineItem myCurve = pane.AddCurve (name, list, color, SymbolType.None);

			// Установим видимость имени для кривой
			myCurve.Label.IsVisible = labelVisible;
		}

		private void DrawGraph ()
		{
			// Получим панель для рисования
			GraphPane pane = zedGraph.GraphPane;

			pane.CurveList.Clear ();

			// Нарисуем три кривые с разными именами
			// Первые два графика будут перечислены в легенде
			AddCurve (pane, 1.0, "k = 1", true, Color.Blue);
			AddCurve (pane, 0.5, "k = 0.5", true, Color.Black);

			// Этот график в легенде не будет отображаться
			AddCurve (pane, 0.1, "k = 0.1", false, Color.Red);

			// Обновим оси и график
			zedGraph.AxisChange ();
			zedGraph.Invalidate ();
		}		
	}
}