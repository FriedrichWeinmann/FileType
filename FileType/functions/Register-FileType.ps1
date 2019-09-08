function Register-FileType
{
<#
	.SYNOPSIS
		Registers a filetype with the parsing instructions to detect it.
	
	.DESCRIPTION
		Registers a filetype with the parsing instructions to detect it.
		The list populated with this data is used to verify, whether a file is of a specified type.
	
	.PARAMETER Type
		The type / file extension of the file type.
	
	.PARAMETER Mime
		The mime label files of that type have.
	
	.PARAMETER Offset
		The offset from where to start looking for the signature, verifying that the file is indeed of this type.
		Basically, the offset means "how many bytes, starting from the beginning of the file, to skip".
	
	.PARAMETER Bytes
		The byte-pattern to use to compare files with.
		Use $null as a wildcard entry that may be anything.
	
	.EXAMPLE
		PS C:\> Register-FileType -Type 'mp3' -Mime 'audio/mpeg' -Offset 0 -Bytes 73, 68, 51
	
		Registers the mp3 file type.
#>
	[CmdletBinding()]
	param (
		[Parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)]
		[AllowEmptyString()]
		[Alias('Extension')]
		[string[]]
		$Type,
		
		[Parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)]
		[string]
		$Mime,
		
		[Parameter(Mandatory = $true, ValueFromPipelineByPropertyName = $true)]
		[AllowEmptyCollection()]
		[AllowNull()]
		[Nullable[Byte][]]
		$Bytes = @(),
		
		[Parameter(ValueFromPipelineByPropertyName = $true)]
		[int]
		$Offset,
		
		[Parameter(ValueFromPipelineByPropertyName = $true)]
		[AllowEmptyString()]
		[string]
		$Description
	)
	
	process
	{
		if ($null -eq $Bytes) { $Bytes = @() }
		foreach ($typeName in $Type)
		{
			foreach ($typeElement in $typeName.Split(","))
			{
				$fileType = New-Object FileType.FileType($Bytes, $typeElement.Trim("."), $Mime, $Offset, $Description)
				[FileType.FTHost]::FileTypes.Add($fileType)
				if (($Bytes.Length + $Offset) -gt ([FileType.FTHost]::MaxHeader))
				{
					[FileType.FTHost]::MaxHeader = $Bytes.Length + $Offset
				}
			}
		}
	}
}