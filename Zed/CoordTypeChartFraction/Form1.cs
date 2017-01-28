using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace CoordTypeChartFraction
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
		/// !!! Нарисовать графические объекты
		/// </summary>
		/// <param name="pane"></param>
		private void DrawObjects (GraphPane pane)
		{
			// Создадим текстовый объект, координаты которого задаются
			// не в координатах графика, а в координатах относительно графика
			TextObj text = new TextObj ("Этот текст всегда находится в углу",
				0.0, 1,                      // Координаты текстового объекта
				CoordType.ChartFraction,     // Координаты задаются относительно графика
				AlignH.Left,                 // Координата по X задает положение левой границы объекта
				AlignV.Bottom);              // Координата по Y задает положение нижней границы объекта

			// Добавим текст в список графических объектов
			pane.GraphObjList.Add (text);

			// Нарисуем перекрестие, которое всегда будет посередине графика
			
			// Создадим горизонтальную линию
			LineObj cross_hor = new LineObj (Color.Black, 0.48, 0.5, 0.52, 0.5);
			// Ее координаты рассчитываются относительно графика
			cross_hor.Location.CoordinateFrame = CoordType.ChartFraction;
			// Координаты задают центральную точку и по горизонтали, и по вертикали
			cross_hor.Location.AlignH = AlignH.Center;
			cross_hor.Location.AlignV = AlignV.Center;

			// Аналогично создаем вертикальную линию перекрестия
			LineObj cross_ver = new LineObj (Color.Black, 0.5, 0.48, 0.5, 0.52);
			cross_ver.Location.CoordinateFrame = CoordType.ChartFraction;
			cross_ver.Location.AlignH = AlignH.Center;
			cross_ver.Location.AlignV = AlignV.Center;

			// Добавим линиии перекрестия в список графических объектов
			pane.GraphObjList.Add (cross_hor);
			pane.GraphObjList.Add (cross_ver);
		}


		private void DrawGraph ()
		{
			GraphPane pane = zedGraph.GraphPane;

			// Нарисуем графические объекты
			DrawObjects (pane);

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