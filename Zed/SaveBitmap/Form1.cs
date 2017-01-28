using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

using ZedGraph;


namespace SaveBitmap
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			// Подпишемся на события кнопок
			saveAsBitmapBtn.Click += new EventHandler (saveAsBitmapBtn_Click);
			savePane1.Click += new EventHandler (savePane1_Click);
			savePane2.Click += new EventHandler (savePane2_Click);

			DrawAllGraph ();
		}

		/// <summary>
		/// Сохранить картинку панели 1
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void savePane1_Click (object sender, EventArgs e)
		{
			SavePaneImage (0);
		}

		/// <summary>
		/// Сохранить картинку панели 2
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void savePane2_Click (object sender, EventArgs e)
		{
			SavePaneImage (1);
		}


		/// <summary>
		/// Сохранить картинку панели, указанной с помощью индекса
		/// </summary>
		/// <param name="index"></param>
		private void SavePaneImage (int index)
		{
			// ДИалог выбора имени файла создаем вручную
			SaveFileDialog dlg = new SaveFileDialog ();
			dlg.Filter = "*.png|*.png|*.jpg; *.jpeg|*.jpg;*.jpeg|*.bmp|*.bmp|Все файлы|*.*";

			if (dlg.ShowDialog () == DialogResult.OK)
			{
				// Получием панель по ее индексу
				GraphPane pane = zedGraph.MasterPane.PaneList[index];

				// Получаем картинку, соответствующую панели
				Bitmap bmp = pane.GetImage ();

				// Сохраняем картинку средствами класса Bitmap
				// Формат картинки выбирается исходя из имени выбранного файла
				if (dlg.FileName.EndsWith (".png"))
				{
					bmp.Save (dlg.FileName, ImageFormat.Png);
				}
				else if (dlg.FileName.EndsWith (".jpg") || dlg.FileName.EndsWith (".jpeg"))
				{
					bmp.Save (dlg.FileName, ImageFormat.Jpeg);
				}
				else if (dlg.FileName.EndsWith (".bmp"))
				{
					bmp.Save (dlg.FileName, ImageFormat.Bmp);
				}
				else
				{
					bmp.Save (dlg.FileName);
				}
			}
		}


		/// <summary>
		/// Обработчик нажатия на кнопку "Сохранить все графики"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void saveAsBitmapBtn_Click (object sender, EventArgs e)
		{
			// Для сохранения картинки всего компонента ZedGraphControl 
			// достаточно вызвать метод SaveAsBitmap().
			// Создание и показ диалога выбора имени файла возьмет на себя ZedGraphControl
			zedGraph.SaveAsBitmap ();
		}


		/// <summary>
		/// Создать два графика с помощью MasterPane
		/// </summary>
		private void DrawAllGraph ()
		{
			MasterPane masterPane = zedGraph.MasterPane;
			masterPane.PaneList.Clear ();

			int count = 2;

			for (int i = 0; i < count; i++)
			{
				// Создаем экземпляр класса GraphPane, представляющий собой один график
				GraphPane pane = new GraphPane ();

				// Нарисуем график на панели
				DrawSingleGraph (pane);

				// Добавим график в MasterPane
				masterPane.Add (pane);
			}

			// Будем размещать добавленные графики в MasterPane
			using (Graphics g = CreateGraphics ())
			{
				// Графики будут размещены в две строки, 
				masterPane.SetLayout (g, PaneLayout.SingleColumn);
			}

			// Обновим оси и перерисуем график
			zedGraph.AxisChange ();
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


		private void DrawSingleGraph (GraphPane pane)
		{
			// Очистим список кривых
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
			pane.AddCurve ("", list, Color.Blue, SymbolType.None);
		}
	}
}