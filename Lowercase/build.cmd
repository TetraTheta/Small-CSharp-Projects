@echo off
set "executable=lowercase.exe"
set "RUSTFLAGS=-Z location-detail=none -C panic=abort"
cargo +nightly build --target x86_64-pc-windows-gnu --release -Z build-std=std,panic_abort -Z build-std-features=optimize_for_size,panic_immediate_abort && for %%F in ("%~dp0\..\target\x86_64-pc-windows-gnu\release\%executable%") do (copy "%%F" "%~dp0")
pause
