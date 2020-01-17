# Find Me An S12 Doctor Deployment Script

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
# NOTE: the acutal AddressSearchApiKey should only be added on the live server
$ConfigFile = "c:\iis\findmeans12doctor\api\appsettings.AimesUat.json"
(Get-Content "$Configfile") `
	-replace '"AddressSearchApiKey": ""', '"AddressSearchApiKey": ""' `
	| Set-Content "$Configfile"


# Start the Application Pool
Start-WebAppPool -Name "FindMeAnS12Doctor"