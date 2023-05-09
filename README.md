# The BankWeb Application

BankWeb is a simple web application that emulates the behavior of a banking application. After **login** in with the user details (Google, Github, or Microsoft accounts), the client can **withdraw or deposit** $100 on a fictional account with different **types of credit cards**. Regarding the withdrawal operation, **overdrafts are not allowed**, which means that trying to perform a withdrawal operation on an account with less than $100 will be revoked. The application also offers the ability to **convert several currencies**. 

This application comprises a **frontend in plain HTML/CSS/JS and a backend in [ASP.NET](https://dotnet.microsoft.com/en-us/apps/aspnet) Core**.

# Branch presentations
This repository is organized with the following branches: 
  - [`master`](https://github.com/Mobioos/Forge-BankWeb/tree/master): The main branch that contains the SPL built with **Mobioos-Forge**.
  - [`initial-application`](https://github.com/Mobioos/Forge-BankWeb/tree/initial-application): The initial application without any forge files.
  - [`variants/gold`](https://github.com/Mobioos/Forge-BankWeb/tree/variants/gold): The [*Gold*](#the-gold-customization) variant.
  - [`variants/silver`](https://github.com/Mobioos/Forge-BankWeb/tree/variants/silver): The [*Silver*](#the-silver-customization) variant.
  - [`variants/bronze`](https://github.com/Mobioos/Forge-BankWeb/tree/variants/bronze): The [*Bronze*](#the-bronze-customization) variant.

# Pre-requisites
To build and execute the application and use <span style="color: #e66300;">Mobioos Forge</span> for the BankWeb application, you need to configure your computer and VScode to support web and .NET development. Thus you need to perform the following steps:
- Backend:
  - **Ensure you have the [.NET 6 SDK](https://dotnet.microsoft.com/en-us/download) installed on your computer**: It lets you compile and execute the application's backend.
  - **Install the [C#](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp) extension**: This allows using <span style="color: #e66300;">Mobioos Forge</span> for the backend part.
- Frontend: 
  - **Install [Node.js](https://nodejs.org/en/), and [npm](https://www.npmjs.com/)** to be able to run the frontend.
  - *As Javascript development is by default integrated inside VScode, you do not need to install additional extensions*.

Before going into details about the SPL implementation, the following section will describe the architecture of the BankWeb application in more detail.

# Architecture

As this application comprises a frontend and a backend application, we separated those two applications into two folders bearing the same name.

![File explorer](https://mobioosstorageaccount.blob.core.windows.net/public-documentation/Forge-tutorials/BankWeb/images/architecture/file-explorer.png)

## Frontend

As this is a simple web application using no frameworks such as Angular or React, its architecture is straightforward. The application's code and resources are located in the `app` folder, and the entry point is the file `app/index.html`.

![File explorer frontend](https://mobioosstorageaccount.blob.core.windows.net/public-documentation/Forge-tutorials/BankWeb/images/architecture/file-explorer-frontend.png)

## Backend

The backend follows the classical architecture of ASP.NET applications.
![File explorer backend](https://mobioosstorageaccount.blob.core.windows.net/public-documentation/Forge-tutorials/BankWeb/images/architecture/file-explorer-backend.png)

# Execution

To launch the application, you will need two terminals to start the front and backend.
- Start the frontend:
  1. Go to the `frontend` folder: `cd frontend`
  2. Install the dependencies and start the local web server: `npm i && npm start`
- Start the Backend:
  1. Go to the `backend` folder: `cd backend`
  2. Build and start the backend: `dotnet run`

The gif below shows the execution of the BankWeb application by performing several withdrawal and deposit operations with different types of credit cards.
![Demo](https://mobioosstorageaccount.blob.core.windows.net/public-documentation/Forge-tutorials/BankWeb/videos/presentation.gif)

# SPL Overview

## The Feature Model

![Feature Model](https://mobioosstorageaccount.blob.core.windows.net/public-documentation/Forge-tutorials/BankWeb/images/spl/feature-model.png)

In the given Feature Model, **we have 13 Features (10 Functional Features and 3 Resource Features)**. On the left, we can see the 3 Resources Features of the Feature Model. The frontend application uses these resources. They allow the customization of the **bank icon used in the UI** and its **data** (name, description, contacts) and the **graphical theme of the interface** (the *CSS* file used).

The ***Login*** Functionality Feature has three children, one child for each authentification provider managed by the application. We have a similar pattern with the Functionality Feature ***Credit Card***, which has 3 children representing the different Credit Card proposed by the Bank. We decided to **specify the *Standard Credit Card* to be mandatory**.

## The Feature Mappings

Mapping the Features of the BankWeb application has been made with 18 Markers (15 Code-Markers and 3 File-Markers). Those markers provided 8 additional Maps in total.

![Feature-Maps view](https://mobioosstorageaccount.blob.core.windows.net/public-documentation/Forge-tutorials/BankWeb/images/spl/feature-mapping/feature-maps-view.png)

The following sections will describe the Markers and Maps for the Functionality Features ***Allow Overdraft***, ***Standard Credit Card***, and the Resource Feature ***Icon***.

### Allow Overdraft Functionality Feature

This Functionality Feature guarantees that the account's balance never goes below $0. As it is entirely managed on the backend side of the application, we will only need to add a Code-Marker on the backend of the applications. This Code-Marker is added on the property `allowOverdraft` of the `Account` class (located on the file `backend/src/models/Accounts.cs`).

![Allow Overdraft Code-Marker](https://mobioosstorageaccount.blob.core.windows.net/public-documentation/Forge-tutorials/BankWeb/images/spl/feature-mapping/allow-overdraft/property-code-marker.png)

Adding this marker will compute a Map inside the `Withdraw` method.
Validating this Map can be done both with a Deletion Map and a Replacement Map. We decided here to perform a Replacement Map as shown in the Figure below.

![Allow Overdraft Map](https://mobioosstorageaccount.blob.core.windows.net/public-documentation/Forge-tutorials/BankWeb/images/spl/feature-mapping/allow-overdraft/feature-map.png)

We also added a Code-Marker on the `CreateOverdraftKoMessage``method for code cleanliness.

### Standard Credit Card Functionality Feature
The code of this Functionality Feature needs to be mapped on both the frontend and the backend.

#### Frontend

This Functionality Feature uses an image to be represented on the UI. So we can add a File-Marker on the image `frontend/app/images/cards/standard.png` to identify the locations where this image is referenced.

![Standard Credit Card File-Marker](https://mobioosstorageaccount.blob.core.windows.net/public-documentation/Forge-tutorials/BankWeb/images/spl/feature-mapping/standard-credit-card/file-marker.png)

Once the File-Marker is added, <span style="color: #e66300;">Mobioos Forge</span> detects that the file is used inside the file `frontend/app/index.html`. We can validate the Map by extending it on the overall `<label>` tag.

![Index.html map](https://mobioosstorageaccount.blob.core.windows.net/public-documentation/Forge-tutorials/BankWeb/images/spl/feature-mapping/standard-credit-card/index-html-map.png)

#### Backend

On the backend, we have a class named `StandardCreditCard` (on the file `backend/src/models/cards/StandardCreditCard.cs`). We add a Code-Marker on the class declaration (we could also add a File-Marker on the file).

Adding this Code-Marker will compute several Maps on the method `Deposit` and `Withdraw` of the `Account` class. As those Maps refer to calls to the `Deposit` and `Withdraw` methods of the interface `ICreditCard` (the interface implemented by `StandardCreditCard`), we can simply delete them.

The other Map found by the Code-Marker added on the `StandardCreditCard` class is located inside the file `backend/src/AccountProvider.cs`. validating this map can be done by simply extending it to the full constructor call.

![AccountProvider Map](https://mobioosstorageaccount.blob.core.windows.net/public-documentation/Forge-tutorials/BankWeb/images/spl/feature-mapping/standard-credit-card/account-provider-map.png)

## CurrencyConverter

The currency converter Functionality Feature is implemented as an `<iframe>` in the frontend/app/index.html file. We add a Code-Marker on the `<iframe>`'s overall `<div>` tag.

Regarding what to do if the Functionality Feature is disabled, we **decided to show the Apple stock chart**. We change the Variability Action to a replacement and give the code of Apple's chart (The code has been generated thanks to this [tool](https://www.tradingview.com/widget/advanced-chart/) from TradingView).

![Currency Converter Replacement Map](https://mobioosstorageaccount.blob.core.windows.net/public-documentation/Forge-tutorials/BankWeb/images/spl/feature-mapping/currency-converter/replacement-map.png)

### Icon Resource Feature

The icon is referenced on the file `frontend/app/index.html`. Mapping this Resource Feature is done by adding a Code-Marker as shown below.

![Icon Code-Marker](https://mobioosstorageaccount.blob.core.windows.net/public-documentation/Forge-tutorials/BankWeb/images/spl/feature-mapping/icon/code-marker.png)

## An Example of Customization

To test the customization process, we created three customization configurations:
- Gold: A *Gold* customization where all Features are enabled except for ***Microsoft***, and ***Deferred Credit Card***.
- Silver: A *Silver* customization where the Functionality Features ***Microsoft***, ***Deferred Credit Card***, and ***CurrencyConverter*** are enabled.
- Bronze: A *Bronze* customization where all optional Features are disabled except for the ***Google*** login provider.

The resource files used for each customization configuration are available in this repository. The icons are located in the folder `frontend/app/images/bank_icons/`, the css files are located in the folder `frontend/app/styles/banks/`, the data files are located in the `frontend/app/data/` folder.

> Before generating any variants, delete the `frontend/node_modules`, as this folder slows down the customization process.
### The Gold Customization

![Gold variant](https://mobioosstorageaccount.blob.core.windows.net/public-documentation/Forge-tutorials/BankWeb/images/spl/customization/gold.png)

### The Silver Customization

![Silver variant](https://mobioosstorageaccount.blob.core.windows.net/public-documentation/Forge-tutorials/BankWeb/images/spl/customization/silver.png)

### The Bronze Customization

![Bronze variant](https://mobioosstorageaccount.blob.core.windows.net/public-documentation/Forge-tutorials/BankWeb/images/spl/customization/bronze.png)