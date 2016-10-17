namespace Interpreter
{
    partial class GUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.command = new System.Windows.Forms.TextBox();
            this.runButton = new System.Windows.Forms.Button();
            this.Output = new System.Windows.Forms.Label();
            this.variablesValuesArea = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // command
            // 
            this.command.Location = new System.Drawing.Point(13, 13);
            this.command.Name = "command";
            this.command.Size = new System.Drawing.Size(400, 31);
            this.command.TabIndex = 0;
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(432, 13);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(88, 31);
            this.runButton.TabIndex = 1;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.RunButtonClick);
            // 
            // Output
            // 
            this.Output.AutoSize = true;
            this.Output.Location = new System.Drawing.Point(12, 60);
            this.Output.MinimumSize = new System.Drawing.Size(400, 70);
            this.Output.Name = "Output";
            this.Output.Size = new System.Drawing.Size(400, 70);
            this.Output.TabIndex = 2;
            this.Output.Text = "Output";
            // 
            // variablesValuesArea
            // 
            this.variablesValuesArea.AutoSize = true;
            this.variablesValuesArea.Location = new System.Drawing.Point(17, 153);
            this.variablesValuesArea.MinimumSize = new System.Drawing.Size(500, 550);
            this.variablesValuesArea.Name = "variablesValuesArea";
            this.variablesValuesArea.Size = new System.Drawing.Size(500, 550);
            this.variablesValuesArea.TabIndex = 3;
            this.variablesValuesArea.Text = "Variable values";
            // 
            // Form1
            // 
            this.AcceptButton = this.runButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 722);
            this.Controls.Add(this.variablesValuesArea);
            this.Controls.Add(this.Output);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.command);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox command;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Label Output;
        private System.Windows.Forms.Label variablesValuesArea;
    }
}

