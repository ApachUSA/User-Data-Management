# User Data Management
## Introduction
Simple web-based application for user data accounting. Realization of technical assignment for Ukrposhta company.

## Technologies
![Static Badge](https://img.shields.io/badge/ASP-badge?style=for-the-badge&logo=ASP&color=%23292929)
![Static Badge](https://img.shields.io/badge/.NET%20CORE%208-badge?style=for-the-badge&logo=.NET&color=%23292929)
![Static Badge](https://img.shields.io/badge/MVC-badge?style=for-the-badge&color=%23292929)
![Static Badge](https://img.shields.io/badge/SQL-badge?style=for-the-badge&logo=sql%20server&color=%23292929)
![Static Badge](https://img.shields.io/badge/AJAX-badge?style=for-the-badge&color=%23292929)


- Using the MVC pattern to organize code.
- Using direct database queries (without ef).
- Implementation of AJAX requests to improve the reactivity of the interface.

## Functions

- View personnel with search filters
- Ability to create/edit/delete an employee
- Salary reporting with filters
- Uploading salary reports to a txt file

## How to start

In **appsettings.json**, paste the name of your server in the connection string
```
    "ConnectionStrings": {
    "DefaultConnection": "Server=your_name;Database=UkrPoshtaDB;Trusted_Connection=True; trustServerCertificate=true;"
  },
```

## Screenshots

<div align="center">
  <img src="/Screenshots/Start.png"/>
  <p>Start page</p>
  <br/>
</div>

<div align="center">
  <img src="/Screenshots/Staff.png"/>
  <p>Staff page</p>
  <br/>
</div>

<div align="center">
  <img src="/Screenshots/StaffWithFilters.png"/>
  <p>Filtered staff</p>
  <br/>
</div>

<div align="center">
  <img src="/Screenshots/PersonCard.png"/>
  <p>Person card page</p>
  <br/>
</div>

<div align="center">
  <img src="/Screenshots/Salary.png"/>
  <p>Salary report page</p>
  <br/>
</div>

<div align="center">
  <img src="/Screenshots/SalaryWithFilters.png"/>
  <p>Filtered salary report</p>
  <br/>
</div>

<div align="center">
  <img src="/Screenshots/SalaryFile.png"/>
  <p>Downloaded file example</p>
  <br/>
</div>
