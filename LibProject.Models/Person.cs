namespace LibProject.Models
{
    public class Person : Entity
    {
        public string FirstName { get; set; }
        public string Surname { get; set; }

        public override string ToString()
        {
            return $"{nameof(FirstName)}: {FirstName}, {nameof(Surname)}: {Surname}";
        }
    }
}