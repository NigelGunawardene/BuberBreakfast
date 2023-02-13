# BuberBreakfast

<div align="center">


 [![GitHub Stars](https://img.shields.io/github/stars/amantinband/buber-breakfast.svg)](https://github.com/amantinband/buber-breakfast/stargazers) [![GitHub license](https://img.shields.io/github/license/amantinband/buber-breakfast)](https://github.com/amantinband/buber-breakfast/blob/main/LICENSE)

---

### [CRUD REST API .NET 6 Youtube](https://youtu.be/PmDJIooZjBE)

</div>


- [Overview](#overview)
- [Service Architecture](#service-architecture)
- [Technologies](#technologies)
- [Architecture](#architecture)
- [Usage](#usage)
- [API Definition](#api-definition)
  - [Create Breakfast](#create-breakfast)
    - [Create Breakfast Request](#create-breakfast-request)
    - [Create Breakfast Response](#create-breakfast-response)
  - [Get Breakfast](#get-breakfast)
    - [Get Breakfast Request](#get-breakfast-request)
    - [Get Breakfast Response](#get-breakfast-response)
  - [Update Breakfast](#update-breakfast)
    - [Update Breakfast Request](#update-breakfast-request)
    - [Update Breakfast Response](#update-breakfast-response)
  - [Delete Breakfast](#delete-breakfast)
    - [Delete Breakfast Request](#delete-breakfast-request)
    - [Delete Breakfast Response](#delete-breakfast-response)
- [Credits](#credits)
- [VSCode Extensions](#vscode-extensions)
- [Chapter notes](#chapter-notes)


---

# Overview

In the tutorial, we build a CRUD REST API from scratch using .NET 6.
As you would expect, the backend system supports Creating, Reading, Updating and Deleting breakfasts.

# Service Architecture

<div align="center">

<img src="assets/BackendServiceArchitecture.png" alt="drawing" width="700px"/>

</div>

# Technologies

<div align="center">

<img src="assets/Technologies.png" alt="drawing" width="700px"/>

</div>

# Architecture

<div align="center">

<img src="assets/Architecture.png" alt="drawing" width="700px"/>

</div>

# Usage

Simply `git clone https://github.com/amantinband/buber-breakfast` and `dotnet run --project BuberBreakfast`.

# API Definition


## Create Breakfast

### Create Breakfast Request

```js
POST /breakfasts
```

```json
{
    "name": "Vegan Sunshine",
    "description": "Vegan everything! Join us for a healthy breakfast..",
    "startDateTime": "2022-04-08T08:00:00",
    "endDateTime": "2022-04-08T11:00:00",
    "savory": [
        "Oatmeal",
        "Avocado Toast",
        "Omelette",
        "Salad"
    ],
    "Sweet": [
        "Cookie"
    ]
}
```

### Create Breakfast Response

```js
201 Created
```

```yml
Location: {{host}}/Breakfasts/{{id}}
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "name": "Vegan Sunshine",
    "description": "Vegan everything! Join us for a healthy breakfast..",
    "startDateTime": "2022-04-08T08:00:00",
    "endDateTime": "2022-04-08T11:00:00",
    "lastModifiedDateTime": "2022-04-06T12:00:00",
    "savory": [
        "Oatmeal",
        "Avocado Toast",
        "Omelette",
        "Salad"
    ],
    "Sweet": [
        "Cookie"
    ]
}
```

## Get Breakfast

### Get Breakfast Request

```js
GET /breakfasts/{{id}}
```

### Get Breakfast Response

```js
200 Ok
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "name": "Vegan Sunshine",
    "description": "Vegan everything! Join us for a healthy breakfast..",
    "startDateTime": "2022-04-08T08:00:00",
    "endDateTime": "2022-04-08T11:00:00",
    "lastModifiedDateTime": "2022-04-06T12:00:00",
    "savory": [
        "Oatmeal",
        "Avocado Toast",
        "Omelette",
        "Salad"
    ],
    "Sweet": [
        "Cookie"
    ]
}
```

## Update Breakfast

### Update Breakfast Request

```js
PUT /breakfasts/{{id}}
```

```json
{
    "name": "Vegan Sunshine",
    "description": "Vegan everything! Join us for a healthy breakfast..",
    "startDateTime": "2022-04-08T08:00:00",
    "endDateTime": "2022-04-08T11:00:00",
    "savory": [
        "Oatmeal",
        "Avocado Toast",
        "Omelette",
        "Salad"
    ],
    "Sweet": [
        "Cookie"
    ]
}
```

### Update Breakfast Response

```js
204 No Content
```

or

```js
201 Created
```

```yml
Location: {{host}}/Breakfasts/{{id}}
```

## Delete Breakfast

### Delete Breakfast Request

```js
DELETE /breakfasts/{{id}}
```

### Delete Breakfast Response

```js
204 No Content
```

# Credits

- [ErrorOr](https://github.com/amantinband/error-or) - A simple, fluent discriminated union of an error or a result.

# VSCode Extensions

- [VSCode Rest Client](https://github.com/Huachao/vscode-restclient) - REST Client allows you to send HTTP request and view the response in Visual Studio Code directly.

- [VSCode Markdown Preview Enhanced](https://github.com/shd101wyy/vscode-markdown-preview-enhanced) - Markdown Preview Enhanced is an extension that provides you with many useful functionalities for previewing markdown files.

# Chapter Notes

## Chapter One

Setting up the projects. Refer commands.txt
Contracts project models our API. If it is separately in a classlib, can be published as Nuget Package.
Web API holds our CRUD API and business logic.

Docs: Holds our API definition
Requests: Holds our API request definitions for testing

## Chapter Two

Created Models in Contracts project
Cleaned up Program.cs

## Chapter Three

API definition
Created CRUD endpoints

## Chapter Four

Models and converting between them

Motivation behind internal service models - 
We create a contracts folder to represent the CONTRACT that we have with the client. We do not modify this.
We map the data from the contract to an internal service model and inside the application, we only speak the language of this internal service model. The contracts model is ONLY for consistency with the client.

In our CreateBreakfast controller method, we get the breakfast request, convert it to the internal service model (breakfast) and then after saving in the database, we convert it to breakfastResponse (DTO) and send it back to the client.

NEW THING WE DID - Instead of returning Ok() from IActionResult, we return CreatedAtAction. This returns 201, and accepts 3 parameters:
1. The action that the client can use to retrieve the resource
2. Then because the resource needs an ID, we pass an object that contains the newly created item ID
3. The response content

Implemented CRUD operations in controller and service


## Chapter Five

### Global error handling
#### Exception Middlware:

In program.cs, we specify a route for error handling - app.UseExceptionHandler("error");

One of the middleware invokes the controller. By adding the line above, we use built in capability of the framework to basically wrap middleware in a try catch block. If an exception is thrown, it catches the exception and it changes the route to what we specified and re-execute the request.

Then we create an ErrorController with the route that we defined and do what we want with the exception/implment any error handling logic.
In this example, we use the Problem method from the ControllerBase, returns 500 internal server error

#### Error Handling:

The approach taken here is from functional programming. Basically, when you request a Breakfast, we will try to give you a breakfast, otherwise we will give you an error that represents what happened

Install ErrorOr. Create ServiceErrors Folder and a class where we can write the errors we expect. In this case, Breakfast.Errors. Then we describe the errors related to that entity.

Then we edit the IService to reflect - ErrorOr<Breakfast> and the Service Implementation
Update the controller

To make the code more readable, we use the Match method in the controller - 
The match method receives 2 functions, one will run if what we have is the right value, and the other one will run if its an error.

In the GetBreakfast method, we can get the specific type of error (like Error.NotFound) and do something for that specific kind of error. Like this, we can get the type of the error and return a relevant response to the client. Otherwise, we can do something a bit more general - 

We implement our own Problem() method:
Create BaseController (called ApiController)
Move common controller Attributes to ApiController

So now we have a BaseController that other controllers will inherit from, when we have an error/list of errors, our new problem method will take the firsterror and customize the status code and then we use the Problem method from the controllerBase to return the status code and description 

The flow:
We call the BreakfastService from the controller, receieve the breakfast or a list of errors. If its a breakfast, we map it to the correct response and send it, otherwise we call our custom problem method, convert it to the correct response and send it. 

Now changing the rest of the methods

For upsert, since we want to know if the upsert actually did an update or insert, we can create our own type instead of using ErrorOr<Updated> - 

Create UpsertedBreakfastResponse with UpsertedBreakfast
Then we can return ErrorOr<UpsertedBreakfast>

Then reduce code duplication in controller by moving shared response logic in create and upsert into a separate method

All the errors we can expect in our system are located in the Errors folder/classes and they are well defined. This is a major advantage.



## Chapter Six

Enforcing business rules on internal service models

We go to Breakfast.cs, change constructor to private and define a Create method ( this is a factory. its a static factory method)
Now we enforce the business rules inside the Create method.
Also define new errors in ServiceErrors
We are also not limited to returning only 1 error, we can create a list and add all errors and return that 
Change controller to use the Create method

To improve our error handling, we check if all the errors that are returned are validation errors.
If so, we return a ValidationProblem from ControllerBase. We need to give it a ModelStateDictionary to convert to a response

## Adding EF Core - separate video (https://www.youtube.com/watch?v=v19arLqQkP8&t=601s)

Add the EF Core Nuget Package to the main project

Create Persistence folder -> BuberBreakfastDbContext

Then change the BreakfastService to use the DbContext

Define our database provider
In program.cs we register our context in the DI container

To configure our data provider, we can either use options in program.cs like :

```c#
    builder.Services.AddDbContext<BuberBreakfastDbContext>(options =>   
        options.UseSqlite("Data Source=BuberBreakfast.db"));
```

OR in the DbContext Class with this method: 
```c#
override onConfiguring(DbContextOptionsBuilder optionsBuilder)
```

We are using SqLite for this tutorial - Install EntityFrameworkCore.Sqlite package

---
SIDE NOTE -
install dotnet tool for EFCore 
dotnet tool install -- global dotnet-ef
dotnet tool list --global
dotnet tool update -- global dotnet-ef

---


Add our migration - 
Install EntityFrameworkCore.Design package
dotnet ef migrations add InitialCreate --project BuberBreakfast

We get an error because our Breakfast constructor is private and requires many parameters
So we add a new constructor and private setters to our Breakfast class:

```c#
    private Breakfast() { }

    public Guid Id { get; private set; }
```

It can be private because EF uses reflection and populates this object. When EF adds values to these properties and
also build the migration definition, it looks at the class definition and iterate over all the properties

We get another failure because our Lists need a primary key - because our savory and sweets items have a one : many relationship and we need to decide how it is going to be stored

To fix this, inside our DbContext class, we use the OnModelCreating method to customize how we configure our object

```c#
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Breakfast>()
            .Property
    }
```

This approach can take up a lot of space and get messy if we have a lot of DbSets.
So we create a Persistence => Configurations folder and create a BreakfastConfigurations:

```c#
public class BreakfastConfigurations : IEntityTypeConfiguration<Breakfast>
{
    public void Configure(EntityTypeBuilder<Breakfast> builder)
    {
        throw new NotImplementedException();
    }
}
```

for our ID column, by default, EF lets the database generate the value. if the name of the property is ID, EF conventions allow it to automatically detect that. 
Because we generate the ID ourselves, we configure EF to never generate IDs.

Once again the migration fails, because EF doesnt know to look for the configuration file we created.
In our DbContext, we add the following to tell it to scan the assembly for configurations

```c#
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BuberBreakfastDbContext).Assembly);
    }
```

Now it works and Migrations folder is created. The warning is that we override hasconversion but not the comparer (like it we override equals but not hashcode and vice versa)


A Model Snapshot is the current state of the model stored in a class file named <YourContext>ModelSnapshot.cs The file is added to the Migrations folder when the first migration is created, and updated with each subsequent migration. It enables the migrations framework to calculate the changes required to bring the database up to date with the model.

Now the MIGRATION is created, this is just the design of the database, and the actual database is not created
We run the following command to create/update the schema:

```c#
dotnet ef database update -p BuberBreakfast
```

After running this, we see the new DB file created in our project






































