use arboard::Clipboard;
use base64::prelude::*;
use console::{print_error, print_info, print_pinfo, print_pwarn};
use std::env;

fn main() {
  let args: Vec<String> = env::args().skip(1).collect();
  let arg = args.into_iter().nth(0);

  if arg.is_none() {
    print_pwarn!("No argument provided.");
    return;
  }
  let s = arg.unwrap();
  if s.is_empty() {
    print_pwarn!("Argument is an empty string. Please provide valid string.");
    return;
  }

  print_info!("Original string: {}", s);

  let decoded = match BASE64_STANDARD.decode(s) {
    Ok(decoded) => decoded,
    Err(e) => {
      print_error!("Failed to decode string as Base64: {}", e);
      return;
    }
  };

  let conv = match String::from_utf8(decoded) {
    Ok(conv) => conv,
    Err(e) => {
      print_error!("Invalid UTF-8 string: {}", e);
      return;
    }
  };

  print_info!("Decoded string: {}", conv);

  if copy_to_clipboard(&conv).is_ok() {
    print_pinfo!("Decoded string has been copied to clipboard.");
  }
}

fn copy_to_clipboard(text: &str) -> Result<(), ()> {
  let mut cb = Clipboard::new().map_err(|e| {
    print_error!("Failed to create clipboard: {}", e);
  })?;
  cb.set_text(text.to_string()).map_err(|e| {
    print_error!("Failed to set clipboard content: {}", e);
  })?;
  Ok(())
}
