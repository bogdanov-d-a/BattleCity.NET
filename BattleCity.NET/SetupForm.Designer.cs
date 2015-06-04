namespace BattleCity.NET
{
    partial class SetupForm
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.dllListBox = new System.Windows.Forms.ListBox();
            this.addButton = new System.Windows.Forms.Button();
            this.cfgButton = new System.Windows.Forms.Button();
            this.startGameButton = new System.Windows.Forms.Button();
            this.startTournamentButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "dll";
            this.openFileDialog.Filter = "DLL files|*.dll";
            this.openFileDialog.Multiselect = true;
            this.openFileDialog.Title = "Choose DLL files";
            // 
            // dllListBox
            // 
            this.dllListBox.FormattingEnabled = true;
            this.dllListBox.Location = new System.Drawing.Point(12, 12);
            this.dllListBox.Name = "dllListBox";
            this.dllListBox.Size = new System.Drawing.Size(318, 212);
            this.dllListBox.Sorted = true;
            this.dllListBox.TabIndex = 0;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(12, 230);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // cfgButton
            // 
            this.cfgButton.Location = new System.Drawing.Point(93, 230);
            this.cfgButton.Name = "cfgButton";
            this.cfgButton.Size = new System.Drawing.Size(75, 23);
            this.cfgButton.TabIndex = 2;
            this.cfgButton.Text = "Options";
            this.cfgButton.UseVisualStyleBackColor = true;
            this.cfgButton.Click += new System.EventHandler(this.cfgButton_Click);
            // 
            // startGameButton
            // 
            this.startGameButton.Location = new System.Drawing.Point(174, 230);
            this.startGameButton.Name = "startGameButton";
            this.startGameButton.Size = new System.Drawing.Size(75, 23);
            this.startGameButton.TabIndex = 3;
            this.startGameButton.Text = "Game";
            this.startGameButton.UseVisualStyleBackColor = true;
            this.startGameButton.Click += new System.EventHandler(this.startGameButton_Click);
            // 
            // startTournamentButton
            // 
            this.startTournamentButton.Location = new System.Drawing.Point(255, 230);
            this.startTournamentButton.Name = "startTournamentButton";
            this.startTournamentButton.Size = new System.Drawing.Size(75, 23);
            this.startTournamentButton.TabIndex = 4;
            this.startTournamentButton.Text = "Tournament";
            this.startTournamentButton.UseVisualStyleBackColor = true;
            this.startTournamentButton.Click += new System.EventHandler(this.startTournamentButton_Click);
            // 
            // SetupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 263);
            this.Controls.Add(this.startTournamentButton);
            this.Controls.Add(this.startGameButton);
            this.Controls.Add(this.cfgButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.dllListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "SetupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game setup";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ListBox dllListBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button cfgButton;
        private System.Windows.Forms.Button startGameButton;
        private System.Windows.Forms.Button startTournamentButton;
    }
}