namespace WinFormsNonBlockingAsync;

partial class frmMain
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
        this.btnWaitButton = new System.Windows.Forms.Button();
        this.lblOut = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // btnWaitButton
        // 
        this.btnWaitButton.Location = new System.Drawing.Point(99, 348);
        this.btnWaitButton.Name = "btnWaitButton";
        this.btnWaitButton.Size = new System.Drawing.Size(112, 34);
        this.btnWaitButton.TabIndex = 0;
        this.btnWaitButton.Text = "Wait";
        this.btnWaitButton.UseVisualStyleBackColor = true;
        this.btnWaitButton.Click += new System.EventHandler(this.btnWaitButton_Click);
        // 
        // lblOut
        // 
        this.lblOut.AutoSize = true;
        this.lblOut.Location = new System.Drawing.Point(233, 353);
        this.lblOut.Name = "lblOut";
        this.lblOut.Size = new System.Drawing.Size(131, 25);
        this.lblOut.TabIndex = 1;
        this.lblOut.Text = "Doing Nothing";
        // 
        // frmMain
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Controls.Add(this.lblOut);
        this.Controls.Add(this.btnWaitButton);
        this.Name = "frmMain";
        this.Text = "Form1";
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private Button btnWaitButton;
    private Label lblOut;
}
