# FileType

## Description

Welcome to the FileType PowerShell Module project.
This module is designed to help validate files - that is, ensuring a file is of the type it claims to be.

As such, it should help detect corrupted files or files.

## Getting Started

To install the module run:

```powershell
Install-Module FileType
```

Once the module has been installed, it can be used to test integrity or resolve the probable type of a file:

```powershell
# Test file integrity
Get-ChildItem -Path D:\Data -Recurse | Test-FileType

# Resolve type
Get-ChildItem -Path D:\Data -Recurse | Resolve-FileType
```

## How does it work?

The FileType module reads the header of each file to determine its type.
This is powered by a predefined list of types, their extensions and their signatures.

Files with extensions for which no type has been registered will always test true, as we are unable to find a determination.
At the same time, some extensions are used for more than one type and there may be false negatives if the expected type is not yet registered, but another type for the same extension has been.

To get a list of signatures available, run `Get-FileType`.
To register your own signatures, use `Register-FileType`.

## Disclaimer

This module is provided "as is" under the MIT license.
It is designed to help with analysis, but there is no guarantee whatsoever.
Providing correct signatures for every single file type in existence is pretty much impossible, especially for a small project such as this.

So ... use with common sense and be aware of its limitations.
