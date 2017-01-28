using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace EditPoints
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();
			
			// !!!
			// Перемещать точки можно будет с помощью средней кнопки мыши...
			zedGraph.EditButtons = MouseButtons.Middle;

			// ... и при нажатой клавише Alt.
			zedGraph.EditModifierKeys = Keys.Alt;

			// Точки можно перемещать, как по горизонтали,...
			zedGraph.IsEnableHEdit = true;

			// ... так и по вертикали.
			zedGraph.IsEnableVEdit = true;

			// Подпишемся на событие, вызываемое после перемещения точки
			zedGraph.PointEditEvent += 
				new ZedGraphControl.PointEditHandler (zedGraph_PointEditEvent);

			DrawGraph ();
		}


		/// <summary>
		/// Обработчик события перемещения точки.
		/// При перемещении точки, информация о ней записывается в заголовок окна
		/// </summary>
		/// <param name="sender">Компонент ZedGraph</param>
		/// <param name="pane">Панель с графиком</param>
		/// <param name="curve">Кривая, точку которой переместили</param>
		/// <param name="iPt">Номер точки</param>
		/// <returns>Метод должен возвращать строку</returns>
		string zedGraph_PointEditEvent (ZedGraphControl sender, 
			GraphPane pane, CurveItem curve, int iPt)
		{
			string title = string.Format ("Точка: {0}. Новые координаты: ({1}; {2})",
				iPt, curve[iPt].X, curve[iPt].Y);

			this.Text = title;

			return title;
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

			// Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
			pane.CurveList.Clear ();

			// Создадим список точек
			PointPairList list = new PointPairList ();

			double xmin = -50;
			double xmax = 50;

			// Заполняем список точек
			for (double x = xmin; x <= xmax; x += 1)
			{
				// добавим в список точку
				list.Add (x, f (x));
			}

			// Создадим кривую
			LineItem myCurve = pane.AddCurve ("", list, Color.Blue, SymbolType.Circle);

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}