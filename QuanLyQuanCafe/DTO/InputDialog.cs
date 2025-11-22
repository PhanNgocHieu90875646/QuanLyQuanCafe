using System;
using System.Windows.Forms;

public class InputDialog
{
    public static string Show(string text, string caption)
    {
        Form prompt = new Form()
        {
            Width = 350,
            Height = 150,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            Text = caption,
            StartPosition = FormStartPosition.CenterScreen
        };

        Label lbl = new Label() { Left = 20, Top = 20, Text = text, Width = 280 };
        TextBox txt = new TextBox() { Left = 20, Top = 50, Width = 280 };
        Button ok = new Button() { Text = "OK", Left = 150, Width = 70, Top = 80, DialogResult = DialogResult.OK };
        Button cancel = new Button() { Text = "Cancel", Left = 230, Width = 70, Top = 80, DialogResult = DialogResult.Cancel };

        prompt.Controls.Add(lbl);
        prompt.Controls.Add(txt);
        prompt.Controls.Add(ok);
        prompt.Controls.Add(cancel);
        prompt.AcceptButton = ok;
        prompt.CancelButton = cancel;

        return prompt.ShowDialog() == DialogResult.OK ? txt.Text : null;
    }
}
