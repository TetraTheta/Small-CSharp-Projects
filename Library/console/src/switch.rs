/// Check if common switches are present in given slice of `str`.
/// - `is_yes`: `/y`, `/yes`, `--yes`, `-y`
/// - `is_recursive`: `/r`, `/recursive`, `--recursive`, `-r`
/// # Return
/// * `bool`: Whether 'yes switch' is present or not
/// * `Vec<String>`: Leftover
pub fn check_common(args: Vec<String>) -> (bool, bool, Vec<String>) {
  let mut is_yes: bool = false;
  let mut is_recursive: bool = false;
  let mut new_args = Vec::new();
  for arg in args {
    let lower = arg.to_lowercase();
    if lower == "/y" || lower == "/yes" || lower == "--yes" || lower == "-y" {
      is_yes = true;
    } else if lower == "/r" || lower == "/recursive" || lower == "--recursive" || lower == "-r" {
      is_recursive = true;
    } else {
      new_args.push(arg.to_string())
    }
  }
  (is_yes, is_recursive, new_args)
}
