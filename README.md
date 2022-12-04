# MunicipalityTax.API

Buying & Owning Real Estate Nordic

.NET Core 6 WebAPI which manages taxes applied in different municipalities. The taxes are valid for a period of time.

Example: Municipality Copenhagen has its taxes scheduled like this:
- yearly tax = 0.2 (for period 2020.01.01-2020.12.31),
- monthly tax = 0.4 (for period 2020.05.01-2020.05.31),
- it has no weekly taxes scheduled,
- and it has two daily taxes scheduled = 0.1 (at days 2020.01.01 and 2020.12.25).

The result according to provided example would be:
Municipality (Input) - Copenhagen
Date (Input) - 2020.01.01
Result - 0.1

Municipality (Input) - Copenhagen
Date (Input) - 2020.05.02
Result - 0.4

Full requirements for the application -
- MS SQL database where municipality taxes are stored
- Taxes have ability to be scheduled (yearly, monthly ,weekly ,daily) for each municipality
- API has the ability to insert new records for municipality taxes (one record at a time)
- User can ask for a specific municipality tax by entering municipality name and date
- API has unit or integration tests
- API is dockerized
- Update record functionality is exposed via API
