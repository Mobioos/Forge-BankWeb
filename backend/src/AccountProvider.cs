using BankWeb.models;
using BankWeb.models.cards;

namespace BankWeb {
    public static class AccountProvider {
        public static Account DEFAULT_ACCOUNT = new Account(1, 500, "USD", new StandardCreditCard());
    }
}
