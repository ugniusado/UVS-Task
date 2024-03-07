$Env:UvsTaskPassword = "guest"
$Env:UvsTaskDatabase = "uvsproject"
$Env:UvsTaskPort = "7777"
$Env:UvsTaskSchemaLocation = $("$(Get-Location)\DatabaseSchema\dbSchema.sql")

Write-Host ""
Write-Host "This script will set up a postgres database hosted in a docker container"
read-host "Press ENTER to continue"

docker pull postgres
if (-not $?) { exit 1 }

$portAssign = $Env:UvsTaskPort + ":5432"
$container = $(docker run -e "POSTGRES_PASSWORD=$Env:UvsTaskPassword" -p "$portAssign" -d postgres)
if (-not $?) { exit 1 }

Try {
    Write-Host "Database starting. Setting db schema..."

    pushd DatabaseSchema
    # Pass 'setup-db' argument to trigger the correct action in your .NET application
    dotnet run -- setup-db
    if (-not $?) { exit 1 }

    Write-Host ""
    Write-Host "The database is ready to use" -ForegroundColor Green
    Write-Host "Connection string: 'Server=localhost; User ID=postgres; Password=$Env:UvsTaskPassword; Port=$Env:UvsTaskPort; Database=$Env:UvsTaskDatabase;'"
    Write-Host "Schema applied to database:"
    cat ./dbSchema.sql
    Write-Host ""
    Write-Host "Press Ctrl^C to stop the database server and exit" -ForegroundColor Green
} Finally {
    popd
    # Checks if the container exists and stops it
    $containerExists = docker ps -q -f id=$container
    if ($containerExists) {
        docker stop "$container"
    }
}

