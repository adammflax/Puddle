function addSnapinIfNotAdded($snapIn)  
{  
 	if ( (Get-PSSnapin -Name $snapIn -ErrorAction SilentlyContinue) -eq $null )  
	{  
		add-pssnapin $snapIn  
	}  
}  
 
addSnapinIfNotAdded "Puddle"

new-psdrive -psprovider puddle -name myHuddle -root "" -Host api.huddle.dev

$loc = "files/folders/1327040"
cd myHuddle:$loc
Write-Host("`n" + "Before!")
ls

$loc = "files/documents/1342525"
remove-Item myHuddle:$loc
Write-Host("`n" + "After!")
ls