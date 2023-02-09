# CashService

CashService Methods

| Method       | Description    |
| ------------- |-------------|
| AddPersonalData()      | Function of add personal data |
| GetPersonalDataById()      | Function of requesting personal data by user ID      |
| UpdatePersonalData() | Function of updating personal data      |
| AddDiscount()      | Function of add discount for profile |
| GetDiscounts()      | Function of requesting personal discounts by user ID      |
| UpdateDiscount() | Function of updating discount      |


Business Models
```
 PersonalData {
   id 
   name 
   surname 
   phone 
   email 
}
```
```
enum DiscountType {
	DISCOUNT_TYPE_UNSPECIFIED = 0;
	DISCOUNT_TYPE_AMOUNT = 1;
	DISCOUNT_TYPE_DISCOUNT = 2;
}
```
```
 Discount{
	 id
	 personalid
	 isalreadyused
	 type 
	 amount 
	 discountvalue 	
}
```


Description of Methons of GRPC Service
```
Function of add personal data
	rpc AddPersonalData (AddPersonalDataRequest) 
		returns (AddPersonalDataResponce);

Function of requesting personal data by user ID
	rpc GetPersonalDataById (GetPersonalDataByIdRequest) 
		returns (GetPersonalDataByIdResponce);

Function of updating personal data
	rpc UpdatePersonalData (UpdatePersonalDataRequest) 
		returns (UpdatePersonalDataResponce);

Function of add discount
	rpc AddDiscount (AddDiscountRequest) 
		returns (AddDiscountResponce);

Function of requesting personal discounts by user ID
	rpc GetDiscounts (GetDiscountsRequest) 
		returns (GetDiscountsResponce);
  
Function of updating discounts
	rpc UpdateDiscount (UpdateDiscountRequest) 
		returns (UpdateDiscountResponce);
  ```
  
   ```
  Examples of CashService proto-entity
   ```
  
  ```

```

```

```

```

```

