# SmartNavigator

This sample uses the Hugging Face Inference API to generate answers for user queries.

## Building

Make sure the [.NET SDK](https://dotnet.microsoft.com/download) is installed and
available on your `PATH`.

Restore the dependencies and build the project:

```bash
dotnet restore
dotnet build
```

## Running

After building, run the application from the repository root:

```bash
dotnet run
```

You can execute the unit tests with:

```bash
dotnet test
```

## Setting up the API token

The app requires a free Hugging Face Inference API token. Create an account on
[huggingface.co](https://huggingface.co/), generate a new access token in your
profile settings and export it as the `HF_TOKEN` environment variable before
running the project:

```bash
export HF_TOKEN=<your-token>
```

