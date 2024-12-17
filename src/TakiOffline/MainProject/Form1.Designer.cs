namespace MainProject
{
    partial class Form1
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
		private void InitializeComponent() {
			lbl_DrawPileCardCount = new Label();
			lbl_LeadingCard = new Label();
			lb_CardsInHand = new ListBox();
			SuspendLayout();
			// 
			// lbl_DrawPileCardCount
			// 
			lbl_DrawPileCardCount.BackColor = Color.Silver;
			lbl_DrawPileCardCount.Font = new Font("Segoe UI", 30F);
			lbl_DrawPileCardCount.Location = new Point(259, 156);
			lbl_DrawPileCardCount.Name = "lbl_DrawPileCardCount";
			lbl_DrawPileCardCount.Size = new Size(100, 130);
			lbl_DrawPileCardCount.TabIndex = 0;
			lbl_DrawPileCardCount.Text = "P";
			lbl_DrawPileCardCount.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// lbl_LeadingCard
			// 
			lbl_LeadingCard.BackColor = Color.Silver;
			lbl_LeadingCard.Font = new Font("Segoe UI", 30F);
			lbl_LeadingCard.Location = new Point(365, 156);
			lbl_LeadingCard.Name = "lbl_LeadingCard";
			lbl_LeadingCard.Size = new Size(100, 130);
			lbl_LeadingCard.TabIndex = 1;
			lbl_LeadingCard.Text = "P";
			lbl_LeadingCard.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// lb_CardsInHand
			// 
			lb_CardsInHand.FormattingEnabled = true;
			lb_CardsInHand.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6", "7", "8", "9" });
			lb_CardsInHand.Location = new Point(259, 289);
			lb_CardsInHand.MultiColumn = true;
			lb_CardsInHand.Name = "lb_CardsInHand";
			lb_CardsInHand.Size = new Size(244, 109);
			lb_CardsInHand.TabIndex = 2;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(lb_CardsInHand);
			Controls.Add(lbl_LeadingCard);
			Controls.Add(lbl_DrawPileCardCount);
			Name = "Form1";
			Text = "Form1";
			ResumeLayout(false);
		}

		#endregion

		private Label lbl_DrawPileCardCount;
		private Label lbl_LeadingCard;
		private ListBox lb_CardsInHand;
	}
}
