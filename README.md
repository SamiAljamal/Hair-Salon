# Salon Project

#### By Sami Al-Jamal

## Description

Hair Salon project is an application using HTTP CRUD method to update and delete elements within the application.




## Installation and how to run page.
To setup Database in SQLCMD:
  * open up the terminal
  * run the following command:
    * sqlcmd -S "(localdb)\mssqllocaldb"
    * CREATE DATABASE hair_salon;
    * Go
    * USE hair_salon;
    * Go
    * CREATE TABLE stylist (name VARCHARR(255), id INT IDENTITY(1,1));
    * Go
    * CREATE TABLE clients (name VARCHARR(255), stylist_id INT,  id INT IDENTITY(1,1));
    * Go
Following Database setup, to run the nancy app type in the following commands:
  * type in the command dnu restore.
  * type in the command dnx Kestrel.
  * open up a web browser, and type  http://localhost:5004 in the url form.


## Support and contact details
For any concerns, please contact me at sami.m.aljamal@gmail.com
## Technologies Used
Technologies used in this program include:
  * Html
  * C#
  * Nancy Framwork.
  * bootstrap
  * Microsfort SQL Server Management Studio (SSMS)
  * Razor Engine view.

### License
This is licensed under the MIT license.

Copyright (c) 2016 **Sami Al-Jamal**
