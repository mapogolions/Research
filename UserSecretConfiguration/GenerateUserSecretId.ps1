Set-Location $env:APPDATA/Microsoft/UserSecrets
$user_sercret_id = [guid]::NewGuid().Guid
New-Item $user_sercret_id -ItemType Directory
New-Item $user_sercret_id/secrets.json
'{"host": "localhost", "port": 9000, "env": "Development"}' | Add-Content $user_sercret_id/secrets.json
