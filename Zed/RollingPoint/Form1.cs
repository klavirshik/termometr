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
		/// ���������� ������������ �����
		/// </summary>
		int _capacity = 100;

		/// <summary>
		/// ����� ������ ������
		/// </summary>
		RollingPointPairList _data;

		// ��������� ������������ ���������
		double _amplitude = 5;
		double _freq = 3;
		double _step = 0.1;

		// ������� �������� �� �������
		double _currentx = 0;


		public Form1 ()
		{
			// !!! ������� ������ ������ � ������������ ��������.
			// ��� ������������������ ������� ������ �������� � ������� ����� ���������
			_data = new RollingPointPairList (_capacity);

			InitializeComponent ();
			PrepareGraph ();
		}


		/// <summary>
		/// ����� ���������� �� �������
		/// </summary>
		private void timer_Tick (object sender, EventArgs e)
		{
			// �������� ����� ��������
			double newValue = _amplitude * Math.Sin (_currentx * _freq);

			// !!! ������� ����� ������ � ������
			_data.Add (_currentx, newValue);
			_currentx += _step;

			// ���������� �������� �� ��� X, ������� ����� ���������� �� �������
			double xmin = _currentx - _capacity * _step;
			double xmax = _currentx;

			GraphPane pane = zedGraph.GraphPane;
			pane.XAxis.Scale.Min = xmin;
			pane.XAxis.Scale.Max = xmax;

			// ������� ���
			zedGraph.AxisChange ();

			// ������� ��� ������
			zedGraph.Invalidate ();
		}


		/// <summary>
		/// ���������� � ����������� ������
		/// </summary>
		private void PrepareGraph ()
		{
			// ������� ������ ��� ���������
			GraphPane pane = zedGraph.GraphPane;

			// ������� ������ ������ �� ��� ������, ���� �� ����� ������� ��� ���� ����������
			pane.CurveList.Clear ();

			// ������� ������ ���� ��� ��� �����-���� �����
			LineItem myCurve = pane.AddCurve ("sin (x)", _data, Color.Blue, SymbolType.None);

			// ������������� ������������ ��� �������� �� ��� Y
			pane.YAxis.Scale.Min = -_amplitude;
			pane.YAxis.Scale.Max = _amplitude;

			// �������� ����� AxisChange (), ����� �������� ������ �� ����. 
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}
	}
}
