using System;
using System.Windows.Forms;

namespace Graph_WinForms
{
    public partial class MainWindow
    {
        private void CursorButton_Click(object sender, EventArgs e)
        {
            CursorButton.Enabled = false;
            VertexButton.Enabled = true;
            EdgeButton.Enabled = true;
            DeleteButton.Enabled = true;
        }

        private void VertexButton_Click(object sender, EventArgs e)
        {
            CursorButton.Enabled = true;
            VertexButton.Enabled = false;
            EdgeButton.Enabled = true;
            DeleteButton.Enabled = true;
        }

        private void EdgeButton_Click(object sender, EventArgs e)
        {
            CursorButton.Enabled = true;
            VertexButton.Enabled = true;
            EdgeButton.Enabled = false;
            DeleteButton.Enabled = true;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            CursorButton.Enabled = true;
            VertexButton.Enabled = true;
            EdgeButton.Enabled = true;
            DeleteButton.Enabled = false;
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            if (Digraph.Vertices.Count == 0)
            {
                MessageBox.Show("Nothing to delete", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            const string message = "Would you like to save the graph? Otherwise, you will lose it.";
            const string caption = "Saving";
            SaveGraph(message, caption, out DialogResult result);
            if (result == DialogResult.Yes || result == DialogResult.No)
                RefreshVariables();
        }
    }
}
