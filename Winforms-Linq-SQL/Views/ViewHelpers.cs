using System.Windows.Forms;


namespace WinformsLinqSQL.Views
{
    public static class ViewHelpers
    {
        public static void ShowMessageBox(string message, bool isSuccess = false)
        {
            MessageBox.Show(message, isSuccess ? "Success" : "Error", MessageBoxButtons.OK, isSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }
        public static void PopulateTextBoxesFromDateGrid(DataGridView dataGrid,int rowIndex, TextBox[] textBoxes, int[]keysToUpdate)
        {
              
            if (dataGrid.CurrentCell != null && rowIndex < dataGrid.RowCount-1)
            {
                for (int i = 0; i < textBoxes.Length; i++)
                {
                    textBoxes[i].Text = dataGrid[keysToUpdate[i], rowIndex].Value.ToString();
                }
            }
        }
    }
}
