pub struct CheckResult {
  pub is_force: bool,
  pub is_recursive: bool,
  pub is_yes: bool,
}

/// Check if common switches are present in given slice of `str`.
/// - `is_yes`: `/y`, `/yes`, `--yes`, `-y`
/// - `is_recursive`: `/r`, `/recursive`, `--recursive`, `-r`
/// # Return
/// * `bool`: Whether 'yes switch' is present or not
/// * `Vec<String>`: Leftover
pub fn check_common(args: Vec<String>) -> (CheckResult, Vec<String>) {
  let mut is_force: bool = false;
  let mut is_recursive: bool = false;
  let mut is_yes: bool = false;
  let mut new_args = Vec::new();
  for arg in args {
    let lower = arg.to_lowercase();
    if lower == "/f" || lower == "/force" || lower == "--force" || lower == "-f" {
      is_force = true;
    } else if lower == "/r" || lower == "/recursive" || lower == "--recursive" || lower == "-r" {
      is_recursive = true;
    } else if lower == "/y" || lower == "/yes" || lower == "--yes" || lower == "-y" {
      is_yes = true;
    } else {
      new_args.push(arg.to_string())
    }
  }
  (CheckResult { is_force, is_recursive, is_yes }, new_args)
}
