namespace doanC.Model
{
    class ModelDonvido
    {
        public int id;
        public string name;
        public ModelDonvido()
        {

        }
        public ModelDonvido(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string GetString()
        {
            return string.Format("{0},{1}", id, name);
        }
    }
}
