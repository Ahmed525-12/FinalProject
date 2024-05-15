# Thriftiness App (.NET)

## Overview
This project is a user-friendly financial management app developed on the .NET framework, utilizing ASP.NET MVC. It enables efficient expense tracking and budget management for users, helping them achieve financial goals and improve financial habits.

## Features
- **Expense Tracking**: Users can easily track their expenses, categorize them, and analyze spending patterns.
- **Budget Management**: The app allows users to set budgets for different expense categories and monitor their progress.
- **Secure Data Storage**: Microsoft SQL Server is integrated for secure data storage, ensuring user data privacy and reliability.
- **External Google Login**: Users can sign in to the app using their Google accounts, providing convenience and security.
- **Caching**: Caching mechanisms are implemented to optimize performance and reduce database load, enhancing user experience.
- **The Assistant**: That Allows to User to have ChatGPT to help him to give him advice about saving money and plans ETC.
## Technologies Used
- ASP.NET MVC
- Microsoft SQL Server (MSS)
- External Google Login
- Caching

## Installation
1. Clone the repository:
2. Navigate to the project directory:
3. Install dependencies (if any):
4. Set up the database:
- Ensure Microsoft SQL Server is installed and running on your system.
- Update the connection string in the `appsettings.json` file with your MSS server information.
- Run database migrations to create the necessary tables:
  ```
  dotnet ef database update
  ```
5. Configure External Google Login:
- Obtain Google OAuth credentials for your application.
- Update the OAuth client ID and client secret in the `appsettings.json` file.
6. Start the app:

## Contributing
Contributions are welcome! If you'd like to contribute to this project, please follow these steps:
1. Fork the repository
2. Create a new branch (`git checkout -b feature`)
3. Make your changes
4. Commit your changes (`git commit -am 'Add new feature'`)
5. Push to the branch (`git push origin feature`)
6. Create a new Pull Request



## Contact
For any inquiries or support, please contact [Ahmed525-12](https://github.com/Ahmed525-12).
