$env:TempEnv="Development"
New-Item -Name "Temp${env:TempEnv}__Host" -Value locahost -ItemType Variable -Path Env:
New-Item -Name "Temp${env:TempEnv}__Port" -Value 9000 -ItemType Variable -Path Env:
dotnet run ./
