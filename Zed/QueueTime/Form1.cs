using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace QueueTime
{
	public partial class Form1 : Form
	{
		/// <summary>
		/// Максимальный размер очереди
		/// </summary>
		int _capacity = 30;

		/// <summary>
		/// Здесь храним данные
		/// </summary>
		List<double> _data;

		/// <summary>
		/// Для генерации слуайных данные по таймеру
		/// </summary>
		Random _rnd = new Random ();

		// Интервал изменения данных по вертикали
		double _ymin = -1.0;
		double _ymax = 1.0;


		public Form1 ()
		{
			_data = new List<double> ();

			InitializeComponent ();

			DrawGraph ();
		}

		private void timer_Tick (object sender, EventArgs e)
		{
			// Вычислим новое значение
			double newValue = _rnd.NextDouble () * (_ymax - _ymin) + _ymin;

			// Добавим его в конец списка
			_data.Add (newValue);

			// Удалим первый элемент в списке данных, 
			// если заполнили максимальную емкость
			if (_data.Count > _capacity)
			{
				_data.RemoveAt (0);
			}

			DrawGraph ();
		}

		private void DrawGraph ()
		{
			// Получим панель для рисования
			GraphPane pane = zedGraph.GraphPane;

			// Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
			pane.CurveList.Clear ();

			// Создадим список точек
			PointPairList list = new PointPairList ();

			// Интервал, где есть данные
			double xmin = 0;
			double xmax = _capacity;

			// Расстояние между соседними точками по горизонтали
			double dx = 1.0;

			double curr_x = 0;

			// Заполняем список точек
			foreach (double val in _data)
			{
				list.Add (curr_x, val);
				curr_x += dx;
			}

			// Очистим список кривых от прошлых рисунков (кадров)
			pane.CurveList.Clear ();
			LineItem myCurve = pane.AddCurve ("Random Value", list, Color.Blue, SymbolType.None);


			// Устанавливаем интересующий нас интервал по оси X
			pane.XAxis.Scale.Min = xmin;
			pane.XAxis.Scale.Max = xmax;

			// Устанавливаем интересующий нас интервал по оси Y
			pane.YAxis.Scale.Min = _ymin;
			pane.YAxis.Scale.Max = _ymax;

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			// В противном случае на рисунке будет показана только часть графика, 
			// которая умещается в интервалы по осям, установленные по умолчанию
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}
