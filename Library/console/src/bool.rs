/// Check if given `str` is truthful.
/// Valid values:
/// * `1`
/// * `ok`
/// * `t`
/// * `true`
/// * `y`
/// * `yes`
pub fn is_true(value: &String) -> bool {
  matches!(value.to_lowercase().as_str(), "1" | "ok" | "t" | "true" | "y" | "yes")
}
