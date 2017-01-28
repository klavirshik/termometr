using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace Fill
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            DrawGraph();
        }

        private double f(double x)
        {
            return Math.Sin(x) * Math.Cos (2.0 * x);
        }

        private void DrawGraph()
        {
            // Получим панель для рисования
            GraphPane pane = zedGraph.GraphPane;

            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            pane.CurveList.Clear();

            // Создадим список точек
            PointPairList list = new PointPairList();

            double xmin = 0;
            double xmax = 7 * Math.PI;

            // Заполняем список точек
            for (double x = xmin; x <= xmax; x += 0.01)
            {
                // добавим в список точку
                list.Add(x, f(x));
            }

            // Создадим кривую
            LineItem myCurve = pane.AddCurve("", list, Color.Black, SymbolType.None);

			// !!! Установим заливку для кривой
			// Используем градиентную заливку от красного цвета до голубого через желтый
			// Последний параметр задает угол наклона градиента
			myCurve.Line.Fill = new ZedGraph.Fill (Color.Red, Color.Yellow, Color.Blue, 90.0f);

            // Вызываем метод AxisChange(), чтобы обновить данные об осях. 
            zedGraph.AxisChange();

            // Обновляем график
            zedGraph.Invalidate();
        }
    }
}