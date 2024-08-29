// Since Windows also support ANSI escape code, I'll just use it to reduce dependency as much as possible.

// region: INFO (Green)
/// Print message prepended by green 'INFO' text in StdOut.<br>
/// First argument must be 'formattable string' that includes `{}`.
/// # Example
/// ```rust
/// use console::print_info;
///
/// print_info!("hello {}", "world!");
/// ```
/// # Output
/// ```plaintext
/// INFO hello world!
/// ```
#[macro_export]
macro_rules! print_info {
  ($fmt:expr $(, $arg:expr)*) => {
    print!("\x1b[32mINFO\x1b[0m ");
    println!($fmt $(, $arg)*);
  };
}
/// Print message prepended by green 'INFO' text in StdOut.<br>
/// This won't format given arguments.
/// # Example
/// ```rust
/// use console::print_pinfo;
///
/// print_pinfo!("hello {}", "world!");
/// ```
/// # Output
/// ```plaintext
/// INFO hello {}world!
/// ```
#[macro_export]
macro_rules! print_pinfo {
  ($($arg:expr),* $(,)?) => {
    print!("\x1b[32mINFO\x1b[0m ");
    $(print!("{}", $arg);)*
    println!();
  }
}
// endregion

// region: WARN (Yellow)
/// Print message prepended by yellow 'WARN' text in StdOut.<br>
/// First argument must be 'formattable string' that includes `{}`.
/// # Example
/// ```rust
/// use console::print_warn;
///
/// print_warn!("hello {}", "world!");
/// ```
/// # Output
/// ```plaintext
/// WARN hello world!
/// ```
#[macro_export]
macro_rules! print_warn {
  ($fmt:expr $(, $arg:expr)*) => {
    print!("\x1b[33mWARN\x1b[0m ");
    println!($fmt $(, $arg)*);
  };
}
/// Print message prepended by yellow 'WARN' text in StdOut.<br>
/// This won't format given arguments.
/// # Example
/// ```rust
/// use console::print_pwarn;
///
/// print_pwarn!("hello {}", "world!");
/// ```
/// # Output
/// ```plaintext
/// WARN hello {}world!
/// ```
#[macro_export]
macro_rules! print_pwarn {
  ($($arg:expr),* $(,)?) => {
    print!("\x1b[33mWARN\x1b[0m ");
    $(print!("{}", $arg);)*
    println!();
  }
}
// endregion

// region: ERROR (Red)
/// Print message prepended by red 'WARN' text in StdOut.<br>
/// First argument must be 'formattable string' that includes `{}`.
/// # Example
/// ```rust
/// use console::print_error;
///
/// print_error!("hello {}", "world!");
/// ```
/// # Output
/// ```plaintext
/// ERROR hello world!
/// ```
#[macro_export]
macro_rules! print_error {
  ($fmt:expr $(, $arg:expr)*) => {
    print!("\x1b[31mERROR\x1b[0m ");
    println!($fmt $(, $arg)*);
  };
}
/// Print message prepended by red 'WARN' text in StdOut.<br>
/// This won't format given arguments.
/// # Example
/// ```rust
/// use console::print_perror;
///
/// print_perror!("hello {}", "world!");
/// ```
/// # Output
/// ```plaintext
/// ERROR hello {}world!
/// ```
#[macro_export]
macro_rules! print_perror {
  ($($arg:expr),* $(,)?) => {
    print!("\x1b[31mERROR\x1b[0m ");
    $(print!("{}", $arg);)*
    println!();
  }
}
// endregion
