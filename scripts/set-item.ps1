function addSnapinIfNotAdded($snapIn)  
{  
 	if ( (Get-PSSnapin -Name $snapIn -ErrorAction SilentlyContinue) -eq $null )  
	{  
		add-pssnapin $snapIn  
	}  
}  
 
addSnapinIfNotAdded "Puddle"

new-psdrive -psprovider puddle -name myHuddle -root "" -Host api.huddle.dev

$loc = "files/folders/1342524"
get-item myHuddle:$loc
Write-Host("`n" + "Before!")

$value = New-Object -title "newFolderName" -desc "foo bar"
Set-Item -Path myHuddle:$loc -Value $value
Write-Host("`n" + "After!")
get-item myHuddle:$loc
