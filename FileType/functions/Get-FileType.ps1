function Get-FileType
{
<#
	.SYNOPSIS
		Returns the registered filetypes.
	
	.DESCRIPTION
		Returns the registered filetypes.
	
	.PARAMETER Type
		The type (or extension) to filter by.
	
	.EXAMPLE
		PS C:\> Get-FileType
	
		Returns all registered filetypes
#>
	[CmdletBinding()]
	Param (
		[string]
		$Type = '*'
	)
	
	process
	{
		[FileType.FTHost]::FileTypes | Where-Object Extension -Like $Type
	}
}