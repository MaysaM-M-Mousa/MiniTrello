# MiniTrello

MiniTrello is a side project meant to simulate a simple behavior for the famous software project management app **Trello** using **Event Sourcing** pattern.

<br>

## MiniTrello:
* Follows **Domain-Driven-Design** principles
* Implements **Event Sourcing** architectural pattern
* Follows the **Clean-Architecture**
* Implements various design patterns and best practices including:
  * Writing unit tests
  * Writing architectural tests
  * Api Versioning
  * Structured Logging
  * Implementing Outbox Pattern
  * Implementing Idempotent consumers
 

<br>

## MiniTrello's behavior:

<br>

![MiniTrello](https://github.com/MaysaM-M-Mousa/MiniTrello/assets/54291847/bdae6797-1c4e-4769-997b-9b6ae3c8857e)

As stated in the diagram; once the ticket is created we can:
* Assign it to a valid user
* Unassign it
* Perform various actions to update its status:
  * ToDo -> InProgress
  * InProgress -> CodeReview
  * CodeReview -> Test
  * Test -> InProgress | Done
 

