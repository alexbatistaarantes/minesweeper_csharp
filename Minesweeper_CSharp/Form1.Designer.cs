namespace Minesweeper_CSharp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label_gameStatus = new Label();
            groupField = new GroupBox();
            SuspendLayout();
            // 
            // label_gameStatus
            // 
            label_gameStatus.AutoSize = true;
            label_gameStatus.Location = new Point(12, 9);
            label_gameStatus.Name = "label_gameStatus";
            label_gameStatus.Size = new Size(0, 15);
            label_gameStatus.TabIndex = 0;
            // 
            // groupField
            // 
            groupField.AutoSize = true;
            groupField.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupField.Location = new Point(12, 27);
            groupField.Name = "groupField";
            groupField.Size = new Size(6, 5);
            groupField.TabIndex = 1;
            groupField.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(253, 236);
            Controls.Add(groupField);
            Controls.Add(label_gameStatus);
            Name = "Form1";
            Text = "Campo Minado";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label_gameStatus;
        private GroupBox group_Field;
        private GroupBox groupField;
    }
}
