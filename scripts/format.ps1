function addSnapinIfNotAdded($snapIn)  
{  
 	if ( (Get-PSSnapin -Name $snapIn -ErrorAction SilentlyContinue) -eq $null )  
	{  
		add-pssnapin $snapIn  
	}  
}  

update-formatdata -prependpath c:\users\adam.flax\Documents\Puddle\src\bin\debug\Puddle.format.ps1xml -verbose

#get our cmdlets and provider
addSnapinIfNotAdded "Puddle"

#create our psdrive
new-psdrive -psprovider puddle -name myHuddle -root "" -Host api.huddle.dev
$loc = "\files/folders/1327040"
#go to the root point
cd myHuddle:$loc

#cd newFolderName
#ls

