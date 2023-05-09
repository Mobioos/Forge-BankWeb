using BankWeb.models.cards;
using BankWeb.models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BankWeb.Controllers
{
  [ApiController]
  [Route("api/account/V1")]
  [EnableCors("AllowAll")]
  public class AccountController : ControllerBase
  {
    private readonly Account _defaultAccount;

    private readonly ILogger<AccountController> _logger;

    public AccountController(ILogger<AccountController> logger)
    {
      _defaultAccount = AccountProvider.DEFAULT_ACCOUNT;
      this._logger = logger;
    }

    [HttpGet]
    public Account Get()
    {
      this._logger.LogInformation("Getting the default account");
      return this._defaultAccount;
    }

    [HttpGet("deposit")]
    public OperationMessage Deposit(double amount)
    {
      this._logger.LogInformation($"Deposing {amount}");
      return this._defaultAccount.Deposit(amount);
    }

    [HttpGet("withdraw")]
    public OperationMessage Withdraw(double amount)
    {
      this._logger.LogInformation($"Withdrawing {amount}");
      return this._defaultAccount.Withdraw(amount);
    }

    [HttpGet("setCard")]
    public OperationMessage SetCard(string cardId)
    {
      this._logger.LogInformation($"Setting card with ID {cardId}");

      switch (cardId) {
        case "standard":
          this._defaultAccount.CreditCard = new StandardCreditCard();
          return OperationMessage.CreateOkMessage(this._defaultAccount.Balance);
        case "cashback":
          this._defaultAccount.CreditCard = new CashBackCreditCard();
          return OperationMessage.CreateOkMessage(this._defaultAccount.Balance);

        default:
          return OperationMessage.CreateKoMessage("Could not set new card", $"Unknown card id \"{cardId}\"", this._defaultAccount.Balance);
      }
    }
  }
}

