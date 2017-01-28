using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace MasterPane2
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
			// Создаем экземпляр класса MasterPane, который представляет собой область, 
			// на которйо "лежат" все графики (экземпляры класса GraphPane)
			ZedGraph.MasterPane masterPane = zedGraph.MasterPane;

			// По умолчанию в MasterPane содержится один экземпляр класса GraphPane 
			// (который можно получить из свойства zedGraph.GraphPane)
			// Очистим этот список, так как потом мы будем создавать графики вручную
			masterPane.PaneList.Clear ();

			// Добавим три графика
			for (int i = 0; i < 9; i++)
			{
				// Создаем экземпляр класса GraphPane, представляющий собой один график
				GraphPane pane = new GraphPane ();

				// Заполнение графика данными не изменилось, 
				// поэтому вынесем заполнение точек в отдельный метод DrawSingleGraph()
				DrawSingleGraph (pane);

				// Добавим новый график в MasterPane
				masterPane.Add (pane);
			}

			// Будем размещать добавленные графики в MasterPane
			using (Graphics g = CreateGraphics ())
			{
				// Графики будут размещены в три строки.
				// В первой будет 4 столбца,
				// Во второй - 2
				// В третей - 3
				// Если бы второй аргумент был равен false, то массив из третьего аргумента
				// задавал бы не количество столбцов в каждой строке,
				// а количество строк в каждом столбце
				masterPane.SetLayout (g, true, new int[] {4, 2, 3 } );
			}

			// Обновим оси и перерисуем график
			zedGraph.AxisChange ();
			zedGraph.Invalidate ();
		}

		private double f (double x)
		{
			if (x == 0)
			{
				return 1;
			}

			return Math.Sin (x) / x;
		}

		private void DrawSingleGraph (GraphPane pane)
		{
			pane.CurveList.Clear ();

			PointPairList list = new PointPairList ();

			double xmin = -40;
			double xmax = 40;

			for (double x = xmin; x <= xmax; x += 0.01)
			{
				list.Add (x, f (x));
			}

			LineItem myCurve = pane.AddCurve ("Sinc", list, Color.Blue, SymbolType.None);
		}
	}
}