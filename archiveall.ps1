git clean -dfX

$sampleFolders = Get-ChildItem . -Directory -Exclude ("_archives", ".git", ".vs")

ForEach ($folder in $sampleFolders) 
{
	$zipPath = "./_archives/" + $folder.name + ".zip"
	Compress-Archive -Path $folder -DestinationPath $zipPath -Force
}