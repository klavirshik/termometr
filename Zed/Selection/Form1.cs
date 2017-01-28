using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;


namespace Selection
{
	public partial class Form1 : Form
	{
		// Сохраним кривую для рисования
		CurveItem _myCurve;

		public Form1 ()
		{
			InitializeComponent ();

			// !!!
			// Разрешим выбор кривых
			zedGraph.IsEnableSelection = true;

			// Выбирать кривые будем с помощью левой кнопки мыши
			zedGraph.SelectButtons = MouseButtons.Left;

			// При этом клавиши нажимать никакие не надо
			zedGraph.SelectModifierKeys = Keys.None;

			// Отключим масштабирование, так как по умолчанию 
			// оно тоже использует левую кнопку мыши без дополнительных клавиш
			zedGraph.IsEnableZoom = false;

			// Подпишемся на событие, которое происходит при выборе или отмене выбора кривых
			zedGraph.Selection.SelectionChangedEvent += 
				new EventHandler (Selection_SelectionChangedEvent);			

			DrawGraph ();
		}


		/// <summary>
		/// Обработчик события выбора/отмены выбора
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Selection_SelectionChangedEvent (object sender, EventArgs e)
		{
			// С помощью свойства класса CurveItem.IsSelected 
			// можно определить, выбрана данная кривая или нет.
			// Также можно воспользоваться свойством zedGraph.Selection,
			// Которое хранит список выбранных в данный момент кривых			
			if (_myCurve.IsSelected)
			{
				Text = "Кривая выбрана";
			}
			else
			{
				Text = "Кривая не выбрана";
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

			// Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
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
			_myCurve = pane.AddCurve ("", list, Color.Blue, SymbolType.None);

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}