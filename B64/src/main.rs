use arboard::Clipboard;
use base64::prelude::*;
use console::{print_error, print_info, print_perror, print_pinfo, print_pwarn};
use std::env;

fn main() {
  let args: Vec<String> = env::args().skip(1).collect();
  let arg = args.into_iter().nth(0);
  match arg {
    Some(ref s) if !s.is_empty() => {
      print_info!("Original string: {}", s);
      match BASE64_STANDARD.decode(s) {
        Ok(decoded) => {
          match String::from_utf8(decoded) {
            Ok(conv) => {
              print_info!("Decoded string: {}", conv);
              match Clipboard::new() {
                Ok(mut cb) => {
                  if let Err(e) = cb.set_text(conv.clone()) {
                    print_perror!("Failed to set clipboard content: {}", e);
                  } else {
                    print_pinfo!("Decoded string has been copied to clipboard.");
                  }
                }
                Err(e) => { print_perror!("Failed to create clipboard: {}", e); }
              }
            }
            Err(e) => { print_error!("Invalid UTF-8 string: {}", e); }
          }
        }
        Err(e) => { print_error!("Failed to decode string: {}", e); }
      }
    }
    Some(_) => { print_pwarn!("Argument is an empty string. Please provide valid string."); }
    None => { print_pwarn!("No argument provided."); }
  }
}
