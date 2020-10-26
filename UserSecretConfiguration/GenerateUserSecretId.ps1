Set-Location $env:APPDATA/Microsoft
if( -Not (Test-Path -Path ./UserSecrets ) )
{
    New-Item -ItemType directory -Path ./UserSecrets
}
Set-Location ./UserSecrets
$user_secret_id = [guid]::NewGuid().Guid
New-Item $user_secret_id -ItemType Directory
New-Item $user_secret_id/secrets.json
'{"host": "localhost", "port": 9000, "env": "Development"}' | Add-Content $user_secret_id/secrets.json
