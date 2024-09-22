fn main() {
  if std::env::var("CARGO_CFG_TARGET_OS").unwrap() == "windows" {
    let res = tauri_winres::WindowsResource::new();
    res.compile().unwrap();
  }
}
