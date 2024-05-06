namespace chatBot.Data
{
    public class PersonData
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronymic { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Birthday { get; set; }
        public string Gender { get; set; }
        public int Balance { get; set; }
        public string Products { get; set; }
        public int Income { get; set; }
        public int Spending { get; set; }

        // Переопределяем методы Equals и GetHashCode для корректной работы HashSet
        public override bool Equals(object obj)
        {
            if (obj is PersonData other)
            {
                return Phone == other.Phone;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Phone);
        }
    }
}