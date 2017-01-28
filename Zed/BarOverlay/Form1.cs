using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;


namespace BarOverlay
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
			// Удалим существующую панель с графиком
			zedGraph.MasterPane.PaneList.Clear ();

			// Создадим две панели для графика, где будут отображаться
			// одинаковые данные, но с разными значениями BarType
			GraphPane pane1 = new GraphPane ();
			GraphPane pane2 = new GraphPane ();

			// Количество столбцов
			int itemscount = 9;
			Random rnd = new Random ();

			// Сгенерируем данные для высот столбцов
			double[] YValues1 = GenerateData (itemscount, rnd);
			double[] YValues2 = GenerateData (itemscount, rnd);
			double[] YValues3 = GenerateData (itemscount, rnd);

			double[] XValues = new double[itemscount];

			// Заполним данные
			for (int i = 0; i < itemscount; i++)
			{
				XValues[i] = i + 1;
			}

			// По одинаковым данным построим две гистограммы
			CreateBars (pane1, XValues, YValues1, YValues2, YValues3);
			CreateBars (pane2, XValues, YValues1, YValues2, YValues3);

			// !!! У первого графика столбцы накладываются один на другой
			// всегда в одинаковой последовательности:
			// впереди синий, затем красный, затем желтый
			pane1.BarSettings.Type = BarType.Overlay;
			pane1.Title.Text = "BarType.Overlay";

			// !!! У второго графика порядок наложения столбцов такой, чтобы все они были видны
			pane2.BarSettings.Type = BarType.SortedOverlay;
			pane2.Title.Text = "BarType.SortedOverlay";

			// Добавим созданные панели в MasterPane
			zedGraph.MasterPane.Add (pane1);
			zedGraph.MasterPane.Add (pane2);

			// Зададим расположение графиков
			using (Graphics g = CreateGraphics ())
			{
				// Графики будут размещены в один столбец друг под другом
				zedGraph.MasterPane.SetLayout (g, PaneLayout.SingleColumn);
			}

			// Обновим данные об осях
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
		

		/// <summary>
		/// Сгенерировать случайные данные для графика
		/// </summary>
		/// <param name="itemscount"></param>
		/// <param name="rnd"></param>
		/// <returns></returns>
		private double[] GenerateData (int itemscount, Random rnd)
		{
			double[] values = new double[itemscount];

			// Заполним данные
			for (int i = 0; i < itemscount; i++)
			{
				values[i] = rnd.NextDouble ();
			}

			return values;
		}


		/// <summary>
		/// Создать столбики по данным
		/// </summary>
		/// <param name="pane">Панель, куда добавляются столбцы</param>
		/// <param name="XValues">Координаты по оси X</param>
		/// <param name="YValues1">Данные по оси Y для первого набора столбцов</param>
		/// <param name="YValues2">Данные по оси Y для второго набора столбцов</param>
		/// <param name="YValues3">Данные по оси Y для третьего набора столбцов</param>
		private static void CreateBars (GraphPane pane, 
			double[] XValues, 
			double[] YValues1, double[] YValues2, double[] YValues3)
		{
			pane.CurveList.Clear ();

			// Создадим три гистограммы
			pane.AddBar ("", XValues, YValues1, Color.Blue);
			pane.AddBar ("", XValues, YValues2, Color.Red);
			pane.AddBar ("", XValues, YValues3, Color.Yellow);
		}
	}
}