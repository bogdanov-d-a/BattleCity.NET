namespace BattleCity.NET
{
    partial class ConfigForm
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
            this.reloadTimeLabel = new System.Windows.Forms.Label();
            this.medRateLabel = new System.Windows.Forms.Label();
            this.reloadTimeValue = new System.Windows.Forms.NumericUpDown();
            this.medRateValue = new System.Windows.Forms.NumericUpDown();
            this.disableGamePbCb = new System.Windows.Forms.CheckBox();
            this.disableSidePbCb = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.reloadTimeValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.medRateValue)).BeginInit();
            this.SuspendLayout();
            // 
            // reloadTimeLabel
            // 
            this.reloadTimeLabel.AutoSize = true;
            this.reloadTimeLabel.Location = new System.Drawing.Point(12, 9);
            this.reloadTimeLabel.Name = "reloadTimeLabel";
            this.reloadTimeLabel.Size = new System.Drawing.Size(63, 13);
            this.reloadTimeLabel.TabIndex = 0;
            this.reloadTimeLabel.Text = "Reload time";
            // 
            // medRateLabel
            // 
            this.medRateLabel.AutoSize = true;
            this.medRateLabel.Location = new System.Drawing.Point(12, 35);
            this.medRateLabel.Name = "medRateLabel";
            this.medRateLabel.Size = new System.Drawing.Size(112, 13);
            this.medRateLabel.TabIndex = 1;
            this.medRateLabel.Text = "MedChest appear rate";
            // 
            // reloadTimeValue
            // 
            this.reloadTimeValue.Location = new System.Drawing.Point(130, 9);
            this.reloadTimeValue.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.reloadTimeValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.reloadTimeValue.Name = "reloadTimeValue";
            this.reloadTimeValue.Size = new System.Drawing.Size(120, 20);
            this.reloadTimeValue.TabIndex = 2;
            this.reloadTimeValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // medRateValue
            // 
            this.medRateValue.Location = new System.Drawing.Point(130, 35);
            this.medRateValue.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.medRateValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.medRateValue.Name = "medRateValue";
            this.medRateValue.Size = new System.Drawing.Size(120, 20);
            this.medRateValue.TabIndex = 3;
            this.medRateValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // disableGamePbCb
            // 
            this.disableGamePbCb.AutoSize = true;
            this.disableGamePbCb.Location = new System.Drawing.Point(15, 61);
            this.disableGamePbCb.Name = "disableGamePbCb";
            this.disableGamePbCb.Size = new System.Drawing.Size(167, 17);
            this.disableGamePbCb.TabIndex = 4;
            this.disableGamePbCb.Text = "Disable in-game progress bars";
            this.disableGamePbCb.UseVisualStyleBackColor = true;
            // 
            // disableSidePbCb
            // 
            this.disableSidePbCb.AutoSize = true;
            this.disableSidePbCb.Location = new System.Drawing.Point(15, 84);
            this.disableSidePbCb.Name = "disableSidePbCb";
            this.disableSidePbCb.Size = new System.Drawing.Size(149, 17);
            this.disableSidePbCb.TabIndex = 5;
            this.disableSidePbCb.Text = "Disable side progress bars";
            this.disableSidePbCb.UseVisualStyleBackColor = true;
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 114);
            this.Controls.Add(this.disableSidePbCb);
            this.Controls.Add(this.disableGamePbCb);
            this.Controls.Add(this.medRateValue);
            this.Controls.Add(this.reloadTimeValue);
            this.Controls.Add(this.medRateLabel);
            this.Controls.Add(this.reloadTimeLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "ConfigForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuration";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.reloadTimeValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.medRateValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label reloadTimeLabel;
        private System.Windows.Forms.Label medRateLabel;
        private System.Windows.Forms.NumericUpDown reloadTimeValue;
        private System.Windows.Forms.NumericUpDown medRateValue;
        private System.Windows.Forms.CheckBox disableGamePbCb;
        private System.Windows.Forms.CheckBox disableSidePbCb;
    }
}