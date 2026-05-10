using System;
namespace DB_P3
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();

            // ── ROW 1 LABEL ──────────────────────────────────────
            // button1 - Insert Book
            this.button1.Location = new System.Drawing.Point(30, 30);
            this.button1.Size = new System.Drawing.Size(150, 30);
            this.button1.Text = "Insert Book";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);

            // button3 - Insert Author
            this.button3.Location = new System.Drawing.Point(200, 30);
            this.button3.Size = new System.Drawing.Size(150, 30);
            this.button3.Text = "Insert Author";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);

            // button4 - (hidden/unused placeholder)
            this.button4.Location = new System.Drawing.Point(0, 0);
            this.button4.Size = new System.Drawing.Size(1, 1);
            this.button4.TabStop = false;
            this.button4.Visible = false;

            // ── ROW 2: DELETE ─────────────────────────────────────
            // button5 - Delete Book
            this.button5.BackColor = System.Drawing.Color.IndianRed;
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(30, 80);
            this.button5.Size = new System.Drawing.Size(150, 30);
            this.button5.Text = "Delete Book";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.btnDeleteBook_Click);

            // button6 - Delete Order
            this.button6.BackColor = System.Drawing.Color.IndianRed;
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(200, 80);
            this.button6.Size = new System.Drawing.Size(150, 30);
            this.button6.Text = "Delete Order";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.btnDeleteOrder_Click);

            // ── ROW 3: UPDATE ─────────────────────────────────────
            // button7 - Update Order Details
            this.button7.BackColor = System.Drawing.Color.SteelBlue;
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Location = new System.Drawing.Point(30, 130);
            this.button7.Size = new System.Drawing.Size(150, 30);
            this.button7.Text = "Update Order Details";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.btnUpdateOrderDetails_Click);

            // button8 - Update Format
            this.button8.BackColor = System.Drawing.Color.SteelBlue;
            this.button8.ForeColor = System.Drawing.Color.White;
            this.button8.Location = new System.Drawing.Point(200, 130);
            this.button8.Size = new System.Drawing.Size(150, 30);
            this.button8.Text = "Update Format";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.btnUpdateFormat_Click);

            // ── ROW 4: SELECT ─────────────────────────────────────
            // button9 - Select Table
            this.button9.BackColor = System.Drawing.Color.SeaGreen;
            this.button9.ForeColor = System.Drawing.Color.White;
            this.button9.Location = new System.Drawing.Point(30, 180);
            this.button9.Size = new System.Drawing.Size(150, 30);
            this.button9.Text = "Select Table";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.btnSelectTable_Click);

            // button10 - Join Tables
            this.button10.BackColor = System.Drawing.Color.SeaGreen;
            this.button10.ForeColor = System.Drawing.Color.White;
            this.button10.Location = new System.Drawing.Point(200, 180);
            this.button10.Size = new System.Drawing.Size(150, 30);
            this.button10.Text = "Join Tables";
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.btnJoinTables_Click);

            // ── DataGridView ──────────────────────────────────────
            this.dataGridView1.Location = new System.Drawing.Point(30, 230);
            this.dataGridView1.Size = new System.Drawing.Size(740, 180);
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.AllowUserToAddRows = false;

            // ── Form1 ─────────────────────────────────────────────
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "DB Phase 3";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}