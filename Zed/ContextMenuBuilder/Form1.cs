using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;


namespace ContextMenuBuilder
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			// !!! Подпишемся на событие, которое будет возникать перед тем, 
			// как будет показано контекстное меню.
			zedGraph.ContextMenuBuilder += 
				new ZedGraphControl.ContextMenuBuilderEventHandler (zedGraph_ContextMenuBuilder);

			DrawGraph ();
		}


		/// <summary>
		/// Обработчик события, который вызывается, перед показом контекстного меню
		/// </summary>
		/// <param name="sender">Компонент ZedGraph</param>
		/// <param name="menuStrip">Контекстное меню, которое будет показано</param>
		/// <param name="mousePt">Координаты курсора мыши</param>
		/// <param name="objState">Состояние контекстного меню. Описывает объект, на который кликнули.</param>
		void zedGraph_ContextMenuBuilder (ZedGraphControl sender, 
			ContextMenuStrip menuStrip, 
			Point mousePt, 
			ZedGraphControl.ContextMenuObjectState objState)
		{
			// !!! 
			// Переименуем (переведем на русский язык) некоторые пункты контекстного меню
			menuStrip.Items[0].Text = "Копировать";
			menuStrip.Items[1].Text = "Сохранить как картинку…";
			menuStrip.Items[2].Text = "Параметры страницы…";
			menuStrip.Items[3].Text = "Печать…";
			menuStrip.Items[4].Text = "Показывать значения в точках…";
			menuStrip.Items[7].Text = "Установить масштаб по умолчанию";

			// Некоторые пункты удалим
			menuStrip.Items.RemoveAt (5);
			menuStrip.Items.RemoveAt (5);

			// Добавим свой пункт меню
			ToolStripItem newMenuItem = new ToolStripMenuItem ("Этот пункт меню ничего не делает");
			menuStrip.Items.Add (newMenuItem);
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
			GraphPane pane = zedGraph.GraphPane;

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
			LineItem myCurve = pane.AddCurve ("", list, Color.Blue, SymbolType.None);

			// Вызываем метод AxisChange (), чтобы обновить данные об осях. 
			zedGraph.AxisChange ();

			// Обновляем график
			zedGraph.Invalidate ();
		}
	}
}