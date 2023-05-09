using BankWeb.models.cards;

namespace BankWeb.models
{
  public class Account
  {
    public long Id { get; }
    public double Balance { get; set; }
    public string Currency { get; }
    public ICreditCard CreditCard {get; set; }

    public Account(long id, double initial, string currency, ICreditCard creditCard)
    {
      this.Id = id;
      this.Balance = initial;
      this.Currency = currency;
      this.CreditCard = creditCard;
    }

    public OperationMessage Deposit(double amount)
    {
      this.Balance += this.CreditCard.Deposit(amount);
      return this.CreateBalanceOkMessage();
    }

    public OperationMessage Withdraw(double amount)
    {
      var toWithdraw = this.CreditCard.Withdraw(amount);
      var sum = this.Balance - toWithdraw;
      this.Balance -= toWithdraw;
return this.CreateBalanceOkMessage();

    }

    private OperationMessage CreateBalanceOkMessage()
    {
      return OperationMessage.CreateOkMessage(this.Balance);
    }

  }
}
