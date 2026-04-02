param(
    [Parameter(Mandatory = $true)]
    [string]$ImageName,

    [string]$Tag = "latest",

    [string]$Platforms = "linux/amd64,linux/arm64",

    [string]$BuilderName = "multiarch-builder"
)

$ErrorActionPreference = "Stop"

$fullImageName = "$ImageName`:$Tag"

function Invoke-DockerCommand {
    param(
        [Parameter(Mandatory = $true)]
        [string[]]$Arguments
    )

    & docker @Arguments

    if ($LASTEXITCODE -ne 0) {
        throw "Docker command failed: docker $($Arguments -join ' ')"
    }
}

Write-Host "Checking Docker buildx builder '$BuilderName'..."
$builderExists = $false

& docker buildx inspect $BuilderName *> $null
if ($LASTEXITCODE -eq 0) {
    $builderExists = $true
}

if ($builderExists) {
    Write-Host "Using existing builder '$BuilderName'..."
    Invoke-DockerCommand -Arguments @("buildx", "use", $BuilderName)
}
else {
    Write-Host "Creating builder '$BuilderName'..."
    Invoke-DockerCommand -Arguments @("buildx", "create", "--name", $BuilderName, "--use")
}

Write-Host "Bootstrapping builder..."
Invoke-DockerCommand -Arguments @("buildx", "inspect", "--bootstrap")

Write-Host "Building and pushing $fullImageName for platforms: $Platforms"
Invoke-DockerCommand -Arguments @(
    "buildx", "build",
    "--platform", $Platforms,
    "-t", $fullImageName,
    "--push",
    "."
)

Write-Host ""
Write-Host "Done. Published image: $fullImageName"
Write-Host "You can verify it with:"
Write-Host "docker buildx imagetools inspect $fullImageName"
