using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace TextBackground
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

			DrawCurve (pane);


			// *** Выведем текст с фоном по умолчанию (с белым фоном) ***
			TextObj text1 = new TextObj ("Text 1", 0.0, 0.9);

			// Отключим рамку вокруг текста
			text1.FontSpec.Border.IsVisible = false;


			// *** Выведем текст с прозрачным фоном ***
			TextObj text2 = new TextObj ("Text 2", 0.0, 0.8);

			// Отключим рамку вокруг текста
			text2.FontSpec.Border.IsVisible = false;

			// Используем конструктор класса Fill без параметров,
			// чтобы фон был прозрачным
			text2.FontSpec.Fill = new Fill ();


			// *** Выведем текст с монотонным фоном ***
			TextObj text3 = new TextObj ("Text 3", 0.0, 0.7);

			// Отключим рамку вокруг текста
			text3.FontSpec.Border.IsVisible = false;

			// Конструктору класса Fill передаем экземпляр класса Color
			text3.FontSpec.Fill = new Fill (Color.Yellow);


			// *** Выведем текст с градиентным фоном ***
			TextObj text4 = new TextObj ("Text 4", 0.0, 0.6);

			// Отключим рамку вокруг текста
			text4.FontSpec.Border.IsVisible = false;

			// Конструктору класса Fill передаем экземпляр класса Color
			text4.FontSpec.Fill = new Fill (Color.Yellow, Color.Red);
			

			// Добавим текстовые объекты в список отображаемых объектов
			pane.GraphObjList.Add (text1);
			pane.GraphObjList.Add (text2);
			pane.GraphObjList.Add (text3);
			pane.GraphObjList.Add (text4);

			// Обновляем график
			zedGraph.Invalidate ();
		}

		private double f (double x)
		{
			if (x == 0)
			{
				return 1;
			}

			return Math.Sin (x) / x;
		}

		private void DrawCurve (GraphPane pane)
		{
			pane.CurveList.Clear ();

			PointPairList list = new PointPairList ();

			double xmin = -50;
			double xmax = 50;

			// Заполняем список точек
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				list.Add (x, f (x));
			}
			LineItem myCurve = pane.AddCurve ("Sinc", list, Color.Blue, SymbolType.None);

			zedGraph.AxisChange ();
		}
	}
}