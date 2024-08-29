use console::{check_common, is_true, print_info, print_pinfo};
use path::{resolve_path_dir, to_windows_path_string};
use std::io::{stdin, stdout, Write};
use std::path::{PathBuf, MAIN_SEPARATOR};
use std::{env, fs};

macro_rules! print_renamed {
  ($fmt:expr, $($args:tt)*) => {{
    print!("\x1b[33mRENAMED\x1b[0m ");
    println!($fmt, $($args)*);
  }};
}

fn main() {
  // Gather arguments
  let args: Vec<String> = env::args().skip(1).collect();
  let (is_yes, _, new_args) = check_common(args);
  let target_string = new_args.into_iter().nth(0); // new_args is destroyed

  let target = resolve_path_dir(target_string);
  print_info!("Target: {}", to_windows_path_string(&target));

  if is_yes {
    lowercase(target);
    print_pinfo!("DONE");
  } else {
    print!("Lowercase all files and sub-directories? (yes|no)> ");
    stdout().flush().expect("Something went wrong");
    let mut user_input = String::new();
    stdin().read_line(&mut user_input).unwrap();
    if is_true(&user_input.trim().to_string()) {
      lowercase(target);
      print_pinfo!("DONE");
    } else {
      print_pinfo!("Aborted");
    }
  }
}

fn lowercase(dir: PathBuf) {
  if let Ok(entries) = fs::read_dir(&dir) {
    let mut dirs = vec![];
    for entry in entries {
      if let Ok(entry) = entry {
        let path = entry.path();
        if path.is_dir() {
          dirs.push(path);
        } else {
          process_entry(path);
        }
      }
    }
    for dir in dirs {
      lowercase(dir.clone());
      process_entry(dir);
    }
  }
}

fn process_entry(path: PathBuf) {
  let file_name = path.file_name().unwrap().to_str().unwrap();
  let lower = file_name.to_lowercase();

  if file_name != lower {
    let new_path = path.with_file_name(&lower);
    print_renamed!(
      "{}{}{{{} -> {}}}",
      to_windows_path_string(&path.parent().unwrap().to_path_buf()),
      MAIN_SEPARATOR,
      file_name,
      lower
    );
    if let Err(e) = fs::rename(&path, &new_path) {
      eprintln!("Failed to rename {}: {}", to_windows_path_string(&path), e);
    }
  }
}
