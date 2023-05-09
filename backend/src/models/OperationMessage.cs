namespace BankWeb.models {
    public class OperationMessage {
        public static string OK_STATUS = "ok";
        public static string KO_STATUS = "ko";
        public string Status { get; }
        public string Title { get; }
        public string Message { get; }
        public double Balance { get; }

        public static OperationMessage CreateOkMessage(double balance) {
            return new OperationMessage(OperationMessage.OK_STATUS, "", "", balance);
        }

        public static OperationMessage CreateKoMessage(string title, string message, double balance) {
            return new OperationMessage(OperationMessage.KO_STATUS, title, message, balance);
        }

        private OperationMessage(string status, string title, string message, double balance) {
            this.Status = status;
            this.Title = title;
            this.Message = message;
            this.Balance = balance;
        }
    }
}