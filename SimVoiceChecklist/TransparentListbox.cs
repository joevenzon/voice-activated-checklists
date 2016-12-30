/*Creator: Pingo
 *Site: mpgh.net
 *Created: 28/07/2012 
 *Version: 1.0.0
 *Contact: ping0@hotmail.co.uk
 * */
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

public class ListBox : Label
{
    public ListBox()
    {
        this.Size = new Size(165, 124);
        this.MinimumSize = new Size(25, 25);
        this.ForeColor = SystemColors.Highlight;
        this.Font = new Font("Verdana", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
    }

    #region Storage
    List<object> lst = new List<object>();
    Color SBorCol = Color.DarkBlue, SelCol = Color.DodgerBlue, BorCol = Color.Navy, InBorCol = Color.SlateBlue;
    string tmp = string.Empty;
    byte SelAlpha = 50;
    Size FSize;
    int _Index = -1, SIndex = 0;
    bool _Focus;
    #endregion

    #region Overrides

    protected override void OnFontChanged(EventArgs e)
    {
        base.OnFontChanged(e);
        FSize = TextRenderer.MeasureText("X", this.Font);
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        this.AutoSize = false;
        this.Text = string.Empty;
        this.BackColor = Color.Transparent;
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
        base.OnMouseDown(e);
        if (e.Button == MouseButtons.Left)
        {
            this.Focus();
            _Focus = true;
            _Index = -1;
            Point P = this.PointToClient(MousePosition);
            for (int i = lst.Count; i-- > 0; )
            {
                if (i < MaxCount)
                {
                    if (P.Y >= (FSize.Height * i) + 5 & P.Y < (FSize.Height * i) + (FSize.Height + 5))
                    {
                        this.Refresh();
                        _Index = i + SIndex;
                        this.CreateGraphics().FillRectangle(new SolidBrush(Color.FromArgb(SelAlpha, SelCol)), new Rectangle(5, (FSize.Height * i) + 5, this.Width - 12, FSize.Height));
                        this.CreateGraphics().DrawRectangle(new Pen(SBorCol), new Rectangle(5, (FSize.Height * i) + 5, this.Width - 12, FSize.Height));
                    }
                }
            }
        }
    }

    protected override void OnInvalidated(InvalidateEventArgs e)//
    {
        base.OnInvalidated(e);
        tmp = string.Empty;
        int Co = 0;
        for (int i = SIndex; i < lst.Count; i++)
        {
            if (Co == MaxCount) return;
            tmp += lst[i] + "\n";
            Co++;
        }
    }

    protected override void OnDoubleClick(EventArgs e)
    {
        base.OnDoubleClick(e);
        if (SelectedIndex != -1)
        {
            if (MessageBox.Show("Are you sure you want to delete\n" + lst[SelectedIndex], "Delete Dat Shizer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                lst.RemoveAt(SelectedIndex);
                _Index = -1;
                this.Invalidate();
            }
        }
    }

    #endregion

    #region Draw

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.DrawRectangle(new Pen(BorCol, 4), new Rectangle(2, 2, this.Width - 5, this.Height - 5));
        e.Graphics.DrawRectangle(new Pen(InBorCol, 2), new Rectangle(4, 4, this.Width - 9, this.Height - 9));
        TextRenderer.DrawText(e.Graphics, tmp, this.Font, new Point(5, 5), this.ForeColor);
        if (_Index != -1)//Draws item highlight when scrolling
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(SelAlpha, SelCol)), new Rectangle(5, (FSize.Height * (_Index - SIndex)) + 5, this.Width - 12, FSize.Height));
            e.Graphics.DrawRectangle(new Pen(SBorCol), new Rectangle(5, (FSize.Height * (_Index - SIndex)) + 5, this.Width - 12, FSize.Height));
        }
    }

    #endregion

    #region UserSettings

    public Color SelectBorderColor
    {
        get { return SBorCol; }
        set { SBorCol = value; }
    }

    [Browsable(false)]
    public new Color BackColor
    {
        get { return base.BackColor; }
        set { base.BackColor = Color.Transparent; }
    }

    public Color BorderColor
    {
        get { return BorCol; }
        set { BorCol = value; }
    }
    public Color BorderColorInner
    {
        get { return InBorCol; }
        set { InBorCol = value; }
    }
    public Color SelectColor
    {
        get { return SelCol; }
        set { SelCol = value; }
    }

    public byte SelectAlpha
    {
        get { return SelAlpha; }
        set
        {
            SelAlpha = value <= 100 ? value : (byte)50;
        }
    }

    #endregion

    #region Functions

    public void Add(object Value)
    {
        if (Value == null) return;
        int i;
        if ((i = lst.IndexOf(Value)) == -1)
        {
            lst.Add(Value);
        }
        this.Invalidate();
    }

    [Browsable(false)]
    public int SelectedIndex
    {
        get { return _Index; }
    }

    public void RemoveAt(int Index)
    {
        if (Index != -1 & Index <= lst.Count)
        {
            lst.RemoveAt(Index);
            this.Invalidate();
        }
    }

    public void Remove(object Value)
    {
        if (Value == null) return;
        int i;
        if ((i = lst.IndexOf(Value)) != -1)
        {
            lst.RemoveAt(i);
            this.Invalidate();
        }
    }

    public bool Contains(object Value)
    {
        return lst.Contains(Value);
    }

    public void Clear()
    {
        lst.Clear();
        this.Invalidate();
    }

    int MaxCount
    {
        get
        {
            return ((this.Height - 8) / FSize.Height);
        }
    }

    [Browsable(false)]
    public object SelectedItem
    {
        get
        {
            return SelectedIndex != -1 ? lst[_Index] : null;
        }
    }

    #endregion

    #region Disabled Settings
    [Browsable(false)]
    public override string Text { get; set; }

    [Browsable(false)]
    public override bool AutoSize { get; set; }

    [Browsable(false)]
    public new BorderStyle BorderStyle { get; set; }

    [Browsable(false)]
    public new FlatStyle FlatStyle { get; set; }

    [Browsable(false)]
    public new Image Image { get; set; }

    [Browsable(false)]
    public new Image ImageAlign { get; set; }

    [Browsable(false)]
    public new Image ImageIndex { get; set; }

    [Browsable(false)]
    public new Image ImageKey { get; set; }

    [Browsable(false)]
    public new Image ImageList { get; set; }

    [Browsable(false)]
    public new Size MinimumSize { get; set; }

    //[Browsable(false)]
    //public new DockStyle Dock { get; set; }

    [Browsable(false)]
    public new HorizontalAlignment TextAlign { get; set; }

    [Browsable(false)]
    public new bool UseWaitCursor { get; set; }
    #endregion

    #region Scroll Code

    protected override void OnMouseWheel(MouseEventArgs e)
    {
        base.OnMouseWheel(e);
        if (_Focus)
        {
            if (e.Delta > 0)
            {
                if (SIndex > 0)
                    SIndex--;
            }
            else
            {
                if ((SIndex + MaxCount) >= lst.Count) return;
                if (SIndex <= (lst.Count - 1))
                    SIndex++;
            }
            this.Refresh();
        }
    }

    protected override void OnMouseEnter(EventArgs e)
    {
        base.OnMouseEnter(e);
    }

    protected override void OnMouseLeave(EventArgs e)
    {
        base.OnMouseLeave(e);
        _Focus = false;
    }
    #endregion

}