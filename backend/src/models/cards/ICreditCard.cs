namespace BankWeb.models.cards {

    public interface ICreditCard {
        double Deposit(double amount);
        double Withdraw(double amount);
    }
}