ls *.log `
| Sort-Object -Descending LastWriteTime `
| ForEach-Object {
    $file = $_
    return Get-Content $_.FullName `
        | ForEach-Object {
            @{
                file = $file;
                json = ConvertFrom-Json $_
            }
        }
} `
| Where-Object { $_.json.event -ne $null } `
| Where-Object { $_.json.event -imatch "material" } `
| ForEach-Object {
    @{
        file = $_.file;
        event = $_.json.event
    }
} `
| Sort-Object -Unique event