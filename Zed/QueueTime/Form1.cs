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
		/// ������������ ������ �������
		/// </summary>
		int _capacity = 30;

		/// <summary>
		/// ����� ������ ������
		/// </summary>
		List<double> _data;

		/// <summary>
		/// ��� ��������� �������� ������ �� �������
		/// </summary>
		Random _rnd = new Random ();

		// �������� ��������� ������ �� ���������
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
			// �������� ����� ��������
			double newValue = _rnd.NextDouble () * (_ymax - _ymin) + _ymin;

			// ������� ��� � ����� ������
			_data.Add (newValue);

			// ������ ������ ������� � ������ ������, 
			// ���� ��������� ������������ �������
			if (_data.Count > _capacity)
			{
				_data.RemoveAt (0);
			}

			DrawGraph ();
		}

		private void DrawGraph ()
		{
			// ������� ������ ��� ���������
			GraphPane pane = zedGraph.GraphPane;

			// ������� ������ ������ �� ��� ������, ���� �� ����� ������� ��� ���� ����������
			pane.CurveList.Clear ();

			// �������� ������ �����
			PointPairList list = new PointPairList ();

			// ��������, ��� ���� ������
			double xmin = 0;
			double xmax = _capacity;

			// ���������� ����� ��������� ������� �� �����������
			double dx = 1.0;

			double curr_x = 0;

			// ��������� ������ �����
			foreach (double val in _data)
			{
				list.Add (curr_x, val);
				curr_x += dx;
			}

			// ������� ������ ������ �� ������� �������� (������)
			pane.CurveList.Clear ();
			LineItem myCurve = pane.AddCurve ("Random Value", list, Color.Blue, SymbolType.None);


			// ������������� ������������ ��� �������� �� ��� X
			pane.XAxis.Scale.Min = xmin;
			pane.XAxis.Scale.Max = xmax;

			// ������������� ������������ ��� �������� �� ��� Y
			pane.YAxis.Scale.Min = _ymin;
			pane.YAxis.Scale.Max = _ymax;

			// �������� ����� AxisChange (), ����� �������� ������ �� ����. 
			// � ��������� ������ �� ������� ����� �������� ������ ����� �������, 
			// ������� ��������� � ��������� �� ����, ������������� �� ���������
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}
	}
}
