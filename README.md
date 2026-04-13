# apbd-2026-ang-tut5
The main object of refactoring here was class SubscriptionRenewalService which was kinda the main operating class. It had all the main knoledge inside of it.
I added seperate namespaces Legacy and Discount also simple DiscountCalculator class, it helped a lot, thanks to that class SubscriptionRenewalService was no longer class that did it all.


Legacy:
It's a namespace holding interfaces and wrapper classes. I created it because LegacyBillingGateway was unchangable soo I had to create something else that will act as a bridge between business logic and legacy static classes. It fufils Dependecy Inversion Rule (DIP)


Discount:
It's a set of small classes implementign a common IDiscountRule interface. It breaks down conditional logic into independet entities. I wrote this namespace with a thout to easyli extend it in time of need, if there appear a new discount option all that must be done is simply writing a new class implementign interface. Worth mentioning is that it encapsulate buisness rules individually instead of using one giant if to check everything. Here I used Open/Closed principle.


DiscountCalculator:
Well it's idea is selfexplainatory it takes raw data and return a specyfic discount(here also at the beggining is part of code that is nowhere use but it's a leftover from my original idea). it's a Single Responsibility Principle(SRP) class



SubscribtionRenewalService thanks to those is simpler to read and analyse. It validates the user request, fetch the customer and plan data, ask the calculator for the discount, add the standard fees and taxes, package everything into a RenewalInvoice object and tell the gateway to save and email it. It's much better in comparrison with the prevoius version which validated inputs, connected to database, calculated discounts and taxes etc.
