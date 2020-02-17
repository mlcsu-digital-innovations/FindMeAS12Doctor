# Find Me An S12 Doctor Deployment Script
$ErrorActionPreference = "Stop"

# Stop the Application Pool
Stop-WebAppPool -Name "FindMeAnS12Doctor"

# Pause for the application pool to stop
Start-Sleep -Seconds 5

# Take a backup of the current api and ui directories
$date = get-date
$backupFileName = "c:\iis\findmeans12doctor\backup_" + $date.ToString("yyyyMMddHHmmss")
Compress-Archive -Path "c:\iis\findmeans12doctor\api", "c:\iis\findmeans12doctor\ui" -DestinationPath $backupFileName -CompressionLevel NoCompression

# Delete the files in api and ui directories
Remove-Item "c:\iis\findmeans12doctor\api\*.*" -Force
Remove-Item "c:\iis\findmeans12doctor\ui\*.*" -Force

# Extract the api and ui zips
Expand-Archive -Path "c:\iis\findmeans12doctor\api.zip" -DestinationPath "c:\iis\findmeans12doctor\"
Expand-Archive -Path "c:\iis\findmeans12doctor\ui.zip" -DestinationPath "c:\iis\findmeans12doctor\"

# Update the API key
$ConfigFile = "c:\iis\findmeans12doctor\api\appsettings.AimesUat.json"
(Get-Content "$Configfile") `
	-replace '"AddressSearchAPiKey": ""', '"AddressSearchAPiKey": ""' `
	| Set-Content "$Configfile"


# If an update sql script is present perform database backup, update and seed
$UpdateDatabase = Test-Path "c:\iis\findmeans12doctor\update.sql" -IsValid

if ($UpdateDatabase) {
	# Backup the Fmas12d database
	Backup-SqlDatabase -ServerInstance "einno-tsql23" -Database "fmas12d"

	# Run the database update from the update.sql file
	Invoke-Sqlcmd  -ServerInstance "einno-tsql23" -InputFile "c:\iis\findmeans12doctor\update.sql"

	# Run the seed
	Set-Location C:\iis\FindMeAnS12Doctor\api\
	dotnet Fmas12d.Api.dll /seednogppracticeorccg    
}

# Start the Application Pool
Start-WebAppPool -Name "FindMeAnS12Doctor"