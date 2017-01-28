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
            // ������� ������ ��� ���������
            GraphPane pane = zedGraph.GraphPane;

            // ������� ������ ������ �� ��� ������, ���� �� ����� ������� ��� ���� ����������
            pane.CurveList.Clear();

            // �������� ������ �����
            PointPairList list = new PointPairList();

            double xmin = 0;
            double xmax = 7 * Math.PI;

            // ��������� ������ �����
            for (double x = xmin; x <= xmax; x += 0.01)
            {
                // ������� � ������ �����
                list.Add(x, f(x));
            }

            // �������� ������
            LineItem myCurve = pane.AddCurve("", list, Color.Black, SymbolType.None);

			// !!! ��������� ������� ��� ������
			// ���������� ����������� ������� �� �������� ����� �� �������� ����� ������
			// ��������� �������� ������ ���� ������� ���������
			myCurve.Line.Fill = new ZedGraph.Fill (Color.Red, Color.Yellow, Color.Blue, 90.0f);

            // �������� ����� AxisChange(), ����� �������� ������ �� ����. 
            zedGraph.AxisChange();

            // ��������� ������
            zedGraph.Invalidate();
        }
    }
}