@echo off
set "executable=b64d.exe"
set "target=x86_64-pc-windows-gnu"
set "RUSTFLAGS=-Z location-detail=none -C panic=abort"
cargo +nightly build --target %target% --release -Z build-std=std,panic_abort -Z build-std-features=optimize_for_size,panic_immediate_abort && for %%F in ("%~dp0\..\target\%target%\release\%executable%") do (copy "%%F" "%~dp0")
pause
