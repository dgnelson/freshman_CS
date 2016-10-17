using System;
using System.Text;
using System.Windows.Forms;

//
// EECS-214 Spring 2016
//

namespace Interpreter
{
    public partial class GUI : Form
    {
        public GUI()
        {
            InitializeComponent();
        }

        private readonly ListDictionary environment = new ListDictionary();

        private void RunButtonClick(object sender, EventArgs e)
        {
            try
            {
                Output.Text = "Result: "+Interpreter.Run(command.Text, environment);
                UpdateVariables();
            }
            catch (Exception exception)
            {
                Output.Text = "Exception:\n"+exception.Message;
                variablesValuesArea.Text = "Stack trace:\n"+exception.StackTrace;
            }
        }

        private void UpdateVariables()
        {
            var b= new StringBuilder();
            b.Append("Variable values:\n");
            bool gotOne = false;
            foreach (var binding in environment)
            {
                b.AppendFormat("{0}={1}\n", binding.Key, binding.Value);
                gotOne = true;
            }
            variablesValuesArea.Text = !gotOne ? "No variables defined." : b.ToString();
        }
    }
}
