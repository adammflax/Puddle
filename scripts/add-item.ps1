function addSnapinIfNotAdded($snapIn)  
{  
 	if ( (Get-PSSnapin -Name $snapIn -ErrorAction SilentlyContinue) -eq $null )  
	{  
		add-pssnapin $snapIn  
	}  
}  
 
addSnapinIfNotAdded "Puddle"

new-psdrive -psprovider Provider -name myHuddle -root "" -Host api.huddle.dev

Write-Host("`n" + "Before!")
$loc = "files/folders/1341804"
cd myHuddle:$loc
ls

$folder = New-Object -title "New Folder" -desc "bar"
New-Item -Path myHuddle:$loc -type folder -Value $folder

$File = New-Object -title "New document" -desc "bar"
$uploadPath = New-Item -Path myHuddle:$loc -type file -Value $file

Set-Item -Path myHuddle:$uploadPath -Value c:\Users\adam.flax\Documents\location.txt

Write-Host("`n" + "After!")
ls