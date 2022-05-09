# GringottsBank
GringottsBank Simple Banking Application using .Net Core Web Api, Entity Framework and Sql Server

* [Introduction](#Introduction)
* [Application Architecture](#Application-Architecture)
* [Design of Microservice](#Design-of-Microservice)
* [Security : JWT Token based Authentication](#Security--JWT-Token-based-Authentication)
* [Development Environment](#Development-Environment)
* [Technologies](#Technologies)
* [Opensource Tools Used](#Opensource-Tools-Used)
* [Database Design](#Database-Design)
* [WebApi Endpoints](#WebApi-Endpoints)
* [Solution Structure](#Solution-Structure)
* [Exception Handling](#Exception-Handling)
* [Db Concurrency Handling](#Db-Concurrency-Handling)
* [Swagger: API Documentation](#Swagger-API-Documentation)
* [How to run the application](#How-to-run-the-application)
---
## Introduction
This is a .Net Core sample application and an example of how to build and implement a web api based back-end system for a simple automated banking feature like customer creation, account creation and banking transactions such as inquirying balance, making deposit and waithdraw in ASP.NET Core Web API with C#.Net, Entity Framework and SQL Server. 

## Application Architecture

The sample application is build based on the microservices architecture. There are serveral advantages in building a application using Microservices architecture like Services can be developed, deployed and scaled independently.The below diagram shows the high level design of Back-end architecture.

- **Identity Microservice** - Authenticates user based on username, password and issues a JWT Bearer token which contains Claims-based identity information in it.
- **Customer Microservice** - Handles user and customer based api operations such as customer inquiry, customer creation etc.
- **Account Microservice** - Handles account web api transactions such as account creation, account inquiry etc.
- **Transaction Microservice** - Handles account transactions like get account transactions list, deposit, withdraw etc.
- **API Gateway** - Acts as a center point of entry to the back-end application, Provides data aggregation and communication path to microservices.

<img width="723" alt="Ekran Resmi 2022-05-10 00 05 26" src="https://user-images.githubusercontent.com/65486090/167498512-4794b469-e415-40c5-836a-52bc9b2e16c7.png">


## Design of Microservice
The business logic and data logic related to business api services is written in a seperate business projects. 
Each microservice is responsible for their own business capabilities and rules. The transaction data is stored up in SQL database.


## Security : JWT Token based Authentication
JWT Token based authentication is implementated to secure the WebApi services. **Identity Microservice** acts as a Auth server and issues a valid token after validating the user credentitals. There are two scenerio, first is direct connection to apis via rest endpoints, the second one is using api gateway. The API Gateway sends the token to the client. The client app uses the token for the subsequent request.
Or request a valid token for identity api and then use this bearer token with sending request to other microservices.

![JWT Token based Security](https://h9yrga.dm.files.1drv.com/y4mCbiAcoeieS5tBZu_z1z1z42C8eoVGWUmC_re1VkLWpxWtywvsOBH73brVXA4gzKm6G59h3b3vbUVF1C3jbYRlpf-7t-faZE4m8-wYplZusss5Fm-71AH87c1aXlKoULtFoUNl5Oh9h6nZDDfgLXeo_LKOH8Q0b4BGVTpg1w7TcCZQPkX5tBZtSiQj67JGqsg4lySz2ghzB9R9ArGtaA7wA?width=702&height=422&cropmode=none)

## Development Environment

- [.Net Core 5 Web Api  SDK]
- [Visual Studio For Mac and Rider]
- [SQL Server 2019 Docker Image for Mac]

## Technologies
- C#.NET
- ASP.NET WEB API Core
- SQL Server

## Opensource Tools Used
- Automapper (For object-to-object mapping)
- Entity Framework Core (For Data Access)
- Swashbucke (For API Documentation)
- XUnit (For Unit test case)
- Ocelot (For API Gateway Aggregation)

## Database Design
<img width="844" alt="Ekran Resmi 2022-05-09 21 34 05" src="https://user-images.githubusercontent.com/65486090/167499429-7cf9d16f-0ff8-4eaf-b78c-4b25f55d69f0.png">

## WebApi Endpoints
The application has four API endpoints configured in the API Gateway to demonstrate the features with token based security options enabled. These routes are exposed to the client app to cosume the back-end services. 

### End-points implemented at the Microservice level

1. Route: **"/api/user/authenticate"** [HttpPost]- To authenticate user and issue a token
2. Route: **"/api/customer"** [HttpGet]- To retrieve customer information.
3. Route: **"/api/customer"** [HttpPost]- To create customer.
4. Route: **"/api/account"** [HttpPost]- To create account.
5. Route: **"/api/account/byaccount"** [HttpGet]- To retrieve account by given account number.
6. Route: **"/api/account/bycustomer"** [HttpGet]- To retrieve all accounts by given customer number.
7. Route: **"/api/transaction/deposit"** [HttpPost]- To deposit amount in an account.
8. Route: **"/api/transaction/withdraw"** [HttpPost]- To withdraw amount from an account.
9. Route: **"/api/transaction/byaccount"** [HttpGet]- To retrieve all transaction list by given account number.
10. Route: **"/api/transaction/bycustomer"** [HttpGet]- To retrieve all transaction list by given customer and date time period.

## Solution Structure
<img width="266" alt="Ekran Resmi 2022-05-10 00 20 02" src="https://user-images.githubusercontent.com/65486090/167500484-e45a2aaf-821f-435c-92ae-f7db5790067b.png">


## Exception Handling
A Middleware is written to handle the exceptions and it is registered in the startup to run as part of http request. Every http request, passes through this exception handling middleware and then executes the Web API controller action method. 

* If the action method is successfull then the success response is send back to the client. 
* If any exception is thrown by the action method, then the exception is caught and handled by the Middleware and appropriate response is sent back to the client.

![Exception Handler Middleware](https://h9wqda.dm.files.1drv.com/y4mgc5I1iveH8tv63QAu-nSpHVmAAHNFMb9J4KRpywPRZsM7orJiFBKAKEG-wV9r1-Ox7gsODTJZFlnMajsyedcfccUWU25GTswug3z47cr9S4itzbCkCuSG_SHVhZG91uwxvQMLnhg6TaHwOwvBrKrTI3XMzLt86TwjZHyKw4ow6vZ5372OenRnyOtfUFhFtbzThwKD2V3N2GX9v8DrLDJZw?width=431&height=371&cropmode=none)

```
public async Task InvokeAsync(HttpContext context, RequestDelegate next)
{
    try
    {
        await next(context);
    }
    catch (Exception ex)
    {
        var message = CreateMessage(context, ex);
        _logger.LogError(message, ex);
        await HandleExceptionAsync(context, ex);
    }
}
```
## Db Concurrency Handling

Db concurrency is related to a conflict when multiple transactions trying to update the same data in the database at the same time. 
When a data is read from the DB and when business logic is applied to the data, at this context, there will be three different states for the values relating to the same record.

- **Database values** are the values currently stored in the database.
- **Original values** are the values that were originally retrieved from the database
- **Current values** are the new values that application attempting to write to the database.

The state of the values in each of the transaction produces a conflict when the system attempts to save the changes and identifies using the concurrency token that the values being updated to the database are not the Original values that was read from the database and it throws DbUpdateConcurrencyException.

[Reference: docs.microsoft.com](https://docs.microsoft.com/en-us/ef/core/saving/concurrency)

The general approach to handle the concurrency conflict is:

1. Catch **DbUpdateConcurrencyException** during SaveChanges
2. Use **DbUpdateConcurrencyException.Entries** to prepare a new set of changes for the affected entities.
3. **Refresh the original values** of the concurrency token to reflect the current values in the database.
4. **Retry the process** until no conflicts occur.

```
while (!isSaved)
{
    try
    {
        await _dbContext.SaveChangesAsync();
        isSaved = true;
    }
    catch (DbUpdateConcurrencyException ex)
    {
        foreach (var entry in ex.Entries)
        {
            if (entry.Entity is AccountSummaryEntity)
            {
                var databaseValues = entry.GetDatabaseValues();
                if (databaseValues != null)
                {
                    entry.OriginalValues.SetValues(databaseValues);
                    CalculateNewBalance();
                    void CalculateNewBalance()
                    {
                        var balance = (decimal)entry.OriginalValues["Balance"];
                        var amount = accountTransactionEntity.Amount;
                        if (accountTransactionEntity.TransactionType == TransactionType.Deposit.ToString())
                        {
                            accountSummaryEntity.Balance =
                            balance += amount;
                        }
                        else if (accountTransactionEntity.TransactionType == TransactionType.Withdrawal.ToString())
                        {
                            if(amount > balance)
                                throw new InsufficientBalanceException();
                            accountSummaryEntity.Balance =
                            balance -= amount;
                        }
                    }
                }
                else
                {
                    throw new NotSupportedException();
                }
            }
        }
    }
}  
```

---
## Swagger: API Documentation

Swashbuckle Nuget package added to the all microservices and Swagger Middleware configured in the startup.cs for API documentation. when running the WebApi service, the swagger UI can be accessed through the swagger endpoint "/swagger".

```
public void ConfigureServices(IServiceCollection services)
{            
     services.AddSwaggerGen(c => {
        c.SwaggerDoc("v1", new Info { Title = "***.Api", Version = "v1" });
     });
}
```

```
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory log)
{           
     app.UseSwagger();
     app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "***.Api v1");
     });           
}
```
![image](https://user-images.githubusercontent.com/65486090/167501087-36381959-ab55-4846-ab78-c1f8df71e9c9.png)

![image](https://user-images.githubusercontent.com/65486090/167501120-aab2d53b-7241-43d1-8590-1a31205e24b4.png)

![image](https://user-images.githubusercontent.com/65486090/167501145-39b668bd-8594-4a22-894d-5cfb51245599.png)

<img width="1429" alt="Ekran Resmi 2022-05-09 20 24 22" src="https://user-images.githubusercontent.com/65486090/167501174-75f30612-e56c-4d94-ab79-12a480a6eeee.png">

<img width="1429" alt="Ekran Resmi 2022-05-09 20 29 28" src="https://user-images.githubusercontent.com/65486090/167501248-c440ea44-f6c1-45dd-87b6-ba71ef88c0c3.png">



---

## How to run the application

1. Run the below sql script for table creation. 
 CREATE TABLE [dbo].[CustomerInformation](
	[CustomerNumber] [int] NOT NULL,
    [Date] DATETIME DEFAULT (getutcdate()) NOT NULL,
	[Name] [varchar](25) NOT NULL,
    [Surname] [varchar](50) NOT NULL,
    [Username] [varchar](50) NOT NULL,
    [Password] [varchar] (250) NOT NULL
	CONSTRAINT [PK_CustomerNumber_1] PRIMARY KEY CLUSTERED([CustomerNumber])
 );
 
 CREATE TABLE [dbo].[AccountSummary](
	[AccountNumber] [int] NOT NULL,
    [CustomerNumber] [int] NOT NULL,
    [Date] DATETIME  DEFAULT (getutcdate()) NOT NULL,
	[Balance] [decimal](19,2) NOT NULL,
	[Currency] [varchar](3) NOT NULL
	CONSTRAINT [PK_AccountNumber_1] PRIMARY KEY CLUSTERED([AccountNumber]),
    CONSTRAINT [FK_Account_CustomerNumber_1] FOREIGN KEY ([CustomerNumber]) REFERENCES [dbo].[CustomerInformation] ([CustomerNumber]) 
 );
 
 CREATE TABLE [dbo].[AccountTransaction](
	[TransactionId] [int] IDENTITY(1,1) NOT NULL,
	[AccountNumber] [int] NOT NULL,
	[Date] DATETIME CONSTRAINT [DF_AccountTransaction_Date] DEFAULT (getutcdate()) NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[TransactionType] [varchar](10) NOT NULL,
	[Amount] [decimal](19,2) NOT NULL
	CONSTRAINT [PK_AccountTransaction] PRIMARY KEY CLUSTERED ([TransactionId] ASC),
	CONSTRAINT [FK_AccountTransaction_AccountNumber] FOREIGN KEY ([AccountNumber]) REFERENCES [dbo].[AccountSummary] ([AccountNumber]) 
 );
 
 and after that insert some sample data to customer information table to generate valid tokens.
 
2. Open the solution (.sln) in appproriate IDE that you familiar with.
3. Configure the SQL connection string in  microservices -> Appsettings.json file
4. Make sure you inserted some sample data for customer table. Because if you not register, you can not use api's.
5. Run the following projects in the solution
    - Identity.Api
    - Customer.Api
    - Account.Api
    - Transaction.Api
    - Gateway.WebApi (optional)
6. Project can be dockerize via project included dockerfiles and it can be used via docker images. After the images have been created, than you can use any cloud provider that support images for publishing a web api such as Heroku.
- Sample user information for authentication

|<img width="672" alt="Ekran Resmi 2022-05-10 00 27 33" src="https://user-images.githubusercontent.com/65486090/167501631-61698df9-eef7-47bd-9388-c812b16ad2fb.png">

