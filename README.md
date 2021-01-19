# Format Converter CLI

Simple CLI app for converting one format to another.

--sourceType (Default: File) Data Source Type (File) <br>
-s, --source Required. Data Source (file path) <br>
-f, --sourceFormat Required. Source Format (Json, Xml) <br>
--sourceType (Default: File) Data Destination Type (File) <br>
-d, --destination Required. Data Destination (file path) <br>
-o, --destinationFormat Required. Destination Format (Json, Xml) <br>
--help Display this help screen. <br>
--version Display version information.

## Usage:

- build the CLI converter
- go to FormatConverter.CLI\bin\Debug\netcoreapp3.1
- run: `FormatConverter.CLI.exe -s ./document.json -f Json -o Xml -d ./out.xml`<br>
  document.json and document.xml are already available in the output dir

## Open points

- better error handling and resiliency

## Original solution

- Homework.cs - contains inline comments for code issues
