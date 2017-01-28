using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

using ZedGraph;
using System.Drawing;

namespace QueueTime
{
	public partial class Form1 : Form
	{
		/// <summary>
		/// Количество отображаемых точек
		/// </summary>
		int _capacity = 100;

		/// <summary>
		/// Здесь храним данные
		/// </summary>
		RollingPointPairList _data;

		// Параметры отображаемой синусоиды
		double _amplitude = 5;
		double _freq = 3;
		double _step = 0.1;

		// Текущее значение на графике
		double _currentx = 0;


		public Form1 ()
		{
			// !!! Создаем массив данных с ограниченной емкостью.
			// При превышениизаданной емкости первые элементы в массиве будут удаляться
			_data = new RollingPointPairList (_capacity);

			InitializeComponent ();
			PrepareGraph ();
		}


		/// <summary>
		/// Метод вызывается по таймеру
		/// </summary>
		private void timer_Tick (object sender, EventArgs e)
		{
			// Вычислим новое значение
			double newValue = _amplitude * Math.Sin (_currentx * _freq);

			// !!! Добавим новый отсчет к данным
			_data.Add (_currentx, newValue);
			_currentx += _step;

			// Рассчитаем интервал по оси X, который нужно отобразить на графике
			double xmin = _currentx - _capacity * _step;
			double xmax = _currentx;

			GraphPane pane = zedGraph.GraphPane;
			pane.XAxis.Scale.Min = xmin;
			pane.XAxis.Scale.Max = xmax;

			// Обновим оси
			zedGraph.AxisChange ();

			// Обновим сам график
			zedGraph.Invalidate ();
		}


		/// <summary>
		/// Подготовка к отображению данных
		/// </summary>
		private void PrepareGraph ()
		{
			// Получим панель для рисования
			GraphPane pane = zedGraph.GraphPane;

			// Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
			pane.CurveList.Clear ();

			// Добавим кривую пока еще без каких-либо точек
			LineItem myCurve = pane.AddCurve ("sin (x)", _data, Color.Blue, SymbolType.None);

			// Устанавливаем интересующий нас интервал по оси Y
			pane.YAxis.Scale.Min = -_amplitude;
			pane.YAxis.Scale.Max = _amplitude;

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}
