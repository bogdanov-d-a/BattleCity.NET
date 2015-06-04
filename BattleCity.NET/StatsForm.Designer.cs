namespace BattleCity.NET
{
    partial class StatsForm
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.heals = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shots = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hits = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dmgTaken = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hitsTaken = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.heals,
            this.shots,
            this.hits,
            this.dmgTaken,
            this.hitsTaken});
            this.dataGridView.Location = new System.Drawing.Point(13, 13);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(671, 419);
            this.dataGridView.TabIndex = 0;
            // 
            // name
            // 
            this.name.Frozen = true;
            this.name.HeaderText = "Name";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // heals
            // 
            this.heals.Frozen = true;
            this.heals.HeaderText = "Heals";
            this.heals.Name = "heals";
            this.heals.ReadOnly = true;
            // 
            // shots
            // 
            this.shots.Frozen = true;
            this.shots.HeaderText = "Shots";
            this.shots.Name = "shots";
            this.shots.ReadOnly = true;
            // 
            // hits
            // 
            this.hits.Frozen = true;
            this.hits.HeaderText = "Hits";
            this.hits.Name = "hits";
            this.hits.ReadOnly = true;
            // 
            // dmgTaken
            // 
            this.dmgTaken.Frozen = true;
            this.dmgTaken.HeaderText = "DmgTaken";
            this.dmgTaken.Name = "dmgTaken";
            this.dmgTaken.ReadOnly = true;
            // 
            // hitsTaken
            // 
            this.hitsTaken.Frozen = true;
            this.hitsTaken.HeaderText = "HitsTaken";
            this.hitsTaken.Name = "hitsTaken";
            this.hitsTaken.ReadOnly = true;
            // 
            // StatsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 444);
            this.Controls.Add(this.dataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "StatsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Statistics";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn heals;
        private System.Windows.Forms.DataGridViewTextBoxColumn shots;
        private System.Windows.Forms.DataGridViewTextBoxColumn hits;
        private System.Windows.Forms.DataGridViewTextBoxColumn dmgTaken;
        private System.Windows.Forms.DataGridViewTextBoxColumn hitsTaken;
    }
}