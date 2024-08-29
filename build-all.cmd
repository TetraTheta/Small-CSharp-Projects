@echo off
mkdir ".build" >nul 2>&1
set "target=x86_64-pc-windows-gnu"
set "RUSTFLAGS=-Z location-detail=none -C panic=abort"
cargo +nightly build --target %target% --release -Z build-std=std,panic_abort -Z build-std-features=optimize_for_size,panic_immediate_abort && for %%F in ("%~dp0\target\%target%\release\*.exe") do (copy "%%F" "%~dp0\.build" 2>&1)
for %%G in ("%~dp0\.build\*.exe") do (upx --brute "%%G")
pause
