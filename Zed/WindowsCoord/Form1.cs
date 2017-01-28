using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


using ZedGraph;

namespace WindowsCoord
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			// ѕодпишемс€ на событие, возникающее при нажатии кнопки мыши
			// ќбратите внимание, что подписываемс€ на событие MouseDownEvent, а не MouseDown
			zedGraph.MouseDownEvent += 
				new ZedGraphControl.ZedMouseEventHandler (zedGraph_MouseDownEvent);
			
			DrawGraph ();
		}


		/// <summary>
		/// ќбработчик событи€ нажати€ на кнопку мыши
		/// </summary>
		/// <returns>ћетод возвращает true, если нужно запретить дальнейшую встроенную обработку событи€ (показ контекстного меню, начало выделени€ и т.п.), и false, если обработка событи€ должна быть продолжена</returns>
		bool zedGraph_MouseDownEvent (ZedGraphControl sender, MouseEventArgs e)
		{
			GraphPane pane = zedGraph.GraphPane;

			//  оординаты, которые переданы в событие
			Point eventPoint = new Point (e.X, e.Y);
			eventCoord.Text = string.Format ("({0}; {1})", eventPoint.X, eventPoint.Y);

			//  оординаты, пересчитанные в систему координат графика
			double graphX, graphY;

			// ѕересчитать координаты из системы координат, св€занной с контролом zedGraph 
			// в систему координат, св€занную с графиком
			pane.ReverseTransform (new PointF (e.X, e.Y), out graphX, out graphY);
			graphCoord.Text = string.Format ("({0:F3}; {1:F3})", graphX, graphY);

			// ѕересчитаем в обратную сторону из системы координат графика
			// в систему координат контрола. 
			// ƒолжны получить те же значени€, что и в eventPoint 
			// (с точностью до погрешности округлени€)
			// ѕоследний параметр CoordType.AxisXYScale обозначает, 
			// в какой системе координат заданы координаты, переданные в первых двух параметрах.
			PointF controlPoint = pane.GeneralTransform (new PointF ((float)graphX, 
				(float)graphY), 
				CoordType.AxisXYScale);

			controlCoord.Text = string.Format ("({0}; {1})", controlPoint.X, controlPoint.Y);

			return false;
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

			pane.XAxis.MajorGrid.IsVisible = true;
			pane.YAxis.MajorGrid.IsVisible = true;

			// ќчистим список кривых
			pane.CurveList.Clear ();

			// —оздадим список точек
			PointPairList list = new PointPairList ();

			double xmin = -50;
			double xmax = 50;

			// «аполн€ем список точек
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				// добавим в список точку
				list.Add (x, f (x));
			}

			// —оздадим кривую
			LineItem myCurve = pane.AddCurve ("", list, Color.Blue, SymbolType.None);

			// ¬ызываем метод AxisChange (), чтобы обновить данные об ос€х. 
			zedGraph.AxisChange ();

			// ќбновл€ем график
			zedGraph.Invalidate ();
		}
	}
}