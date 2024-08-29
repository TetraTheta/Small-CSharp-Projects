//! Library about console input/output, and parsing them.
pub mod bool;
pub mod message;
pub mod switch;

// re-export
pub use bool::is_true;
pub use switch::check_common;
