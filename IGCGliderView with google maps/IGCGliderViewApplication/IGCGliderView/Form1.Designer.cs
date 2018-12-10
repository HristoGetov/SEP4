namespace IGCGliderView
{
    partial class MapForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.System_messages = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxColorCoding = new System.Windows.Forms.ComboBox();
            this.ClearMap_Button = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonLightWind = new System.Windows.Forms.RadioButton();
            this.RadioButtonStrongWind = new System.Windows.Forms.RadioButton();
            this.radioButtonModerateWind = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Change = new System.Windows.Forms.Button();
            this.wind = new System.Windows.Forms.Label();
            this.LoadHotSpots_button = new System.Windows.Forms.Button();
            this.comboBoxWindDirection = new System.Windows.Forms.ComboBox();
            this.map = new GMap.NET.WindowsForms.GMapControl();
            this.legend_1 = new System.Windows.Forms.Button();
            this.legend_2 = new System.Windows.Forms.Button();
            this.legend_3 = new System.Windows.Forms.Button();
            this.legend_4 = new System.Windows.Forms.Button();
            this.legend_5 = new System.Windows.Forms.Button();
            this.label_legendmin = new System.Windows.Forms.Label();
            this.label_legendmax = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel1.Controls.Add(this.label_legendmax);
            this.splitContainer1.Panel1.Controls.Add(this.label_legendmin);
            this.splitContainer1.Panel1.Controls.Add(this.legend_5);
            this.splitContainer1.Panel1.Controls.Add(this.legend_4);
            this.splitContainer1.Panel1.Controls.Add(this.legend_3);
            this.splitContainer1.Panel1.Controls.Add(this.legend_2);
            this.splitContainer1.Panel1.Controls.Add(this.legend_1);
            this.splitContainer1.Panel1.Controls.Add(this.System_messages);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxColorCoding);
            this.splitContainer1.Panel1.Controls.Add(this.ClearMap_Button);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel1.Controls.Add(this.Change);
            this.splitContainer1.Panel1.Controls.Add(this.wind);
            this.splitContainer1.Panel1.Controls.Add(this.LoadHotSpots_button);
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxWindDirection);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.map);
            this.splitContainer1.Size = new System.Drawing.Size(978, 536);
            this.splitContainer1.SplitterDistance = 203;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 0;
            // 
            // System_messages
            // 
            this.System_messages.AutoSize = true;
            this.System_messages.Enabled = false;
            this.System_messages.Location = new System.Drawing.Point(6, 459);
            this.System_messages.Name = "System_messages";
            this.System_messages.Size = new System.Drawing.Size(58, 13);
            this.System_messages.TabIndex = 18;
            this.System_messages.Text = "Welcome !";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 419);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Color Coding accorting to:";
            // 
            // comboBoxColorCoding
            // 
            this.comboBoxColorCoding.FormattingEnabled = true;
            this.comboBoxColorCoding.Items.AddRange(new object[] {
            "Vertical Speed",
            "Altitude Gain",
            "Thermals Count"});
            this.comboBoxColorCoding.Location = new System.Drawing.Point(9, 435);
            this.comboBoxColorCoding.Name = "comboBoxColorCoding";
            this.comboBoxColorCoding.Size = new System.Drawing.Size(191, 21);
            this.comboBoxColorCoding.TabIndex = 16;
            this.comboBoxColorCoding.SelectedIndexChanged += new System.EventHandler(this.comboBoxColorCoding_SelectedIndexChanged);
            // 
            // ClearMap_Button
            // 
            this.ClearMap_Button.Location = new System.Drawing.Point(108, 373);
            this.ClearMap_Button.Name = "ClearMap_Button";
            this.ClearMap_Button.Size = new System.Drawing.Size(92, 40);
            this.ClearMap_Button.TabIndex = 14;
            this.ClearMap_Button.Text = "Clear Map";
            this.ClearMap_Button.UseVisualStyleBackColor = true;
            this.ClearMap_Button.Click += new System.EventHandler(this.Clear_Map);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonLightWind);
            this.groupBox1.Controls.Add(this.RadioButtonStrongWind);
            this.groupBox1.Controls.Add(this.radioButtonModerateWind);
            this.groupBox1.Location = new System.Drawing.Point(3, 251);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(197, 116);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Wind Speed";
            // 
            // radioButtonLightWind
            // 
            this.radioButtonLightWind.AutoSize = true;
            this.radioButtonLightWind.Location = new System.Drawing.Point(15, 20);
            this.radioButtonLightWind.Name = "radioButtonLightWind";
            this.radioButtonLightWind.Size = new System.Drawing.Size(130, 17);
            this.radioButtonLightWind.TabIndex = 10;
            this.radioButtonLightWind.TabStop = true;
            this.radioButtonLightWind.Text = "Light Wind (0-5 Knots)";
            this.radioButtonLightWind.UseVisualStyleBackColor = true;
            this.radioButtonLightWind.CheckedChanged += new System.EventHandler(this.radioButton_LightWind_CheckedChanged);
            // 
            // RadioButtonStrongWind
            // 
            this.RadioButtonStrongWind.AutoSize = true;
            this.RadioButtonStrongWind.Location = new System.Drawing.Point(15, 87);
            this.RadioButtonStrongWind.Name = "RadioButtonStrongWind";
            this.RadioButtonStrongWind.Size = new System.Drawing.Size(150, 17);
            this.RadioButtonStrongWind.TabIndex = 12;
            this.RadioButtonStrongWind.TabStop = true;
            this.RadioButtonStrongWind.Tag = "";
            this.RadioButtonStrongWind.Text = "Strong Wind (10-20 Knots)";
            this.RadioButtonStrongWind.UseVisualStyleBackColor = true;
            this.RadioButtonStrongWind.CheckedChanged += new System.EventHandler(this.RadioButtonStrongWind_CheckedChanged);
            // 
            // radioButtonModerateWind
            // 
            this.radioButtonModerateWind.AutoSize = true;
            this.radioButtonModerateWind.Location = new System.Drawing.Point(15, 53);
            this.radioButtonModerateWind.Name = "radioButtonModerateWind";
            this.radioButtonModerateWind.Size = new System.Drawing.Size(158, 17);
            this.radioButtonModerateWind.TabIndex = 11;
            this.radioButtonModerateWind.TabStop = true;
            this.radioButtonModerateWind.Text = "Moderate Wind (5-10 Knots)";
            this.radioButtonModerateWind.UseVisualStyleBackColor = true;
            this.radioButtonModerateWind.CheckedChanged += new System.EventHandler(this.radioButtonModerateWind_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(33, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(144, 154);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // Change
            // 
            this.Change.Location = new System.Drawing.Point(34, 163);
            this.Change.Name = "Change";
            this.Change.Size = new System.Drawing.Size(143, 44);
            this.Change.TabIndex = 8;
            this.Change.Text = "Change View";
            this.Change.UseVisualStyleBackColor = true;
            this.Change.Click += new System.EventHandler(this.Change_Click);
            // 
            // wind
            // 
            this.wind.AutoSize = true;
            this.wind.Location = new System.Drawing.Point(2, 210);
            this.wind.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.wind.Name = "wind";
            this.wind.Size = new System.Drawing.Size(77, 13);
            this.wind.TabIndex = 7;
            this.wind.Text = "Wind Direction";
            // 
            // LoadHotSpots_button
            // 
            this.LoadHotSpots_button.Location = new System.Drawing.Point(9, 373);
            this.LoadHotSpots_button.Margin = new System.Windows.Forms.Padding(2);
            this.LoadHotSpots_button.Name = "LoadHotSpots_button";
            this.LoadHotSpots_button.Size = new System.Drawing.Size(92, 40);
            this.LoadHotSpots_button.TabIndex = 4;
            this.LoadHotSpots_button.Text = "Load Hotspots";
            this.LoadHotSpots_button.UseVisualStyleBackColor = true;
            this.LoadHotSpots_button.Click += new System.EventHandler(this.Generate_polygons);
            // 
            // comboBoxWindDirection
            // 
            this.comboBoxWindDirection.FormattingEnabled = true;
            this.comboBoxWindDirection.Items.AddRange(new object[] {
            "North",
            "South",
            "East",
            "West"});
            this.comboBoxWindDirection.Location = new System.Drawing.Point(3, 225);
            this.comboBoxWindDirection.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxWindDirection.Name = "comboBoxWindDirection";
            this.comboBoxWindDirection.Size = new System.Drawing.Size(198, 21);
            this.comboBoxWindDirection.TabIndex = 0;
            this.comboBoxWindDirection.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // map
            // 
            this.map.Bearing = 0F;
            this.map.CanDragMap = true;
            this.map.EmptyTileColor = System.Drawing.Color.Navy;
            this.map.GrayScaleMode = false;
            this.map.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.map.LevelsKeepInMemmory = 5;
            this.map.Location = new System.Drawing.Point(2, 3);
            this.map.Margin = new System.Windows.Forms.Padding(2);
            this.map.MarkersEnabled = true;
            this.map.MaxZoom = 2;
            this.map.MinZoom = 2;
            this.map.MouseWheelZoomEnabled = true;
            this.map.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.map.Name = "map";
            this.map.NegativeMode = false;
            this.map.PolygonsEnabled = true;
            this.map.RetryLoadTile = 0;
            this.map.RoutesEnabled = true;
            this.map.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.map.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.map.ShowTileGridLines = false;
            this.map.Size = new System.Drawing.Size(768, 530);
            this.map.TabIndex = 0;
            this.map.Zoom = 0D;
            this.map.Click += new System.EventHandler(this.map_Click);
            this.map.MouseClick += new System.Windows.Forms.MouseEventHandler(this.map_MouseClick);
            // 
            // legend_1
            // 
            this.legend_1.Enabled = false;
            this.legend_1.Location = new System.Drawing.Point(13, 510);
            this.legend_1.Name = "legend_1";
            this.legend_1.Size = new System.Drawing.Size(24, 23);
            this.legend_1.TabIndex = 19;
            this.legend_1.UseVisualStyleBackColor = true;
            // 
            // legend_2
            // 
            this.legend_2.Enabled = false;
            this.legend_2.Location = new System.Drawing.Point(52, 510);
            this.legend_2.Name = "legend_2";
            this.legend_2.Size = new System.Drawing.Size(24, 23);
            this.legend_2.TabIndex = 20;
            this.legend_2.UseVisualStyleBackColor = true;
            // 
            // legend_3
            // 
            this.legend_3.Enabled = false;
            this.legend_3.Location = new System.Drawing.Point(91, 510);
            this.legend_3.Name = "legend_3";
            this.legend_3.Size = new System.Drawing.Size(24, 23);
            this.legend_3.TabIndex = 21;
            this.legend_3.UseVisualStyleBackColor = true;
            // 
            // legend_4
            // 
            this.legend_4.Enabled = false;
            this.legend_4.Location = new System.Drawing.Point(130, 510);
            this.legend_4.Name = "legend_4";
            this.legend_4.Size = new System.Drawing.Size(24, 23);
            this.legend_4.TabIndex = 22;
            this.legend_4.UseVisualStyleBackColor = true;
            // 
            // legend_5
            // 
            this.legend_5.Enabled = false;
            this.legend_5.Location = new System.Drawing.Point(169, 510);
            this.legend_5.Name = "legend_5";
            this.legend_5.Size = new System.Drawing.Size(24, 23);
            this.legend_5.TabIndex = 23;
            this.legend_5.UseVisualStyleBackColor = true;
            // 
            // label_legendmin
            // 
            this.label_legendmin.AutoSize = true;
            this.label_legendmin.Location = new System.Drawing.Point(12, 494);
            this.label_legendmin.Name = "label_legendmin";
            this.label_legendmin.Size = new System.Drawing.Size(13, 13);
            this.label_legendmin.TabIndex = 25;
            this.label_legendmin.Text = "0";
            // 
            // label_legendmax
            // 
            this.label_legendmax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_legendmax.AutoSize = true;
            this.label_legendmax.Location = new System.Drawing.Point(135, 494);
            this.label_legendmax.Name = "label_legendmax";
            this.label_legendmax.Size = new System.Drawing.Size(13, 13);
            this.label_legendmax.TabIndex = 26;
            this.label_legendmax.Text = "0";
            this.label_legendmax.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // MapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 536);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MapForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HotSpot Spotter";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button LoadHotSpots_button;
        private System.Windows.Forms.ComboBox comboBoxWindDirection;
        private GMap.NET.WindowsForms.GMapControl map;
        private System.Windows.Forms.Label wind;
        private System.Windows.Forms.Button Change;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonLightWind;
        private System.Windows.Forms.RadioButton RadioButtonStrongWind;
        private System.Windows.Forms.RadioButton radioButtonModerateWind;
        private System.Windows.Forms.Button ClearMap_Button;
        private System.Windows.Forms.ComboBox comboBoxColorCoding;
        private System.Windows.Forms.Label System_messages;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button legend_2;
        private System.Windows.Forms.Button legend_1;
        private System.Windows.Forms.Button legend_3;
        private System.Windows.Forms.Button legend_4;
        private System.Windows.Forms.Button legend_5;
        private System.Windows.Forms.Label label_legendmax;
        private System.Windows.Forms.Label label_legendmin;
    }
}

