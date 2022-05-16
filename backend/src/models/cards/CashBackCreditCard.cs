namespace BankWeb.models.cards {

    public class CashBackCreditCard : ICreditCard {
        public double Deposit(double amount) {
            return amount + (amount * 0.01);
        }

        public double Withdraw(double amount) {
            return amount;
        }
    }
}