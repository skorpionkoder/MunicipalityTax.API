# MunicipalityTax.API

Buying & Owning Real Estate Nordic

.NET Core 6 application which manages taxes applied in different municipalities. The taxes are valid for a period of time.

Example: Municipality Copenhagen has its taxes scheduled like this:
- yearly tax = 0.2 (for period 2020.01.01-2020.12.31),
- monthly tax = 0.4 (for period 2020.05.01-2020.05.31),
- it has no weekly taxes scheduled,
- and it has two daily taxes scheduled = 0.1 (at days 2020.01.01 and 2020.12.25).

The result according to provided example would be:
Municipality (Input)
Date (Input)
Result
Copenhagen
2020.01.01
0.1
Copenhagen
2020.05.02
0.4
Copenhagen
2020.07.10
0.2
Copenhagen
2020.03.16
0.2

Full requirements for the application -
 MS SQL database where municipality taxes are stored
 Taxes have ability to be scheduled (yearly, monthly ,weekly ,daily) for each municipality
 Application has the ability to insert new records for municipality taxes (one record at a time)
 User can ask for a specific municipality tax by entering municipality name and date
 Application have unit or integration tests
Application has no visible user interface, requests are given directly to application as a service (producer service).

 Application is dockerized
 Update record functionality is exposed via API