namespace doanC.Model
{
    class ModelNhacungcap
    {
        public int id;
        public string name;
        public string address;
        public string phone;
        public string email;
        public string note;
        public string date;

        public ModelNhacungcap()
        {
        }
        public ModelNhacungcap(int id, string name, string address, string phone, string email, string note, string date)
        {
            this.id = id;
            this.name = name;
            this.address = address;
            this.phone = phone;
            this.email = email;
            this.note = note;
            this.date = date;
        }
        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Address { get => address; set => address = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Email { get => email; set => email = value; }
        public string Note { get => note; set => note = value; }
        public string Date { get => date; set => date = value; }

        public string GetString()
        {
            return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}", id, name, address, phone, email, note, date);
        }
    }
}
