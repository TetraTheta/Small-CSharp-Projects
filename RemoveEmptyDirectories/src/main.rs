use std::{env, fs};
use std::path::{Path, PathBuf};
use std::process::ExitCode;
use argh::FromArgs;
use ini::Ini;
use path::to_windows_path_string;

macro_rules! print_info {
  ($msg:expr $(, $arg:expr)*) => {
    print!("\x1b[32mINFO\x1b[0m ");
    println!("{}", format!($msg $(, $arg)*));
  }
}
macro_rules! print_error {
  ($msg:expr $(, $arg:expr)*) => {
    eprint!("\x1b[31mERROR\x1b[0m ");
    eprintln!("{}", format!($msg $(, $arg)*));
  }
}
macro_rules! print_skip {
  ($msg:expr $(, $arg:expr)*) => {
    print!("\x1b[33mSKIP\x1b[0m ");
    println!("{}", format!($msg $(, $arg)*));
  }
}
macro_rules! print_remove {
  ($msg:expr $(, $arg:expr)*) => {
    print!("\x1b[33mREMOVE\x1b[0m ");
    println!("{}", format!($msg $(, $arg)*));
  }
}

#[derive(FromArgs)]
#[argh(description = "remove empty directories")]
struct Cli {
  #[argh(positional, arg_name = "DIRECTORY", description = "target directory for scanning empty directories")]
  target: PathBuf,
  #[argh(switch, short = 'f', description = "whether to ignore any directory protection")]
  force: bool,
}

fn main() -> ExitCode {
  // get arguments
  let cli: Cli = argh::from_env();
  let force = cli.force;
  let target = match cli.target.canonicalize() {
    Err(e) => {
      print_error!("Failed to get valid path: {}", e);
      return ExitCode::from(1);
    },
    Ok(path) => {
      if !path.is_dir() {
        print_error!("The given path is not a directory: {}", to_windows_path_string(&path));
        return ExitCode::from(1);
      } else {
        path
      }
    }
  };

  // debug
  println!("## Cli Target: {}", to_windows_path_string(&target));
  println!("## Cli Force: {}", &force);

  // get important directories
  let config_path = find_config_file();
  let section = Ini::load_from_file(config_path).unwrap().section(Some("ImportantDirectories"));
  if let Err(e) = section {
    print_error!("Failed to get 'ImportantDirectories' section from config file: {}", e);
    return ExitCode::from(2);
  }
  let idirs = section.unwrap().iter().map(|(_, v)| v.to_string()).collect::<Vec<String>>();

  // debug
  println!("## Important Directories: {}", &idirs.join(","));

  if check_dir_names(&target, &idirs) {
    if (force) {
      print_info!("The given directory contains important directory: ")
    } else {

    }
  }


  ExitCode::SUCCESS
}

/// Returns `true` if given `str` starts or ends with any element of `idirs`
fn check_dir_name(target: &str, idirs: &[String]) -> bool {
  let target_lower = target.to_lowercase();
  for idir in idirs {
    let idir_lower = idir.to_lowercase();
    if idir_lower.ends_with("*") {
      let pattern = &idir_lower[..idir_lower.len() - 1];
      if target_lower.ends_with(pattern) {
        return true;
      }
    } else if idir_lower.starts_with("*") {
      if idir_lower.len() > 1 {
        let pattern = &idir_lower[1..];
        if target_lower.starts_with(pattern) {
          return true;
        }
      } else {
        print_error!("Invalid INI entry: {}", idir);
        std::process::exit(3);
      }
    } else if target_lower == idir_lower {
      return true;
    }
  }
  false
}

/// Returns `true` if given `Path` contains any element of `idirs`
fn check_dir_names(target: &Path, idirs: &[String]) -> bool {
  // TODO: return `bool` and formatted `str`(or `String`)
  let mut components = target.components().map(|comp| comp.as_os_str().to_string_lossy().to_string()).collect::<Vec<String>>();
  let target_name = components.pop().unwrap_or_default();
  if check_dir_name(&target_name, &idirs) {
    return true;
  }
  while let Some(parent) = components.pop() {
    if check_dir_name(&parent, &idirs) {
      return true;
    }
  }
  false
}

fn find_config_file() -> PathBuf {
  let mut path = env::current_exe().unwrap().canonicalize().unwrap();
  path.set_file_name("red.ini");

  if !path.exists() {
    path = if cfg!(target_os = "windows") {
      let appdata = env::var("APPDATA").unwrap();
      PathBuf::from(format!("{}/RemoveEmptyDirectories/red.ini", appdata))
    } else {
      let home = env::var("HOME").unwrap();
      PathBuf::from(format!("{}/.config/RemoveEmptyDirectories/red.ini", home))
    };
  }
  println!("## Config Path: {:?}", to_windows_path_string(&path));

  if !path.exists() {
    fs::create_dir_all(path.parent().unwrap()).unwrap();
    let mut config = Ini::new();
    config.with_section(Some("ImportantDirectories"))
      .set("1", "System Volume Information")
      .set("2", "RECYCLER")
      .set("3", "Recycled")
      .set("4", "NtUninstall")
      .set("5", "$RECYCLE.BIN")
      .set("6", "GAC_*")
      .set("7", "WinSxS")
      .set("8", "System32")
      .set("9", "Start Menu");
    config.write_to_file(&path).unwrap();
    print_info!("Config file create at: {}", to_windows_path_string(&path));
  }
  path
}

/*

fn main() -> ExitCode {
  check_important(&target, &idirs, cli.force);
  scan_and_clean(&target, &idirs, cli.force);

  ExitCode::SUCCESS
}

fn check_important(target: &Path, idirs: &[String], force: bool) {
  let mut current_dir = Some(target);
  while let Some(path) = current_dir {
    let path_string = path.to_string_lossy();
    for idir in idirs {
      if path_string == *idir {
        if !force {
          print_error!("The given path is important directory: {}", to_windows_path_string(&target.to_path_buf()).replace(idir, &format!("\x1b[33m{}\x1b[0m", idir)));
          std::process::exit(1);
        }
      }
    }
    current_dir = path.parent().unwrap().parent();
  }
}



fn load_importants(ini_path: &Path) -> Vec<String> {

}

fn scan_and_clean(target: &Path, idirs: &[String], force: bool) {
  println!("Scanning directory: {:?}", target);

  let read_dir = fs::read_dir(target);
  match read_dir {
    Ok(entries) => {
      for entry in entries {
        let entry = match entry {
          Ok(entry) => entry,
          Err(e) => {
            print_error!("Error reading directory: {}", e);
            continue;
          }
        };
        let path = entry.path();
        println!("Checking entry: {:?}", path);
        if path.is_dir() {
          for idir in idirs {
            if Pattern::new(idir).unwrap().matches_path(&path) {
              if !force {
                print_skipped!("{}", to_windows_path_string(&path).replace(idir, &format!("\x1b[33m{}\x1b[0m", idir)));
                return;
              }
            }
          }
          scan_and_clean(&target, idirs, force);
          if let Ok(count) = fs::read_dir(&path).map(|r| r.count()) {
            if count == 0 {
              print_removed!("{}", to_windows_path_string(&path));
              if let Err(e) = fs::remove_dir(&path) {
                println!("Failed to remove directory: {:?}", e);
              }
            }
          } else {
            println!("Failed to count entries in {:?}", path);
          }
        }
      }
    }
    Err(e) => {
      println!("Error reading directory: {}", e);
    }
  }
}
*/
