using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace ScaleFormat
{
	public partial class Form1 : Form
	{
		public Form1 ()
		{
			InitializeComponent ();

			// ��������� ������� ��� X
			SetAxisProperties ();

			DrawGraph ();
		}

		/// <summary>
		/// ��������� ������� ��� X
		/// </summary>
		void SetAxisProperties ()
		{
			GraphPane pane = zedGraph.GraphPane;

			// ��������� ��� �������� �����, ������ 5
			pane.XAxis.Scale.MajorStep = 5;

			// ������� �������� ����� �����, ����� �� ������ ���������
			pane.XAxis.Scale.FontSpec.Size = 10;
			
			// ���������� �� �������, ������� ����� ���������� ��� ������ ������ ������� �� ���
			pane.XAxis.ScaleFormatEvent += new Axis.ScaleFormatHandler (XAxis_ScaleFormatEvent);	
		}


		/// <summary>
		/// �����, ������� ����������, ����� ���� ���������� ��������� ����� �� ���
		/// </summary>
		/// <param name="pane">��������� �� ������� GraphPane</param>
		/// <param name="axis">��������� �� ���</param>
		/// <param name="val">��������, ������� ���� ����������</param>
		/// <param name="index">���������� ����� ������� �������</param>
		/// <returns>����� ������ ������� ������, ������� ����� ������������ ��� ������ ������</returns>
		string XAxis_ScaleFormatEvent (GraphPane pane, Axis axis, double val, int index)
		{
			if (val % 10 == 0)
			{
				// ���� ������� �������� ������ 10, �� ������� ��� � ���������� ������
				return string.Format ("[{0}]", val);
			}
			else
			{
				// ��������� ����� ������ ����������� � ������
				return val.ToString ();
			}
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
			// ������� ������ ��� ���������
			GraphPane pane = zedGraph.GraphPane;

			// ������� ������ ������
			pane.CurveList.Clear ();

			// �������� ������ �����
			PointPairList list = new PointPairList ();

			double xmin = -29;
			double xmax = 29;

			// ��������� ������ �����
			for (double x = xmin; x <= xmax; x += 0.01)
			{
				// ������� � ������ �����
				list.Add (x, f (x));
			}

			// �������� ������
			LineItem myCurve = pane.AddCurve ("", list, Color.Blue, SymbolType.None);

			// �������� ������ �� ����. 
			zedGraph.AxisChange ();

			// ��������� ������
			zedGraph.Invalidate ();
		}
	}
}