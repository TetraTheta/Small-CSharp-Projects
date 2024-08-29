#[cfg(windows)]
fn main() {
  let res = tauri_winres::WindowsResource::new();
  res.compile().unwrap();
}

#[cfg(not(windows))]
fn main() {}
