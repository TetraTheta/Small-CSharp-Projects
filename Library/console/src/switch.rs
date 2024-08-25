/// Check if 'yes switch' is present in given slice of `str`.
/// # Return
/// * `bool`: Whether 'yes switch' is present or not
/// * `Vec<String>`: Leftover
pub fn check_yes(args: Vec<String>) -> (bool, Vec<String>) {
  let mut is_yes: bool = false;
  let mut new_args = Vec::new();
  for arg in args {
    let lower = arg.to_lowercase();
    if lower == "/y" || lower == "/yes" || lower == "--yes" || lower == "-y" {
      is_yes = true;
    } else {
      new_args.push(arg.to_string())
    }
  }
  (is_yes, new_args)
}
