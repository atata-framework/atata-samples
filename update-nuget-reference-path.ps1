$files = Get-ChildItem "*.csproj" -Recurse
$find1 = '..\..\packages\'
$find2 = '..\packages\'
$replace = '$(SolutionDir)\packages\'

Get-ChildItem $files -Recurse |
select -ExpandProperty fullname |
foreach {
	write-host "Processing: " $_
	$text = Get-Content -Path $_
	$text.Replace($find1, $replace).Replace($find2, $replace) | Set-Content -Path $_
}