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
            await  Task.Delay(1000).ConfigureAwait(false); //设置为false表示不需要同步上下文来执行后面的代码 会报错


            context.Post(obj => { this.textBox1.Text = "hahh"; }, null);

    
        }
    }
}