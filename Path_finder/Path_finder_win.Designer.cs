namespace Path_finder
{
    partial class Path_finder_win
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Path_finder_win));
            Map_win = new Panel();
            Aim_point = new Label();
            Map_status = new Label();
            Clear_map_bt = new Button();
            Save_map_bt = new Button();
            Ortogonal_grid_ChBx = new CheckBox();
            Radial_grid_ChBx = new CheckBox();
            Ortogonal_map_ChBx = new CheckBox();
            Radial_map_ChBx = new CheckBox();
            Map_format_lable = new Label();
            Aim_lable = new Label();
            Aim_x = new TextBox();
            Aim_y = new TextBox();
            label_aimX = new Label();
            label_aimY = new Label();
            label_PathCount = new Label();
            PathCount_mode = new ComboBox();
            progressBar1 = new ProgressBar();
            PathCount = new Button();
            Map_win.SuspendLayout();
            SuspendLayout();
            // 
            // Map_win
            // 
            Map_win.BackColor = SystemColors.GradientInactiveCaption;
            Map_win.Controls.Add(Aim_point);
            Map_win.Cursor = Cursors.Cross;
            resources.ApplyResources(Map_win, "Map_win");
            Map_win.Name = "Map_win";
            Map_win.Paint += Map_win_Paint;
            Map_win.MouseDown += Map_win_MouseDown;
            Map_win.MouseMove += Map_win_MouseMove;
            Map_win.MouseUp += Map_win_MouseUp;
            // 
            // Aim_point
            // 
            Aim_point.BackColor = Color.FromArgb(0, 192, 0);
            Aim_point.Cursor = Cursors.Hand;
            Aim_point.FlatStyle = FlatStyle.Flat;
            resources.ApplyResources(Aim_point, "Aim_point");
            Aim_point.Name = "Aim_point";
            Aim_point.MouseDown += Aim_point_MouseDown;
            Aim_point.MouseMove += Aim_point_MouseMove;
            Aim_point.MouseUp += Aim_point_MouseUp;
            // 
            // Map_status
            // 
            resources.ApplyResources(Map_status, "Map_status");
            Map_status.Name = "Map_status";
            // 
            // Clear_map_bt
            // 
            resources.ApplyResources(Clear_map_bt, "Clear_map_bt");
            Clear_map_bt.Name = "Clear_map_bt";
            Clear_map_bt.UseVisualStyleBackColor = true;
            Clear_map_bt.Click += Clear_map_bt_Click;
            // 
            // Save_map_bt
            // 
            resources.ApplyResources(Save_map_bt, "Save_map_bt");
            Save_map_bt.Name = "Save_map_bt";
            Save_map_bt.UseVisualStyleBackColor = true;
            Save_map_bt.Click += Save_map_bt_Click;
            // 
            // Ortogonal_grid_ChBx
            // 
            resources.ApplyResources(Ortogonal_grid_ChBx, "Ortogonal_grid_ChBx");
            Ortogonal_grid_ChBx.Name = "Ortogonal_grid_ChBx";
            Ortogonal_grid_ChBx.UseVisualStyleBackColor = true;
            Ortogonal_grid_ChBx.CheckedChanged += Orthogonal_grid_ChBx_CheckedChanged;
            // 
            // Radial_grid_ChBx
            // 
            resources.ApplyResources(Radial_grid_ChBx, "Radial_grid_ChBx");
            Radial_grid_ChBx.Name = "Radial_grid_ChBx";
            Radial_grid_ChBx.UseVisualStyleBackColor = true;
            Radial_grid_ChBx.CheckedChanged += Radial_grid_ChBx_CheckedChanged;
            // 
            // Ortogonal_map_ChBx
            // 
            resources.ApplyResources(Ortogonal_map_ChBx, "Ortogonal_map_ChBx");
            Ortogonal_map_ChBx.Name = "Ortogonal_map_ChBx";
            Ortogonal_map_ChBx.UseVisualStyleBackColor = true;
            Ortogonal_map_ChBx.CheckedChanged += Orthogonal_map_ChBx_CheckedChanged;
            // 
            // Radial_map_ChBx
            // 
            resources.ApplyResources(Radial_map_ChBx, "Radial_map_ChBx");
            Radial_map_ChBx.Name = "Radial_map_ChBx";
            Radial_map_ChBx.UseVisualStyleBackColor = true;
            Radial_map_ChBx.CheckedChanged += Radial_map_ChBx_CheckedChanged;
            // 
            // Map_format_lable
            // 
            resources.ApplyResources(Map_format_lable, "Map_format_lable");
            Map_format_lable.Name = "Map_format_lable";
            // 
            // Aim_lable
            // 
            resources.ApplyResources(Aim_lable, "Aim_lable");
            Aim_lable.Name = "Aim_lable";
            // 
            // Aim_x
            // 
            resources.ApplyResources(Aim_x, "Aim_x");
            Aim_x.Name = "Aim_x";
            Aim_x.TextChanged += Aim_x_TextChanged;
            Aim_x.KeyDown += Aim_x_KeyDown;
            Aim_x.KeyPress += Aim_x_KeyPress;
            Aim_x.Leave += Aim_x_Leave;
            // 
            // Aim_y
            // 
            resources.ApplyResources(Aim_y, "Aim_y");
            Aim_y.Name = "Aim_y";
            Aim_y.TextChanged += Aim_y_TextChanged;
            Aim_y.KeyDown += Aim_y_KeyDown;
            Aim_y.KeyPress += Aim_y_KeyPress;
            Aim_y.Leave += Aim_y_Leave;
            // 
            // label_aimX
            // 
            resources.ApplyResources(label_aimX, "label_aimX");
            label_aimX.Name = "label_aimX";
            // 
            // label_aimY
            // 
            resources.ApplyResources(label_aimY, "label_aimY");
            label_aimY.Name = "label_aimY";
            // 
            // label_PathCount
            // 
            resources.ApplyResources(label_PathCount, "label_PathCount");
            label_PathCount.Name = "label_PathCount";
            // 
            // PathCount_mode
            // 
            PathCount_mode.DropDownStyle = ComboBoxStyle.DropDownList;
            PathCount_mode.FormattingEnabled = true;
            PathCount_mode.Items.AddRange(new object[] { resources.GetString("PathCount_mode.Items"), resources.GetString("PathCount_mode.Items1") });
            resources.ApplyResources(PathCount_mode, "PathCount_mode");
            PathCount_mode.Name = "PathCount_mode";
            PathCount_mode.SelectedIndexChanged += PathCount_mode_SelectedIndexChanged;
            // 
            // progressBar1
            // 
            resources.ApplyResources(progressBar1, "progressBar1");
            progressBar1.Name = "progressBar1";
            // 
            // PathCount
            // 
            resources.ApplyResources(PathCount, "PathCount");
            PathCount.Name = "PathCount";
            PathCount.UseVisualStyleBackColor = true;
            PathCount.Click += PathCount_Click;
            // 
            // Path_finder_win
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Menu;
            Controls.Add(PathCount);
            Controls.Add(progressBar1);
            Controls.Add(PathCount_mode);
            Controls.Add(label_PathCount);
            Controls.Add(label_aimY);
            Controls.Add(label_aimX);
            Controls.Add(Aim_y);
            Controls.Add(Aim_x);
            Controls.Add(Aim_lable);
            Controls.Add(Map_format_lable);
            Controls.Add(Radial_map_ChBx);
            Controls.Add(Ortogonal_map_ChBx);
            Controls.Add(Radial_grid_ChBx);
            Controls.Add(Ortogonal_grid_ChBx);
            Controls.Add(Save_map_bt);
            Controls.Add(Clear_map_bt);
            Controls.Add(Map_status);
            Controls.Add(Map_win);
            HelpButton = true;
            Name = "Path_finder_win";
            Load += Form1_Load;
            Map_win.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }




        #endregion

        private Panel Map_win;
        private Label Map_status;
        private Button Clear_map_bt;
        private Button Save_map_bt;
        private CheckBox Ortogonal_grid_ChBx;
        private Label Aim_point;
        private CheckBox Radial_grid_ChBx;
        private CheckBox Ortogonal_map_ChBx;
        private CheckBox checkBox1;
        private CheckBox Radial_map_ChBx;
        private Label Map_format_lable;
        private Label Aim_lable;
        private TextBox Aim_x;
        private TextBox Aim_y;
        private Label label_aimX;
        private Label label_aimY;
        private Label label_PathCount;
        private ComboBox PathCount_mode;
        private ProgressBar progressBar1;
        private Button PathCount;
    }
}
