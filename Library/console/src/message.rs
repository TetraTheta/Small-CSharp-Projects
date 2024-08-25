// Since Windows also support ANSI escape code, I'll just use it to reduce dependency as much as possible.

/// Print message prepended by green 'INFO' text in StdOut.
/// # Example
/// ```rust
/// use console::info_green;
///
/// info_green!("hello ", "world!");
/// ```
/// # Output
/// ```plaintext
/// INFO hello world!
/// ```
#[macro_export]
macro_rules! info_green {
  ($($args:expr),*) => {{
    print!("\x1b[32mINFO\x1b[0m ");
    $(print!("{}", $args);)*
    println!();
  }};
}

/// Print message prepended by yellow 'WARN' text in StdOut.
/// # Example
/// ```rust
/// use console::warn_yellow;
///
/// warn_yellow!("hello", "world!");
/// ```
/// # Output
/// ```plaintext
/// WARN hello world!
/// ```
#[macro_export]
macro_rules! warn_yellow {
  ($($args:expr),*) => {{
    print!("\x1b[33mWARN\x1b[0m ");
    $(print!("{}", $args);)*
    println!();
  }};
}

/// Print message prepended by yellow 'WARN' text in StdErr.
/// # Example
/// ```rust
/// use console::error_red;
///
/// error_red!("hello ", "world!");
/// ```
/// # Output
/// ```plaintext
/// ERROR hello world!
/// ```
#[macro_export]
macro_rules! error_red {
  ($($args:expr),*) => {{
    eprint!("\x1b[31mERROR\x1b[0m ");
    $(eprint!("{}", $args);)*
    eprintln!();
  }};
}
