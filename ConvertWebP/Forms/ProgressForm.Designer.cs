namespace ConvertWebP {
  partial class ProgressForm {
    /// <summary>
    /// 필수 디자이너 변수입니다.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// 사용 중인 모든 리소스를 정리합니다.
    /// </summary>
    /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form 디자이너에서 생성한 코드

    /// <summary>
    /// 디자이너 지원에 필요한 메서드입니다. 
    /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
    /// </summary>
    private void InitializeComponent() {
      this.labelPath = new System.Windows.Forms.Label();
      this.progressBar = new System.Windows.Forms.ProgressBar();
      this.listBoxHistory = new System.Windows.Forms.ListBox();
      this.labelCurrent = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // labelPath
      // 
      this.labelPath.Location = new System.Drawing.Point(13, 9);
      this.labelPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.labelPath.Name = "labelPath";
      this.labelPath.Size = new System.Drawing.Size(558, 21);
      this.labelPath.TabIndex = 0;
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(12, 33);
      this.progressBar.Name = "progressBar";
      this.progressBar.Size = new System.Drawing.Size(560, 23);
      this.progressBar.Step = 1;
      this.progressBar.TabIndex = 1;
      this.progressBar.Value = 1;
      // 
      // listBoxHistory
      // 
      this.listBoxHistory.FormattingEnabled = true;
      this.listBoxHistory.ItemHeight = 21;
      this.listBoxHistory.Location = new System.Drawing.Point(12, 83);
      this.listBoxHistory.Name = "listBoxHistory";
      this.listBoxHistory.ScrollAlwaysVisible = true;
      this.listBoxHistory.Size = new System.Drawing.Size(559, 319);
      this.listBoxHistory.TabIndex = 2;
      // 
      // labelCurrent
      // 
      this.labelCurrent.Location = new System.Drawing.Point(12, 59);
      this.labelCurrent.Name = "labelCurrent";
      this.labelCurrent.Size = new System.Drawing.Size(560, 21);
      this.labelCurrent.TabIndex = 3;
      // 
      // ProgressForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(584, 411);
      this.Controls.Add(this.labelCurrent);
      this.Controls.Add(this.listBoxHistory);
      this.Controls.Add(this.progressBar);
      this.Controls.Add(this.labelPath);
      this.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = global::ConvertWebP.Properties.Resources.MainIcon;
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.MaximizeBox = false;
      this.Name = "ProgressForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label labelPath;
    private System.Windows.Forms.ProgressBar progressBar;
    private System.Windows.Forms.ListBox listBoxHistory;
    private System.Windows.Forms.Label labelCurrent;
  }
}

