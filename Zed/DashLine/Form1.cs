using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;
using System.Drawing.Drawing2D;

namespace DashLine
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

			pane.CurveList.Clear ();

			// Нарисуем три кривые с разными стилями
			AddCurve1 (pane);
			AddCurve2 (pane);
			AddCurve3 (pane);

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			// В противном случае на рисунке будет показана только часть графика, 
			// которая умещается в интервалы по осям, установленные по умолчанию
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}

		/// <summary>
		/// Нарисовать кривую в виде точек
		/// </summary>
		/// <param name="pane"></param>
		private void AddCurve1 (GraphPane pane)
		{
			PointPairList list = new PointPairList ();

			double xmin = -20;
			double xmax = 20;

			for (double x = xmin; x <= xmax; x += 0.1)
			{
				list.Add (x, f (x));
			}

			LineItem myCurve = pane.AddCurve ("Curve 1", list, Color.Blue, SymbolType.None);

			// Используем предустановленный стиль, рисующий кривую точками.
			// Идентификатор стиля определен в System.Drawing.Drawing2D.DashStyle.
			myCurve.Line.Style = DashStyle.Dot;

			// Укажем, что график должен быть сглажен, 
			// иначе несплошные линии будут выглядеть неаккуратно.
			// Это происходит из-за того, что без использования сглаживания
			// ZedGraph будет каждый участок линии рисовать независимо от соседних.
			myCurve.Line.IsSmooth = true;
		}


		/// <summary>
		/// Нарисовать кривую в виде штрихпунктирной линии
		/// </summary>
		/// <param name="pane"></param>
		private void AddCurve2 (GraphPane pane)
		{
			PointPairList list = new PointPairList ();

			double xmin = -20;
			double xmax = 20;

			for (double x = xmin; x <= xmax; x += 0.1)
			{
				list.Add (x, f (2 * x));
			}

			LineItem myCurve = pane.AddCurve ("Curve 2", list, Color.Black, SymbolType.None);

			// Используем предустановленный стиль, рисующий кривую в виде штрихпунктирной линии.
			myCurve.Line.Style = DashStyle.DashDot;

			// Укажем, что график должен быть сглажен
			myCurve.Line.IsSmooth = true;
		}

		/// <summary>
		/// Нарисовать кривую нестандартным стилем
		/// </summary>
		/// <param name="pane"></param>
		private void AddCurve3 (GraphPane pane)
		{
			PointPairList list = new PointPairList ();

			double xmin = -20;
			double xmax = 20;

			for (double x = xmin; x <= xmax; x += 0.1)
			{
				list.Add (x, f (0.1 * x));
			}

			LineItem myCurve = pane.AddCurve ("Curve 3", list, Color.Brown, SymbolType.None);

			// Определим свой стиль рисования линии
			myCurve.Line.Style = DashStyle.Custom;

			// Длина пунктира будет 10 у.е. (1 у.е = 1/72 inch)
			myCurve.Line.DashOn = 10.0f;

			// Длина пропуска между пунктирами
			myCurve.Line.DashOff = 3.0f;

			// Укажем, что график должен быть сглажен
			myCurve.Line.IsSmooth = true;
		}
	}
}