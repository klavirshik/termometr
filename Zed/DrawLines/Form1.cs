using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace DrawLines
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
			GraphPane pane = zedGraph.GraphPane;

			pane.CurveList.Clear ();

			PointPairList list = new PointPairList ();

			double xmin = -50;
			double xmax = 50;

			// Заполняем список точек
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				list.Add (x, f(x));
			}
			LineItem myCurve = pane.AddCurve ("Sinc", list, Color.Blue, SymbolType.None);

			zedGraph.AxisChange ();

			// !!!
			// Линию рисуем после обновления осей с помощью AxisChange (), 
			// так как мы будем использовать значения
			// Нарисуем горизонтальную пунктирную линию от левого края до правого на уровне y = 0.5
			double level = 0.5;
			LineObj line = new LineObj (pane.XAxis.Scale.Min, level, pane.XAxis.Scale.Max, level);

			// Стиль линии - пунктирная
			line.Line.Style = System.Drawing.Drawing2D.DashStyle.Dash;

			// Добавим линию в список отображаемых объектов
			pane.GraphObjList.Add (line); 

			// Нарисуем стрелку, указыающую на максимум функции
			// Координаты точки, куда указывает стрелка
			// Координаты привязаны к осям
			double xend = 0.0;
			double yend = f(0);

			// Координаты точки начала стрелки
			double xstart = xend + 5.0;
			double ystart = yend + 0.1;

			// Рисование стрелки с текстом
			// Создадим стрелку
			ArrowObj arrow = new ArrowObj (xstart, ystart, xend, yend);

			// Добавим стрелку в список отображаемых объектов
			pane.GraphObjList.Add (arrow);

			// Напишем текст около начала стрелки
			// Координаты привязаны к осям
			TextObj text = new TextObj ("Max", xstart, ystart);

			// Отключим рамку вокруг текста
			text.FontSpec.Border.IsVisible = false;

			// Добавим текст в список отображаемых объектов
			pane.GraphObjList.Add (text);

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}