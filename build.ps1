Set-Location .\helion4x-gdnative
Write-Output "Building binaries"
cargo build --release
Set-Location ..
Write-Output "Copying binaries to godot"
Copy-Item -Force .\helion4x-gdnative\target\release\helion4x_gdnative.dll .\helion4x\bin\helion4x_native.dll