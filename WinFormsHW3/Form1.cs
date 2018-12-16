using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Reflection;
using WinFormsHW3.Functions;
using WinFormsHW3.Interfaces;

namespace WinFormsHW3
{
    public partial class Form1 : Form
    {
        double xStart;
        double xEnd;
        int dpi;
        int decimPlaces;
        IFunction fSinx;
        IFunction f2cosx;
        bool createSinx = false;
        bool create2Cosx = false;
        public Form1()
        {
            try
            {
                InitializeComponent();
                button1.Text = "In one area";
                button1.Font = new Font("Microsoft Sans Serif", 8);
                button2.Text = "Singly";
                button2.Font = new Font("Microsoft Sans Serif", 8);
                checkBox1.Text = "sinx";
                checkBox1.Font = new Font("Microsoft Sans Serif", 8);
                checkBox2.Text = "2cosx";
                checkBox2.Font = new Font("Microsoft Sans Serif", 8);
                checkBox3.Text = "Change parameters";
                checkBox3.Font = new Font("Microsoft Sans Serif", 8);
                this.Text = "Function graph";
                groupBox1.Controls.Add(label1);
                groupBox1.Controls.Add(label2);
                groupBox1.Controls.Add(label3);
                groupBox1.Controls.Add(label4);
                groupBox1.Controls.Add(textBox1);
                groupBox1.Controls.Add(textBox2);
                groupBox1.Controls.Add(textBox3);
                groupBox1.Controls.Add(textBox4);
                groupBox1.Visible = false;
                groupBox1.Font = new Font("Microsoft Sans Serif", 8);
                chart1.Series.Add("2cosx");
                chart1.Series[0].Name = "sinx";
                chart1.ChartAreas[0].Name = chart1.Series[0].Name;
                chart1.Series[0].ChartType = SeriesChartType.Spline;
                chart1.Series[1].ChartType = SeriesChartType.Spline;
                chart1.Series[0].Color = Color.Blue;
                chart1.Series[1].Color = Color.Red;
                xStart = Convert.ToDouble(textBox1.Text);
                xEnd = Convert.ToDouble(textBox2.Text);
                dpi = Convert.ToInt32(textBox3.Text);
                decimPlaces = Convert.ToInt32(textBox4.Text);
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong! "+e.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }
        // sinx
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!createSinx)
            {
                fSinx = new StandartFunction(xStart, xEnd, dpi);
                createSinx = true;
            }
        }
        // 2cosx
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!create2Cosx)
            {
                f2cosx = new F2cosx(xStart, xEnd, dpi);
                create2Cosx = true;
            }
        }
        // Change parameters
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if(groupBox1.Visible)
                groupBox1.Visible = false;
            else
                groupBox1.Visible = true;
        }
        // In one area
        private void button1_Click(object sender, EventArgs e)
        {
            if (chart1.ChartAreas.Select(x=>x.Name).Contains(chart1.Series[1].Name))
            {
                chart1.ChartAreas[chart1.Series[1].Name].Visible = false;
                chart1.Series[1].ChartArea = chart1.Series[0].Name;
            }
            if (checkBox1.Checked && checkBox2.Checked)
            {
                for (int i = 0; i < chart1.Series.Count; i++)
                {
                    chart1.Series[i].IsVisibleInLegend = true;
                }
                CalculateGraph(0, chart1.Series[0].Name, ref fSinx);
                CalculateGraph(1, chart1.Series[0].Name, ref f2cosx);
            }
            else if (checkBox1.Checked && !checkBox2.Checked)
            {
                CalculateGraph(0, chart1.Series[0].Name, ref fSinx);
                chart1.Series[0].IsVisibleInLegend = true;
                for (int i = 1; i < chart1.Series.Count; i++)
                {
                    chart1.Series[i].IsVisibleInLegend = false;
                    chart1.Series[i].Points.Clear();
                }
            }
            else if (!checkBox1.Checked && checkBox2.Checked)
            {
                CalculateGraph(1, chart1.Series[0].Name, ref f2cosx);
                chart1.Series[1].IsVisibleInLegend = true;
                for (int i = 0; i < 1; i++)
                {
                    chart1.Series[i].IsVisibleInLegend = false;
                    chart1.Series[i].Points.Clear();
                }
            }
            else
            {
                MessageBox.Show("Please enter a function!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        // Singly
        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked && checkBox2.Checked)
            {
                if (!chart1.ChartAreas.Select(x => x.Name).Contains(chart1.Series[1].Name))
                {
                    chart1.ChartAreas.Add(chart1.Series[1].Name);
                }
                chart1.ChartAreas[chart1.Series[1].Name].Visible = true;
                chart1.Series[1].ChartArea = chart1.Series[1].Name;
                CalculateGraph(0, chart1.Series[0].Name, ref fSinx);
                CalculateGraph(1, chart1.Series[1].Name, ref f2cosx);
            }
            else
            {
                MessageBox.Show("Please select all functions!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Xstart
        private void textBox1_Leave(object sender, EventArgs e)
        {
            ChangeTextBox(1);
        }

        // Xend
        private void textBox2_Leave(object sender, EventArgs e)
        {
            ChangeTextBox(2);
        }

        // dpi
        private void textBox3_Leave(object sender, EventArgs e)
        {
            ChangeTextBox(3);
        }

        // decimal places
        private void textBox4_Leave(object sender, EventArgs e)
        {
            ChangeTextBox(4);
        }

        private void CalculateGraph(int seriesNumber, string chartAreasName, ref IFunction function)
        {
            if (seriesNumber < chart1.Series.Count && seriesNumber >= 0)
            {
                (double[] pX, double[] pY) points = (null, null);
                chart1.Series[seriesNumber].Points.Clear();
                function.Xstart = xStart;
                function.Xend = xEnd;
                function.Dpi = dpi;
                switch (seriesNumber)
                {
                    case 0:
                        {
                            points = function.GetPoints(Math.Sin);
                            break;
                        }
                    case 1:
                        {
                            points = ((F2cosx)function).GetPoints(Math.Cos, 2);
                            break;
                        }
                }
                for (int i = 0; i < points.pX.Length; i++)
                {
                    chart1.Series[seriesNumber].Points.AddXY(points.pX[i], points.pY[i]);
                }
                chart1.ChartAreas[chartAreasName].AxisX.Minimum = xStart;
                chart1.ChartAreas[chartAreasName].AxisX.Maximum = xEnd;
                chart1.ChartAreas[chartAreasName].AxisY.LabelStyle.Format = "f" + decimPlaces;
                chart1.ChartAreas[chartAreasName].AxisX.LabelStyle.Format = "f" + decimPlaces;
            }
            else
            {
                MessageBox.Show("Wrong seriesNumber in CalculateGraph()! ", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ChangeTextBox(int numberTextBox, int dpiMin = 5)
        {
            try
            {
                double start = xStart;
                double end = xEnd;
                int dpi_;
                int decPl;
                switch (numberTextBox)
                {
                    case 1:
                        start = Convert.ToDouble(textBox1.Text);
                        if (start < xEnd)
                            xStart = start;
                        else
                            textBox1.Text = xStart.ToString();
                        break;
                    case 2:
                        end = Convert.ToDouble(textBox2.Text);
                        if (start < end)
                            xEnd = end;
                        else
                            textBox2.Text = xEnd.ToString();
                        break;
                    case 3:
                        dpi_ = Convert.ToInt32(textBox3.Text);
                        if (dpi_ > dpiMin && dpi_ > 0)
                            dpi = dpi_;
                        else
                        {
                            MessageBox.Show("dpi should be more "+ dpiMin, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBox3.Text = dpi.ToString();
                        }
                        break;
                    case 4:
                        decPl = Convert.ToInt32(textBox4.Text);
                        if (decPl >= 0 && decPl < 10)
                            decimPlaces = decPl;
                        else
                        {
                            MessageBox.Show("The number of decimal places must be between 0 and 10!", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textBox4.Text = decimPlaces.ToString();
                        }
                        break;
                }
                if (start >= end)
                {
                    MessageBox.Show("Xstart should be less Xend", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Parameter must be a number", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = xStart.ToString();
                textBox2.Text = xEnd.ToString();
                textBox3.Text = dpi.ToString();
                textBox4.Text = decimPlaces.ToString();
            }
        }
    }
}
