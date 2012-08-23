function addSnapinIfNotAdded($snapIn)  
{  
 	if ( (Get-PSSnapin -Name $snapIn -ErrorAction SilentlyContinue) -eq $null )  
	{  
		add-pssnapin $snapIn  
	}  
}  

function Install_Puddle()
{
	addSnapinIfNotAdded "Provider"
	update-formatdata -prependpath "C:\Users\adam.flax\Documents\Visual Studio 2010\Projects\Provider\Provider\bin\Debug\Puddle.format.ps1xml" -verbose
	new-psdrive -psprovider Provider -name myHuddle -root "" -Host api.huddle.dev/ -Scope "Global"
#	new-psdrive -psprovider Providr -name myHuddle -root "" -Host api.huddle.dev
}

##get our cmdlets and provider
#addSnapinIfNotAdded "Puddle"
#
#update-formatdata -prependpath c:\users\adam.flax\Documents\Puddle\src\bin\debug\Puddle.format.ps1xml -verbose
#
##create our psdrive
#new-psdrive -psprovider puddle -name myHuddle -root "" -Host api.huddle.dev
#
#$loc = "\"
##go to the root point
#cd myHuddle:$loc
#