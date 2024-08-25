use console::{check_yes, info_green, is_true};
use path::{resolve_path_dir, to_windows_path_string};
use std::io::{stdin, stdout, Write};
use std::path::PathBuf;
use std::{env, fs, process};

fn main() {
  // Gather arguments
  let args: Vec<String> = env::args().skip(1).collect();
  let (is_yes, new_args) = check_yes(args);
  let target_string = new_args.into_iter().nth(0); // new_args is destroyed

  let target = resolve_path_dir(target_string);
  info_green!("Target: ", to_windows_path_string(&target));

  if is_yes {
    lowercase(target);
    info_green!("DONE");
  } else {
    print!("Lowercase all files and sub-directories? (yes|no)> ");
    stdout().flush().expect("Something went wrong");
    let mut user_input = String::new();
    stdin().read_line(&mut user_input).unwrap();
    if is_true(&user_input.trim().to_string()) {
      lowercase(target);
      info_green!("DONE");
    } else {
      info_green!("Aborted");
      process::exit(0);
    }
  }
}

fn lowercase(dir: PathBuf) {
  if let Ok(entries) = fs::read_dir(&dir) {
    for entry in entries {
      if let Ok(entry) = entry {
        let path = entry.path();
        let file_name = path.file_name().unwrap().to_str().unwrap().to_lowercase();
        let new_path = path.with_file_name(file_name);
        fs::rename(&path, &new_path).expect("Failed to rename");
        if new_path.is_dir() {
          lowercase(new_path);
        }
      }
    }
  }
}
