# Book Shop
This web project was developed as a full-stack project, using technologies such as ASP.NET MVC, Entity Framework, MSSQL, JS, HTML/CSS, jQuery, Bootstrap, Azure Studio, and design patterns. During development, authentication, authentication via Facebook and Microsoft accounts, 4 user roles, an admin panel, a payment method through the Stripe platform, a shopping cart, and more were implemented. Various APIs were also used.

To launch the project, you need to run the command `git clone https://github.com/FilonenkoDima/shop_MVC.git` or download the archive, then navigate to `shop_on_asp/appsettings.json` and change the `ConnectionStrings/DefaultConnection` parameter according to your settings, and run it through MS Visual Studio 2022.

You can preview this project [here](filonenkoshop.azurewebsites.net/).

## The goal of the project.
- Creation of a fully functional webpage for selling books
- Implementation of authentication using different methods
- Creation of 4 roles:
  - Administrator – has all the rights that other users have and can add new administrators and other users
  - Employee – can perform CRUD operations
  - User – can add items to the cart and purchase them
  - Company – has the option for deferred payment
  - Unregistered user – can only view products
- Integration with a payment system
- Improvement of acquired skills

## The structure of the project.
- Shop.DataAccess – contains the implementation of the 'Repository and Unit of Work' pattern, data access implementation, migrations, and seed data
- Shop.Models – contains data models
- Shop.Utility – created for working with third-party services and internally implemented utilities
- shop_on_asp/Areas – logic for role creation
- shop_on_asp/Views – views
- shop_on_asp/wwwroot – frontend
