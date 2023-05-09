namespace BankWeb.models.cards {

    public class StandardCreditCard : ICreditCard {
        public double Deposit(double amount) {
            return amount;
        }

        public double Withdraw(double amount) {
            return amount;
        }
    }
}
