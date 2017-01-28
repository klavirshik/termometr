using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace ChangeCurves
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			DrawGraphs ();
		}

		/// <summary>
		/// Функция для рисования
		/// </summary>
		/// <param name="x"></param>
		/// <returns></returns>
		private double f (double x)
		{
			if (x == 0)
			{
				return 1;
			}

			return Math.Sin (x) / x;
		}


		/// <summary>
		/// Нарисовать изначальные графики
		/// </summary>
		private void DrawGraphs ()
		{
			// Получим панель для рисования
			GraphPane pane = zedGraph.GraphPane;

			// Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
			pane.CurveList.Clear ();

			// Два списка точек для двух графиков
			PointPairList list1 = new PointPairList ();
			PointPairList list2 = new PointPairList ();

			double xmin = -50;
			double xmax = 50;

			// Заполняем списи точек
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				list1.Add (x, f (x));
				list2.Add (x, f (x * 0.5));
			}

			// Добавим две кривые, но не будем сохранять указатели на них
			pane.AddCurve ("", list1, Color.Blue, SymbolType.None);
			pane.AddCurve ("", list2, Color.Red, SymbolType.None);

			zedGraph.AxisChange ();
			zedGraph.Invalidate ();
		}


		/// <summary>
		/// Обработчик нажатия на кнопку
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void changeBtn_Click (object sender, EventArgs e)
		{
			GraphPane pane = zedGraph.GraphPane;

			// Изменим существующие кривые.
			// Доступ к кривым осуществляется по индексам 
			// (Есть еще перегруженный оператор this[] для доступа к кривым по меткам)
			ModifyCurve (pane.CurveList[0], 1.1);
			ModifyCurve (pane.CurveList[1], 0.9);

			// Обновим оси и сам график
			zedGraph.AxisChange ();
			zedGraph.Invalidate ();
		}


		/// <summary>
		/// Изменение кривой. Координата X остается неизменной, а координата Y умножается на k
		/// </summary>
		private static void ModifyCurve (CurveItem curve, double k)
		{
			// Создадим новый список точек для кривой
			PointPairList newlist = new PointPairList ();

			// Пробежимся по всем точкам на кривой
			for (int i = 0; i < curve.Points.Count; i++)
			{
				// Заполним новый список точек
				newlist.Add (curve.Points[i].X, curve.Points[i].Y * k);
			}

			// Заменим список точек в кривой
			curve.Points = newlist;
		}
	}
}