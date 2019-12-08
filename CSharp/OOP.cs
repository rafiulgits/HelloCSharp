using System;

namespace CSharp.OOP
{
    public class BankAccount{
        private string ID;
        private string Name;
        private decimal Balance;

        public BankAccount(string name, decimal initialBalance){
            this.Name = name;
            if(initialBalance >=0){
                this.Balance = initialBalance;
            }
            else{
                this.Balance = 0;
            }
            this.ID = Guid.NewGuid().ToString().Substring(0,13);
        }

        public void Diposite(decimal amount){
            if(amount > 0){
                StatementSeparate();
                Console.WriteLine($"Previous Balance: {Balance}");
                Console.WriteLine($"Diposite Amount : {amount}");
                Balance += amount;
                Console.WriteLine($"Current Balance : {Balance}");
                StatementSeparate();
            }
        }

        public void Withdraw(decimal amount){
            if(amount <= Balance){
                StatementSeparate();
                Console.WriteLine($"Previous Balance: {Balance}");
                Console.WriteLine($"Withdraw Amount : {amount}");
                Balance -= amount;
                Console.WriteLine($"Current Balance : {Balance}");
                StatementSeparate();
            }
        }

        private void StatementSeparate(){
            for(int i=0; i<25; i++)
                Console.Write("-");
            Console.WriteLine("");
        }

        public override string ToString(){
            return $"ID: {ID}\nName: {Name}\nBalance: {Balance}";
        }
    }

    public interface Vehicle {
        bool Flying { get; set; }
        bool HasTier { get; }
        string Color { get; set; }

        double CurrentSpeed();
    }



    public abstract class Car : Vehicle {
        
        public bool Flying { get; set; }
        public bool HasTier { get; }
        public string Color { get; set; }

        public int Seat {get; set;}
        public string Name {get;}


        public Car(string name, string color, int seat){
            Name = name;
            Flying = false;
            HasTier = true;
            Color = color;
            Seat = seat;
        }

        public abstract double CurrentSpeed();
    }

    public class Audi : Car {

        private Random random;

        public Audi():base("Audi", "White", 4) {
            random = new Random();
        }

        public Audi(string color, int seat):base("Audi", color, seat){
            random = new Random();
        }

        public override double CurrentSpeed(){
            return (double)random.Next(0, 150);
        }
    }



    public class OOPExamples {
        public static void BankAccountExample(){
            var myAccount = new BankAccount("Rafiul Islam", 1000);
            Console.WriteLine(myAccount);
            myAccount.Diposite(500);
            myAccount.Withdraw(800);
            Console.WriteLine(myAccount);
        }

        public static void InheritanceAndPolyMorfism(){
            var A8 = new Audi("Black", 2);
            Console.WriteLine(A8.Flying);
            Console.WriteLine(A8.CurrentSpeed());
        }
    }
}