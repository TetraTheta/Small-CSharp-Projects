use console::error_red;
use std::path::{Path, PathBuf};
use std::{env, process};

/// Get absolute path from either given `Option<String>` or current working directory.
///
/// If given path is valid directory, absolute path of the directory will be returned.
/// If given path is empty `String`, current working directory will be returned.
/// # Usage
/// ```rust
/// use path::resolve_path_dir;
///
/// let path1 = Some(r"C:\".to_string());
/// let path2 = None;
/// println!("{}", resolve_path_dir(path1).display());
/// println!("{}", resolve_path_dir(path2).display());
/// ```
pub fn resolve_path_dir(target: Option<String>) -> PathBuf {
  match target {
    Some(ref path_str) => {
      let path = Path::new(path_str);
      if path.exists() {
        if path.is_dir() {
          let abs_path = path.canonicalize().unwrap_or_else(|_| {
            error_red!("Failed to canonicalize the path: {:?}", path.display());
            process::exit(1);
          });
          abs_path
        } else {
          error_red!("Path is not a directory: {:?}", path.display());
          process::exit(1);
        }
      } else {
        error_red!("Path does not exist: {:?}", path.display());
        process::exit(1);
      }
    }
    None => env::current_dir().unwrap_or_else(|_| {
      error_red!("Failed to get the current working directory");
      process::exit(1);
    }),
  }
}

/// Convert `PathBuf` to `String` which represents Windows Path.
/// This is basically removing `\\?\` from lossy string of `PathBuf`.
/// # Usage
/// ```rust
/// use path::to_windows_path_string;
/// use std::path::Path;
///
/// let path = Path::new(r"C:\");
/// let path_buf = path.canonicalize().unwrap().to_path_buf();
/// assert_eq!(r"C:\", to_windows_path_string(&path_buf));
/// ```
pub fn to_windows_path_string(path_buf: &PathBuf) -> String {
  let cloned = path_buf.clone();
  let lossy = cloned.to_string_lossy();
  lossy.strip_prefix(r"\\?\").unwrap_or(&lossy).to_string()
}
