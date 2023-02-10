# CashService

CashService Methods

| Method       | Description    |
| ------------- |-------------|
| GetBalance     | Balance receipt function |
| Deposit      | Function for Deposit cash into the account     |
| Withdraw | Function to Withdraw cash from account  |
| DepositRange     | Function to Deposit cash to different accounts |
| WithdrawRange     | Function to Withdraw cash from different accounts      |


Business Models
```
message TransactionModel
{
	string profileid = 1;
	repeated Transaction transactions = 2;
}
```
```
message Transaction
{
	string transactionid = 1; 
	CashType cashtype = 2;
	double amount = 3;
}
```
```
enum CashType
{
	CASH_TYPE_UNSPECIFIED = 0;
	CASH_TYPE_CASH=1;
	CASH_TYPE_BONUS=2;
}
```


Description of Methons of GRPC Service
```
  rpc GetBalance(GetBalanceRequest) returns (GetBalanceResponce);
  rpc Deposit (DepositRequest) returns (DepositResponce);
  rpc Withdraw (WithdrawRequest) returns (WithdrawResponce);
  rpc DepositRange(DepositRangeRequest) returns (DepositRangeResponce);
  rpc WithdrawRange (WithdrawRangeRequest) returns (WithdrawRangeResponce);
```
  
   ```
  Examples of CashService proto-entity
   ```
  
  
GetBalance
  ```
{
   "profileid" : "ececbe9f-6a4a-4f72-9dec-68912349e53c"
}
```
Deposit
```
{
	"ProfileId":"ececbe9f-6a4a-4f72-9dec-68912349e53c",
	"Transactions":
	[
		{"TransactionId":"35ec2f77-d4b9-4e93-85bf-6b013d2ee838","TransactionProfileId":"ececbe9f-6a4a-4f72-9dec-68912349e53c","TransactionProfileEntity":null,"CashType":1,"Amount":95.0},
		{"TransactionId":"9dca2d44-83b8-455a-88f6-d2abbe80ffd4","TransactionProfileId":"ececbe9f-6a4a-4f72-9dec-68912349e53c","TransactionProfileEntity":null,"CashType":2,"Amount":50.0}
	]
}
```

```

```

