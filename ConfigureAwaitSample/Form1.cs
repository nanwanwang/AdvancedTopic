namespace ConfigureAwaitSample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var context = SynchronizationContext.Current;
            await  Task.Delay(1000).ConfigureAwait(false); //����Ϊfalse��ʾ����Ҫͬ����������ִ�к���Ĵ��� �ᱨ��


            context.Post(obj => { this.textBox1.Text = "hahh"; }, null);

    
        }
    }
}