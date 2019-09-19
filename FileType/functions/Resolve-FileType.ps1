function Resolve-FileType
{
<#
	.SYNOPSIS
		Takes a file and figures out its potential filetypes.
	
	.DESCRIPTION
		Takes a file and figures out its potential filetypes.
		Ignores folder.
	
	.PARAMETER Path
		Path to the file.
	
	.EXAMPLE
		PS C:\> Get-ChildItem C:\Shares\Data -Recurse | Resolve-FileType
	
		Detects the filetype of all files in C:\Shares\Data
#>
	[CmdletBinding()]
	param (
		[Parameter(Mandatory = $true, ValueFromPipeline = $true, ValueFromPipelineByPropertyName = $true)]
		[PsfValidateScript('FileType.Validate.Paths', ErrorString = 'FileType.Validate.Paths.Failed')]
		[Alias('FullName')]
		[string[]]
		$Path
	)
	
	process
	{
		foreach ($pathItem in $Path)
		{
			foreach ($resolvedPath in $pathItem)
			{
				$item = Get-Item -Path $resolvedPath -Force
				if ($item.PSIsContainer) { continue }
				
				New-Object FileType.Resolution -Property @{
					FullName  = $resolvedPath
					FileTypes = @([FileType.FTHost]::ResolveType($item) | Where-Object { $_.Header.Count -gt 0 })
				}
			}
		}
	}
}