namespace TypedConfiguration
{
    public class Config
    {
        public string Env { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

        public override string ToString() => $"{Env}|{Host}|{Port}";
    }
}
