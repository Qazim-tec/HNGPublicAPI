Number Classification API
This is a C# ASP.NET Core API that classifies a given number and returns its mathematical properties along with a fun fact fetched from the Numbers API.

Features
Number Classification: Determines if a number is prime, perfect, or an Armstrong number.

Digit Sum Calculation: Calculates the sum of the digits of the number.

Parity Check: Identifies if the number is even or odd.

Fun Fact Integration: Fetches a fun fact about the number from the Numbers API.

API Endpoint
Classify a Number
Endpoint: GET /api/NumberClassification?number={number}

Description: Classifies the given number and returns its properties along with a fun fact.

Parameters:

number (required): The number to classify. Must be a valid integer.