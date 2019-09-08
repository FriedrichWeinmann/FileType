function Test-FileType
{
<#
	.SYNOPSIS
		Tests, whether a specified file is of the type it proclaims to be.
	
	.DESCRIPTION
		Tests, whether a specified file is of the type it proclaims to be.
		Files of types that have not been registered will be returned as valid.
	
		Note that type determination is not a certain and guaranteed thing.
		Register detection patterns using Register-FileType.
		There are file types that share a common detection pattern.
		For example the common office files (pptx, docx, xslx, ...) share the same file headers and are not distinguishable.
		Renaming a word document to *.pptx will still test $true.
	
	.PARAMETER Path
		Path to the file to scan.
	
	.PARAMETER Quiet
		Only return $true or $false.
		By default, this command returns result objects including validity and path.
	
	.EXAMPLE
		PS C:\> Test-FileType -Path 'C:\temp\Presentation.pptx'
	
		Returns, whether the presentation.pptx file actually is a legal pptx file.
#>
	[CmdletBinding()]
	param (
		[Parameter(Mandatory = $true, ValueFromPipeline = $true, ValueFromPipelineByPropertyName = $true)]
		[PsfValidateScript('FileType.Validate.Paths', ErrorString = 'FileType.Validate.Paths.Failed')]
		[Alias('FullName')]
		[string[]]
		$Path,
		
		[switch]
		$Quiet
	)
	
	begin
	{
		$basePath = ''
	}
	process
	{
		foreach ($pathString in $Path)
		{
			foreach ($resolvedPath in (Resolve-PSFPath $pathString))
			{
				# Skip Folders
				if ((Get-Item -Path $resolvedPath -Force).PSIsContainer) { continue }
				
				if (-not $basePath -or -not $resolvedPath.StartsWith($basePath)) { $basePath = '{0}{1}' -f (Split-Path -Path $resolvedPath), ([System.IO.Path]::DirectorySeparatorChar) }
				
				$result = $null
				try
				{
					$result = [FileType.FTHost]::IsValidType($resolvedPath, $true)
					if ($Quiet)
					{
						$result
						continue
					}
					$success = $true
				}
				catch
				{
					if ($Qiet) { throw }
					else { $success = $false }
				}
				New-Object FileType.TestResult -Property @{
					IsValid  = $result
					Success  = $success
					FullName = $resolvedPath
					BasePath = $basePath
				}
			}
		}
	}
}