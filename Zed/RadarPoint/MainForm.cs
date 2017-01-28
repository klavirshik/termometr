using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace RadarPoint
{
	public partial class MainForm : Form
	{
		public MainForm ()
		{
			InitializeComponent ();

			DrawGraph ();
		}

		private void DrawGraph ()
		{
			GraphPane pane = zedGraph.GraphPane;

			pane.CurveList.Clear ();

			pane.XAxis.MajorGrid.IsVisible = true;
			pane.YAxis.MajorGrid.IsVisible = true;

			pane.YAxis.MajorGrid.IsZeroLine = false;

			// Создаем список точек 
			RadarPointList points = new RadarPointList ();


			// Т.к. в списке будет 4 точки, то и окружность будет разбиваться на 4 части
			// Обход точек будет осуществляться против часовой стрелки
			points.Clockwise = false;

			// Первая точка - сверху над началом координат. Расстояние до центра = 1
			points.Add (1, 1);

			// Вторая точка - слева от начала координат.  Расстояние до центра = 2
			points.Add (2, 1);

			// Третья точка - снизу под началом координат.  Расстояние до центра = 3
			points.Add (3, 1);

			// Четвертая точка - справа от начала координат.  Расстояние до центра = 4
			points.Add (4, 1);

			
			// Добавляем кривую по этим четырем точкам
			LineItem myCurve = pane.AddCurve ("", points, Color.Black, SymbolType.None);


			// Для наглядности нарисуем расстояния от начала координат до каждой из точек
			ArrowObj arrow1 = new ArrowObj (0, 0, 0, 1);
			pane.GraphObjList.Add (arrow1);

			ArrowObj arrow2 = new ArrowObj (0, 0, -2, 0);
			pane.GraphObjList.Add (arrow2);

			ArrowObj arrow3 = new ArrowObj (0, 0, 0, -3);
			pane.GraphObjList.Add (arrow3);

			ArrowObj arrow4 = new ArrowObj (0, 0, 4, 0);
			pane.GraphObjList.Add (arrow4);

			// Отметим начало координат черным квадратиком
			BoxObj box = new BoxObj (-0.05, 0.05, 0.1, 0.1, Color.Black, Color.Black);
			pane.GraphObjList.Add (box);			

			zedGraph.AxisChange ();			

			zedGraph.Invalidate ();
		}
	}
}