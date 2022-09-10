# NumbersIntoWords coding task

- Write a program which converts a currency (dollars) from numbers into words. The maximum number of dollars is 999 999 999.
- The maximum number of cents is 99.
- The separator between dollars and cents is a ‘,’ (comma).


## Projects to run
### Api
Starts the HTTP server.
### WpfClient
Starts the WPF client to interact with the api above.

## Http client generation
```shell
# NSwag is used for client generation
# See more on https://github.com/RicoSuter/NSwag

# Ensure you have Node.js installed before

# Install NSwag
npm install -g npm
npm i -g nswag

# Generate Open Api specification (using swagger) and save swagger.json file in "src\Api.Client\Generator\" folder.

# Execute PowerShell script
src\Api.Client\Generator\GenerateClient.ps1
```

## Tests
```shell
# Execute all tests
dotnet test
```
