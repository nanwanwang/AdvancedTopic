namespace SynchronizationContextSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task task = WaitAsync();
            task.Wait();


            MessageBox.Show("hahahha");
        }

        async Task WaitAsync()
        {
            await Task.Delay(1000);
        }
    }
}