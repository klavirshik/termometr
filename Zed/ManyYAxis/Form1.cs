using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace ManyYAxis
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			DrawGraph ();
		}

		private double f1 (double x)
		{
			if (x == 0)
			{
				return 1;
			}

			return Math.Sin (x) / x;
		}


		private double f2 (double x)
		{
			if (x == 0)
			{
				return 1;
			}

			return 10 * Math.Sin (x * 0.5) / x;
		}


		private double f3 (double x)
		{
			if (x == 0)
			{
				return 1;
			}

			return 0.1 * Math.Sin (x * 2) / x;
		}


		private void DrawGraph ()
		{
			// Получим панель для рисования
			GraphPane pane = zedGraph.GraphPane;

			// Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
			pane.CurveList.Clear ();

			// Списки точек для трех графиков
			PointPairList list1 = new PointPairList ();
			PointPairList list2 = new PointPairList ();
			PointPairList list3 = new PointPairList ();

			double xmin = -50;
			double xmax = 50;

			// Заполняем списки точек
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				list1.Add (x, f1 (x));
				list2.Add (x, f2 (x));
				list3.Add (x, f3 (x));
			}

			// Удалим существующие оси Y
			pane.YAxisList.Clear ();

			// Создадим три новых оси Y
			// Метод AddYAxis() возвращает индекс новой оси в списке осей (YAxisList)
			int axis1 = pane.AddYAxis ("Axis 1");
			int axis2 = pane.AddYAxis ("Axis 2");
			int axis3 = pane.AddYAxis ("Axis 3");			

			// Создадим три кривые
			LineItem myCurve1 = pane.AddCurve ("Curve 1", list1, Color.Blue, SymbolType.None);
			LineItem myCurve2 = pane.AddCurve ("Curve 2", list2, Color.Black, SymbolType.None);
			LineItem myCurve3 = pane.AddCurve ("Curve 3", list3, Color.Red, SymbolType.None);

			// Для каждой кривой установим свои оси
			myCurve1.YAxisIndex = axis1;
			myCurve2.YAxisIndex = axis2;
			myCurve3.YAxisIndex = axis3;

			// Для наглядности раскрасим надписи на оси Y в цвета графика, 
			// который рисуется с этой осью
			pane.YAxisList[axis1].Title.FontSpec.FontColor = Color.Blue;
			pane.YAxisList[axis2].Title.FontSpec.FontColor = Color.Black;
			pane.YAxisList[axis3].Title.FontSpec.FontColor = Color.Red;

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			// В противном случае на рисунке будет показана только часть графика, 
			// которая умещается в интервалы по осям, установленные по умолчанию
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}