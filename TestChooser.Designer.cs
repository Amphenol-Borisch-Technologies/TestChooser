﻿namespace TestChooser {
    partial class TestChooser {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.progressBarWorking = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // progressBarWorking
            // 
            this.progressBarWorking.Location = new System.Drawing.Point(22, 52);
            this.progressBarWorking.Name = "progressBarWorking";
            this.progressBarWorking.Size = new System.Drawing.Size(541, 42);
            this.progressBarWorking.TabIndex = 0;
            // 
            // TestChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 165);
            this.Controls.Add(this.progressBarWorking);
            this.Name = "TestChooser";
            this.Text = "Working...";
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBarWorking;
    }
}

