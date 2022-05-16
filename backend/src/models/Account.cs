using BankWeb.models.cards;

namespace BankWeb.models
{
  public class Account
  {
    public long Id { get; }
    public double Balance { get; set; }
    public string Currency { get; }
    public ICreditCard CreditCard {get; set; }
    private readonly bool allowOverdraft = false;

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
      OperationMessage message;
      if (sum >= 0 || allowOverdraft)
      {
        this.Balance -= toWithdraw;
        message = this.CreateBalanceOkMessage();
      } else
      {
        message = this.CreateOverdraftKoMessage();
      }
      return message;
    }

    private OperationMessage CreateBalanceOkMessage()
    {
      return OperationMessage.CreateOkMessage(this.Balance);
    }

    private OperationMessage CreateOverdraftKoMessage()
    {
      return OperationMessage.CreateKoMessage("Unauthorized overdraft", "You cannot perform this withdraw operation because you do not have enough fund", this.Balance);
    }
  }
}