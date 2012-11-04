using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MatrixOperations;

//using MatrixOperations;

namespace MatrixTransposition
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			m_richTextBox.Text = "";
			var r = new Random();
			//var m1 = new Matrix(2, 2);
			//m1[0, 0] = 0; m1[0, 1] = 1;
			//m1[1, 0] = 1; m1[1, 1] = 0;
			//var m1 = new Matrix(3, 3);
			//m1[0, 0] = 0; m1[0, 1] = -2; m1[0, 2] = 4;
			//m1[1, 0] = 0; m1[1, 1] = -4; m1[1, 2] = -1; 
			//m1[2, 0] = -4; m1[2, 1] = -3; m1[2, 2] = 3; 
			//var m1 = new Matrix(5, 5);
			//m1[0, 0] = 2; m1[0, 1] = 1; m1[0, 2] = 4; m1[0, 3] = -3; m1[0, 4] = 0.000001;
			//m1[1, 0] = 1; m1[1, 1] = -1; m1[1, 2] = 2; m1[1, 3] = 2; m1[1, 4] = 1;
			//m1[2, 0] = 3; m1[2, 1] = 0; m1[2, 2] = 6; m1[2, 3] = -1; m1[2, 4] = 1;
			//m1[3, 0] = 1; m1[3, 1] = 1; m1[3, 2] = 1; m1[3, 3] = 1; m1[3, 4] = 1;
			//m1[4, 0] = 2; m1[4, 1] = 4; m1[4, 2] = -1; m1[4, 3] = 0; m1[4, 4] = 2;

			var m1 = new Matrix(4, 4);
			m1[0, 0] = 2; m1[0, 1] = 1; m1[0, 2] = 4; m1[0, 3] = 0;
			m1[1, 0] = 1; m1[1, 1] = -1; m1[1, 2] = 2; m1[1, 3] = 1.00001;
			m1[2, 0] = 3; m1[2, 1] = 0; m1[2, 2] = 6; m1[2, 3] = 1;
			m1[3, 0] = 1; m1[3, 1] = 1; m1[3, 2] = 1; m1[3, 3] = 0;

			//for (int i = 0; i < m1.RowsCount; i++)
			//{
			//    for (int j = 0; j < m1.ColumnsCount; j++)
			//    {
			//        m1[i, j] = r.Next(-5, 5);
			//    }
			//}

			m_richTextBox.Text += "A\n";
			m_richTextBox.Text += m1.ToString();

			Matrix minv;
			try
			{
				minv = m1.Copy().Invert();
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
				return;
			}
			
			m_richTextBox.Text += "B\n";
			m_richTextBox.Text += minv.ToString();
			m_richTextBox.Text += "AB=\n";
			m_richTextBox.Text += m1.Multiply(minv).ToString();

		}
	}
}
