Set-Location $PSScriptRoot

$ModName = "ItemSubtypeFilter"

New-Item "$env:TAIWU_PATH\Mod\$ModName" -ItemType "directory"
New-Item "$env:TAIWU_PATH\Mod\$ModName\Plugins" -ItemType "directory"

& "../../../MynahModConfigGenerator/bin/Debug/net6.0/MynahModConfigGenerator.exe" Config.lua "$ModName.dll"

Copy-Item *.dll "$env:TAIWU_PATH\Mod\$ModName\Plugins" -Verbose
Copy-Item Config.lua "$env:TAIWU_PATH\Mod\$ModName" -Verbose

Write-Output "copy over"