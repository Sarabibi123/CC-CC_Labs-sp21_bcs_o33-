using System;
using System.Windows.Forms;

namespace Lab1_Task1

{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnFunction_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                double input = double.Parse(txtInput.Text);
                double result = 0;

                switch (btn.Tag.ToString())
                {
                    case "sin":
                        result = Math.Sin(input);
                        break;
                    case "cos":
                        result = Math.Cos(input);
                        break;
                    case "tan":
                        result = Math.Tan(input);
                        break;
                    case "log":
                        result = Math.Log10(input);
                        break;
                }

                lblResult.Text = "Result: " + result.ToString();
            }
        }
    }
}
