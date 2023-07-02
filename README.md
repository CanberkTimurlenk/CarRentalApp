# Abstract

This project was developed as part of the assignment for the Car Rental Back-End Project, which is a task within the Software Developer Training Camp content prepared by Engin Demiroğ.
The project includes improvements made by myself along with the project requirements which could be found under the "Features & Technologies", in the subtitle "Additionally Integrated By Me"

<br>
<br>

# Objective

The development the backend side of a car rental website on ASP.NET Core Web API, along the specified features and technologies. The development process will prioritize clean code practices and adhere to SOLID principles.

Richardson API Maturity Level : 2 out of 3 (Multiple URIs Based Resources and Multiple HTTP Verbs)


<br>
<br>


# Features & Technologies

<br>


* ## To Accomplish the Task Requriements


Result Types

* Service Methods could returns a custom type which could include success flag, message and if existed also could be returned from the service methods

Autofac IOC Container

Fluent Validation 

Entity Framework

Aspect Oriented Programming Support
•	Validation, Cache, Performance, Transaction, Authorization Aspects Included

Global Exception Handling, 
•	Custom Exceptions could be added by selecting one of the HTTP status Codes

JWT Access Token (Refresh Token Feature was also added by me)

Repository Design Pattern Implementation

RESTful API Design

<br> 

* ## Additionally Integrated By Me

AutoMapper Library

Code – First Approach Implementation

Async Programming Support 

Refresh Token Feature

Unit of Work Design Pattern Implementation

Sort By Query String Support

* API Supports sorting by query string in GET Requests for Cars and Rentals resources. For instance, you can specify Price or CarName to sort the results ascending/descending 

Paginated Response Support

* In many scenarios, when working with large datasets, it is not desirable to generate a response with the entire set of data belonging to requested resource at once. Instead, pagination is used to divide the data into smaller chunks and send only a specific page or quantity in each request. 

  This helps reduce the load on the server, decrease network traffic, and allows for faster responses on the client side.

  API supports to generation of paginated responses. PageSize and Current Page could be set via query string. Generated responses has also a header that named "X-Pagination" that include information about pagination applied.
