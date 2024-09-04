use argh::FromArgs;
use console::{is_true, print_error, print_info, print_pinfo};
use path::to_windows_path_string;
use std::fs;
use std::io::{stdin, stdout, Write};
use std::path::{PathBuf, MAIN_SEPARATOR};
use std::process::ExitCode;

macro_rules! print_renamed {
  ($path:expr, $separator:expr, $old:expr, $new:expr) => {{
    print!("\x1b[33mRENAMED\x1b[0m ");
    print!("{}", $path);
    print!("{}", $separator);
    print!("{{\x1b[33m{}\x1b[0m", $old);
    println!(" -> \x1b[32m{}\x1b[0m}}", $new);
  }};
}

#[derive(FromArgs)]
#[argh(description = "rename any uppercase letters to lowercase in name of files or directories")]
struct Cli {
  #[argh(positional, arg_name = "DIRECTORY", description = "target directory to scan names")]
  target: PathBuf,
  #[argh(switch, short = 'y', description = "whether to skip confirmation")]
  yes: bool,
}

fn main() -> ExitCode {
  let cli: Cli = argh::from_env();
  let target = match cli.target.canonicalize() {
    Ok(target) => target,
    Err(e) => {
      print_error!("Failed to get valid path: {}", e);
      return ExitCode::from(1);
    }
  };
  print_info!("Target: {}", to_windows_path_string(&target));

  if cli.yes {
    lowercase(target);
    print_pinfo!("DONE");
    ExitCode::SUCCESS
  } else {
    print!("Lowercase all files and sub-directories? (yes|no)> ");
    stdout().flush().unwrap();
    let mut user_input = String::new();
    stdin().read_line(&mut user_input).unwrap();
    if is_true(&user_input.trim().to_string()) {
      lowercase(target);
      print_pinfo!("DONE");
      ExitCode::SUCCESS
    } else {
      print_pinfo!("Aborted");
      ExitCode::SUCCESS
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
