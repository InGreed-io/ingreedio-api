# InGreed.IO - API

## Development

```
$ docker compose up --build
```

## Building
```
$ dotnet build
```

## Testing
To run all tests:
```
$ dotnet test
```
To run unit tests:
```
$ dotnet test --filter Category=Unit
```

## Architecture

<center>

| Area | Technology |
|---|---|
| Environment | `.NET 8.0` |
| Contenerization | `Docker` |
| Database | `PostgreSQL` |
| Testing | `xUnit.net` |
| Code style | `.editorconfig` |

</center>
