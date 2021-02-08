# Demo Contacts Application

Complex ASP.NET Core App Demo Created by Danny L. Joe (2021)

## Application Notes
- The **Contacts** and **Contacts.Api** projects have two SQL connection strings defined in their respective **appsettings.json** files. If you want to run the app after reviewing its source code, you must first edit these connection strings and specify the MSSQL 2019 server and database that you want the app to use. 
- The **Contacts.Api** project has a script in its **Sql** folder that you can use to create the app's **Contacts** table in your target MSSQL database.
- You must login before you can access the **Contacts** page via the **Contacts option** in the top navigation menu. 
- Before you can login, you must first register as a new user. The first time that you complete the registration process, the **ASP.NET Core Identity system** will create its tables in the SQL database that you specified in the two **appsettings.json** files and then populate them with the data for your new user account. After the process completes, you should then be able to login using the credentials you entered.
- Any Contacts that you create will be stored in the **Contacts** SQL table along with the UserId of your new ASP.NET Core Identity user.
- The **Contacts** controller in the **Contacts.Api** project has methods that are customized to work specifically with the **DevExtreme DataGrid** JavaScript control. The **Contacts** app has logic to ensure that the controller receives your ASP.NET Core Identity UserId so it can filter the contact rows that it returns to the data grid on the Contacts page.
- The [DevExtreme](https://js.devexpress.com/Overview/DataGrid/) data grid on the **Contacts** page can easily handle millions of rows without any degradation in performance.
- You can read about the **DevExtreme JavaScript Component Suite** [here](https://js.devexpress.com).
- The **Contacts.Data** project contains the **Identity** and **Contacts** EF Core entity models used by the app. I generated the **Contacts** model using **version 6.9.1112.0** of a powerful tool named, [Entity Developer](https://www.devart.com/entitydeveloper/). The generated model targets Net Core 3 and is compatible only with SQL Server 2019 or later.
## Demo Project Requirements
-	A user can interact with a scrollable and pageable list of their business contacts, as well as adding, editing, and deleting their contacts.
-	Each business contact record contains basic information such as email, name and address. The application accesses a SQL database through a RESTful service. The UI is responsive and fast.
-	The application can support millions of users.
## Demo Technical Requirements
-	A modern JavaScript UI framework
-	An HTTP-based API layer based on .Net Core 5.0
-	A database backend based on Microsoft SQL Server 2019
-	A database communication layer based on Entify Framework Core