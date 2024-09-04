use arboard::Clipboard;
use argh::FromArgs;
use base64::prelude::*;
use console::{print_error, print_info, print_pinfo, print_pwarn};
use std::process::ExitCode;

#[derive(FromArgs)]
#[argh(description = "decode BASE64-encoded string and copy the decoded string to clipboard.")]
struct Cli {
  #[argh(positional, arg_name = "ENCODED", description = "BASE64-encoded string")]
  encoded: String,
}

fn main() -> ExitCode {
  let cli: Cli = argh::from_env();
  let encoded = cli.encoded;

  if encoded.is_empty() {
    print_pwarn!("The given argument is an empty string. Please provide a valid string.");
    return ExitCode::from(1);
  }

  print_info!("Original string: {}", encoded);

  let decoded = match BASE64_STANDARD.decode(encoded) {
    Ok(decoded) => decoded,
    Err(e) => {
      print_error!("Failed to decode string as BASE64: {}", e);
      return ExitCode::from(2);
    }
  };

  let conv = match String::from_utf8(decoded) {
    Ok(conv) => conv,
    Err(e) => {
      print_error!("Invalid UTF-8 string: {}", e);
      return ExitCode::from(3);
    }
  };

  print_info!("Decoded string: {}", conv);

  if copy_to_clipboard(&conv).is_ok() {
    print_pinfo!("Decoded string has been copied to the clipboard.");
  }

  ExitCode::SUCCESS
}

fn copy_to_clipboard(text: &str) -> Result<(), ()> {
  let mut cb = Clipboard::new().map_err(|e| {
    print_error!("Failed to create a new clipboard instance: {}", e);
  })?;
  cb.set_text(text.to_string()).map_err(|e| {
    print_error!("Failed to set a clipboard content: {}", e);
  })?;
  Ok(())
}
