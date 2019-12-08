namespace FileCheker.Lib
{
    class PrintResult : Printer
    {
        public PrintResult():base("Results")
        {
        }

        public override void Send(string message)
        {
            base.Send(message);
        }
    }
}
