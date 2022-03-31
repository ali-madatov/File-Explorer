using System.Diagnostics;

namespace HW3
{
    public partial class Files : Form
    {
        FileInfo updatedFile = null;
        int countdown = 100;
        public Files()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void miExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void miOpen_Click(object sender, EventArgs e)
        {
         
            InputDialog dialog = new InputDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                showItems(dialog.InputText);
            }

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count  == 1)
            {
                tbName.Text = listView1.SelectedItems[0].SubItems[0].Text;
                tbCreation.Text = listView1.SelectedItems[0].SubItems[2].Text;
                tbContent.Text = " ";
                reloadTimer.Stop();
                updatedFile = null;
                panel1.Invalidate();
            }
        }

        private void miRun_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                string filename = File.ReadAllText(listView1.SelectedItems[0].SubItems[3].Text);
                if (filename != null)
                {
                    Process.Start(filename);
                }
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                if (listView1.SelectedItems[0].Tag is FileInfo)
                {
                    tbContent.Text = File.ReadAllText(listView1.SelectedItems[0].SubItems[3].Text);
                    reloadTimer.Start();
                    countdown = 100;
                    updatedFile = new FileInfo(listView1.SelectedItems[0].SubItems[3].Text);
                        
                }
                else
                    showItems(listView1.SelectedItems[0].SubItems[3].Text);
            }
        }

        private void reloadTimer_Tick(object sender, EventArgs e)
        {
            countdown--;
            panel1.Invalidate();
            if(countdown <= 0 && updatedFile!=null)
            {
                countdown = 100;
                tbContent.Text = File.ReadAllText(updatedFile.FullName);
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (updatedFile != null)
                e.Graphics.FillRectangle(Brushes.Blue, 0, 0, countdown, 10);
            else
                e.Graphics.FillRectangle(Brushes.Transparent, 0, 0, 0, 0);
        }

        private void showItems(string directoryInfo)
        {
            tbName.Text = " ";
            tbCreation.Text = " ";
            tbContent.Text = " ";
            updatedFile = null;
            panel1.Invalidate();
            reloadTimer.Stop();
            DirectoryInfo parentDI = new DirectoryInfo(directoryInfo);
            listView1.Items.Clear();
            try
            {
                if (parentDI.Parent!=null)
                {
                    DirectoryInfo parentDirectory = new DirectoryInfo(parentDI.Parent.ToString());
                    ListViewItem topItem = new ListViewItem(new string[] {
                                "..."," ",parentDirectory.CreationTime.ToString(), parentDirectory.FullName });
                    topItem.Tag = parentDirectory;
                    listView1.Items.Add(topItem);
                }
                  
                foreach (DirectoryInfo directory in parentDI.GetDirectories())
                {
                    ListViewItem item = new ListViewItem(new string[] {
                                directory.Name," ",directory.CreationTime.ToString(), directory.FullName });
                    item.Tag = directory;
                    listView1.Items.Add(item);
                }

                foreach (FileInfo file in parentDI.GetFiles())
                {
                    ListViewItem item = new ListViewItem(new String[]
                    {
                            file.Name, file.Length.ToString() + " Kb", file.CreationTime.ToString(),file.FullName
                    });
                    item.Tag = file;
                    listView1.Items.Add(item);
                }
            }
            catch
            {

            }
        }
    }
}