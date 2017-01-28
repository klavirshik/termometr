using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace MoveAxis
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
			return -x * x + 30;
		}


		private void DrawGraph ()
		{
			GraphPane pane = zedGraph.GraphPane;

			// Очистим список кривых
			pane.CurveList.Clear ();

			// Создадим список точек
			PointPairList list = new PointPairList ();

			double xmin = -10;
			double xmax = 10;

			// Заполняем список точек
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				// добавим в список точку
				list.Add (x, f (x));
			}

			// Создадим кривую
			LineItem myCurve = pane.AddCurve ("", list, Color.Blue, SymbolType.None);


			// !!!
			// Ось X будет пересекаться с осью Y на уровне Y = 0
			pane.XAxis.Cross = 0.0;

			// Ось Y будет пересекаться с осью X на уровне X = 0
			pane.YAxis.Cross = 0.0;

			// Отключим отображение первых и последних меток по осям
			pane.XAxis.Scale.IsSkipFirstLabel = true;
			pane.XAxis.Scale.IsSkipLastLabel = true;

			// Отключим отображение меток в точке пересечения с другой осью
			pane.XAxis.Scale.IsSkipCrossLabel = true;


			// Отключим отображение первых и последних меток по осям
			pane.YAxis.Scale.IsSkipFirstLabel = true;

			// Отключим отображение меток в точке пересечения с другой осью
			pane.YAxis.Scale.IsSkipLastLabel = true;
			pane.YAxis.Scale.IsSkipCrossLabel = true;

			// Спрячем заголовки осей
			pane.XAxis.Title.IsVisible = false;
			pane.YAxis.Title.IsVisible = false;

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			zedGraph.AxisChange ();


			// Обновляем график
			zedGraph.Invalidate ();
		}
	}

}