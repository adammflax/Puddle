function addSnapinIfNotAdded($snapIn)  
{  
 	if ( (Get-PSSnapin -Name $snapIn -ErrorAction SilentlyContinue) -eq $null )  
	{  
		add-pssnapin $snapIn  
	}  
}  
 
addSnapinIfNotAdded "Puddle"

new-psdrive -psprovider puddle -name myHuddle -root "" -Host api.huddle.dev

$loc = "files/folders/1327039"
$store = get-item myHuddle:$loc


$store
Write-Host("`n" + "Lets Display All the Links of the folder at 1327039")
$store.Link

Write-Host("`n" + "Now we are just going to output there href and sort it by descending (Thanks Ian)")
$store.Link | %{ $_.href} | Sort-Object -Descending

#Write-Host("`n" + "Now we are going to get that pesky self link")
#$store.Link | Where-Object{$_ -eq "Href=self"}

#($store | ConvertTo-XML).Save("C:\Scripts\Test.xml")