# Format Converter CLI

  --sourceType               (Default: File) Data Source Type (File)

  -s, --source               Required. Data Source (file path)

  -f, --sourceFormat         Required. Source Format (Json, Xml)

  --sourceType               (Default: File) Data Destination Type (File)

  -d, --destination          Required. Data Destination (file path)

  -o, --destinationFormat    Required. Destination Format (Json, Xml)

  --help                     Display this help screen.

  --version                  Display version information.

Example of usage:
  ```FormatConverter.CLI.exe -s ./document.json -f Json -o Xml -d ./out.xml```
