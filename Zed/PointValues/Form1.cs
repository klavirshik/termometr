using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace PointValues
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			// Включим показ всплывающих подсказок при наведении курсора на график
			zedGraph.IsShowPointValues = true;

			// Будем обрабатывать событие PointValueEvent, чтобы изменить формат представления координат
			zedGraph.PointValueEvent += 
				new ZedGraphControl.PointValueHandler (zedGraph_PointValueEvent);

			DrawGraph ();
		}


		/// <summary>
		/// Обработчик события PointValueEvent.
		/// Должен вернуть строку, которая будет показана во всплывающей подсказке
		/// </summary>
		/// <param name="sender">Отправитель сообщения</param>
		/// <param name="pane">Панель для рисования</param>
		/// <param name="curve">Кривая, около которой находится курсор</param>
		/// <param name="iPt">Номер точки в кривой</param>
		/// <returns>Нужно вернуть отображаемую строку</returns>
		string zedGraph_PointValueEvent (ZedGraphControl sender, 
			GraphPane pane, 
			CurveItem curve, 
			int iPt)
		{
			// Получим точку, около которой находимся
			PointPair point = curve[iPt];

			// Сформируем строку
			string result = string.Format ("X: {0:F3}\nY: {1:F3}", point.X, point.Y);

			return result;
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

			// Заполним список точек
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				// Добавим в список точку
				list.Add (x, f (x));
			}

			// Создадим кривую с названием "Sinc", 
			// которая будет рисоваться голубым цветом (Color.Blue),
			// Опорные точки выделяться не будут (SymbolType.None)
			LineItem myCurve = pane.AddCurve ("Sinc", list, Color.Blue, SymbolType.None);

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			// В противном случае на рисунке будет показана только часть графика, 
			// которая умещается в интервалы по осям, установленные по умолчанию
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}