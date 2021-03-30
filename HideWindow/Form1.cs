using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace HideWindow
{
    public class Form1 : Form
    {
        private const int SW_HIDE = 0;

        private const int SW_NORMAL = 1;

        private const int SW_RESTORE = 9;

        private IContainer components;

        private ListBox lstProcess;

        private Label lblPID;

        private TextBox txtPID;

        private Button btnHide;

        private Button btnUnhide;

        private int hWnd;

        private IntPtr pHWND;

        private Process[] processRunning = Process.GetProcesses();

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lstProcess = new System.Windows.Forms.ListBox();
            lblPID = new System.Windows.Forms.Label();
            txtPID = new System.Windows.Forms.TextBox();
            btnHide = new System.Windows.Forms.Button();
            btnUnhide = new System.Windows.Forms.Button();
            SuspendLayout();
            lstProcess.FormattingEnabled = true;
            lstProcess.Location = new System.Drawing.Point(12, 12);
            lstProcess.Name = "lstProcess";
            lstProcess.Size = new System.Drawing.Size(372, 147);
            lstProcess.TabIndex = 0;
            lstProcess.SelectedIndexChanged += new System.EventHandler(lstProcess_SelectedIndexChanged);
            lblPID.AutoSize = true;
            lblPID.Location = new System.Drawing.Point(117, 181);
            lblPID.Name = "lblPID";
            lblPID.Size = new System.Drawing.Size(69, 13);
            lblPID.TabIndex = 1;
            lblPID.Text = "Process PID:";
            txtPID.Location = new System.Drawing.Point(192, 174);
            txtPID.Name = "txtPID";
            txtPID.Size = new System.Drawing.Size(76, 20);
            txtPID.TabIndex = 2;
            btnHide.Location = new System.Drawing.Point(120, 209);
            btnHide.Name = "btnHide";
            btnHide.Size = new System.Drawing.Size(75, 23);
            btnHide.TabIndex = 3;
            btnHide.Text = "Hide";
            btnHide.UseVisualStyleBackColor = true;
            btnHide.Click += new System.EventHandler(btnHide_Click);
            btnUnhide.Location = new System.Drawing.Point(201, 209);
            btnUnhide.Name = "btnUnhide";
            btnUnhide.Size = new System.Drawing.Size(75, 23);
            btnUnhide.TabIndex = 4;
            btnUnhide.Text = "Unhide";
            btnUnhide.UseVisualStyleBackColor = true;
            btnUnhide.Click += new System.EventHandler(btnUnhide_Click);
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(396, 245);
            base.Controls.Add(btnUnhide);
            base.Controls.Add(btnHide);
            base.Controls.Add(txtPID);
            base.Controls.Add(lblPID);
            base.Controls.Add(lstProcess);
            base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "Form1";
            Text = "Hide Window";
            base.Load += new System.EventHandler(Form1_Load);
            ResumeLayout(false);
            PerformLayout();
        }

        [DllImport("User32")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Process[] array = processRunning;
            foreach (Process process in array)
            {
                lstProcess.Items.Add(process.ProcessName + "|" + process.Id);
            }
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            Process[] array = processRunning;
            foreach (Process process in array)
            {
                if (process.Id == Convert.ToInt32(txtPID.Text))
                {
                    hWnd = process.MainWindowHandle.ToInt32();
                    ShowWindow(hWnd, 0);
                }
            }
        }

        private void btnUnhide_Click(object sender, EventArgs e)
        {
            Process[] array = processRunning;
            foreach (Process process in array)
            {
                if (process.Id == Convert.ToInt32(txtPID.Text))
                {
                    hWnd = process.MainWindowHandle.ToInt32();
                    pHWND = process.MainWindowHandle;
                    if (IsIconic(pHWND))
                    {
                        ShowWindowAsync(pHWND, 9);
                    }
                    SetForegroundWindow(pHWND);
                    ShowWindow(hWnd, 1);
                }
            }
        }

        private void lstProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] array = lstProcess.Items[lstProcess.SelectedIndex].ToString().Split('|');
            txtPID.Text = array[1];
        }
    }
}
