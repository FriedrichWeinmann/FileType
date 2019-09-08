# Reset FileType cache
[FileType.FTHost]::FileTypes.Clear()
[FileType.FTHost]::MaxHeader = 0

# Load FileType definitions
$configFile = "$script:ModuleRoot\internal\data\fileTypes.json"
Get-Content -Path $configFile -Raw | ConvertFrom-Json | Write-Output | Register-FileType