using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace BarHorizontal
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
			GraphPane pane = zedGraph.GraphPane;
			pane.CurveList.Clear ();

			// Количество столбцов
			int itemscount = 9;

			// Сгенерируем данные для оси X (длин столбцов)
			double[] barLength = GenerateData (itemscount);

			double[] barPosition = new double[itemscount];

			// Заполним данные по оси Y (положения столбцов)
			for (int i = 0; i < itemscount; i++)
			{
				barPosition[i] = i + 1;
			}

			// !!! Создадим гистограмму. 
			// Обратите внимание на порядок следования массивов: 
			// сначала идут данные по оси X (длины столцов), потом по оси Y (положения столбцов)
			// Для вертикальных гистограмм значения по осям X и Y имеют противоположные значения.
			pane.AddBar ("", barLength, barPosition, Color.Blue);

			// Этот параметр указывает, что базовой осью для гистограммы будет ось Y,
			// то есть положения столбцов соответствуют значениям по оси Y.
			pane.BarSettings.Base = BarBase.Y;

			// Обновим данные об осях
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
		

		/// <summary>
		/// Сгенерировать случайные данные для графика
		/// </summary>
		/// <param name="itemscount">Количество столбцов</param>
		/// <returns></returns>
		private double[] GenerateData (int itemscount)
		{
			Random rnd = new Random ();

			double[] values = new double[itemscount];

			// Заполним данные
			for (int i = 0; i < itemscount; i++)
			{
				values[i] = rnd.NextDouble ();
			}

			return values;
		}
	}
}