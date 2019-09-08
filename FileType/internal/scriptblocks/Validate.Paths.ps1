Set-PSFScriptblock -Name 'FileType.Validate.Paths' -Scriptblock {
	try { Resolve-PSFPath -Path $_ -Provider FileSystem }
	catch { return $false }
	return $true
}