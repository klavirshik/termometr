using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace ScaleFormat
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			// Установка свойств оси X
			SetAxisProperties ();

			DrawGraph ();
		}

		/// <summary>
		/// Установка свойств оси X
		/// </summary>
		void SetAxisProperties ()
		{
			GraphPane pane = zedGraph.GraphPane;

			// Установим шаг основных меток, равным 5
			pane.XAxis.Scale.MajorStep = 5;

			// Немного уменьшим шрифт меток, чтобы их больше умещалось
			pane.XAxis.Scale.FontSpec.Size = 10;
			
			// Подпишемся на событие, которое будет вызываться при выводе каждой отметки на оси
			pane.XAxis.ScaleFormatEvent += new Axis.ScaleFormatHandler (XAxis_ScaleFormatEvent);	
		}


		/// <summary>
		/// Метод, который вызывается, когда надо отобразить очередную метку по оси
		/// </summary>
		/// <param name="pane">Указатель на текущий GraphPane</param>
		/// <param name="axis">Указатель на ось</param>
		/// <param name="val">Значение, которое надо отобразить</param>
		/// <param name="index">Порядковый номер данного отсчета</param>
		/// <returns>Метод должен вернуть строку, которая будет отображаться под данной меткой</returns>
		string XAxis_ScaleFormatEvent (GraphPane pane, Axis axis, double val, int index)
		{
			if (val % 10 == 0)
			{
				// Если текущее значение кратно 10, то возьмем его в квадратные скобки
				return string.Format ("[{0}]", val);
			}
			else
			{
				// Остальные числа просто преобразуем в строку
				return val.ToString ();
			}
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

			// Очистим список кривых
			pane.CurveList.Clear ();

			// Создадим список точек
			PointPairList list = new PointPairList ();

			double xmin = -29;
			double xmax = 29;

			// Заполняем список точек
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				// добавим в список точку
				list.Add (x, f (x));
			}

			// Создадим кривую
			LineItem myCurve = pane.AddCurve ("", list, Color.Blue, SymbolType.None);

			// Обновить данные об осях. 
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}